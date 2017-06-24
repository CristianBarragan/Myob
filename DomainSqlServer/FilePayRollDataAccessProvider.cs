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
    public class FilePayRollDataAccessProvider : IFilePayrollDataAccessProvider
    {
        private readonly DomainModelContext _context;
        private readonly ILogger _logger;

        public FilePayRollDataAccessProvider(DomainModelContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessMSQL");
        }

        public FilePayRoll Add(FilePayRoll payRoll)
        {
            if (payRoll.File != null && payRoll.FileId == 0)
            {
                _context.Files.Add(payRoll.File);
            }

            _context.FilePayrolls.Add(payRoll);
            _context.SaveChanges();
            return payRoll;
        }

        public void Update(FilePayRoll payRoll)
        {
            _context.FilePayrolls.Update(payRoll);
            _context.SaveChanges();
        }

        public void Delete(long payRollId)
        {
            var entity = _context.FilePayrolls.First(t => t.Id == payRollId);
            _context.FilePayrolls.Remove(entity);
            _context.SaveChanges();
        }

        public FilePayRoll Get(long payRollId)
        {
            return _context.FilePayrolls.Include(x => x.File).First(t => t.Id == payRollId);
        }

        public List<FilePayRoll> Get()
        {
            // Using the shadow property EF.Property<DateTime>(dataEventRecord)
            return _context.FilePayrolls.Include(x => x.File).OrderByDescending(filePayRoll => EF.Property<DateTime>(filePayRoll, "UpdatedTimestamp")).ToList();
        }

        public List<FilePayRoll> GetByFile(long FileId)
        {
            // Using the shadow property EF.Property<DateTime>(dataEventRecord)
            return _context.FilePayrolls.Include(x => x.File).Where(p => p.FileId == FileId).OrderByDescending(filePayRoll => EF.Property<DateTime>(filePayRoll, "UpdatedTimestamp")).ToList();
        }
    }
}
