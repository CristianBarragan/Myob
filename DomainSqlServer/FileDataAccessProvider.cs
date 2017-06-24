using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelEntities;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DomainSqlServer
{
    public class FileDataAccessProvider : IFileDataAccessProvider
    {
        private readonly DomainModelContext _context;
        private readonly ILogger _logger;

        public FileDataAccessProvider(DomainModelContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessMSQL");
        }

        public File Add(File file)
        {
            _context.Files.Add(file);
            _context.SaveChanges();
            return file;
        }

        public void Update(File file)
        {
            _context.Files.Update(file);
            _context.SaveChanges();
        }

        public void Delete(long fileId)
        {
            var entity = _context.Files.First(t => t.Id == fileId);
            _context.Files.Remove(entity);
            _context.SaveChanges();
        }

        public File Get(long fileId)
        {
            return _context.Files.First(t => t.Id == fileId);
        }

        public File Get(string name)
        {
            return _context.Files.First(t => t.Name == name);
        }

        public List<File> Get(bool withChildren)
        {
            // Using the shadow property EF.Property<DateTime>(srcInfo)
            if (withChildren)
            {
                return _context.Files.Include(s => s.FilePayRolls).OrderByDescending(srcInfo => EF.Property<DateTime>(srcInfo, "UpdatedTimestamp")).ToList();
            }

            return _context.Files.OrderByDescending(srcInfo => EF.Property<DateTime>(srcInfo, "UpdatedTimestamp")).ToList();
        }
    }
}
