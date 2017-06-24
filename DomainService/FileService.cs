using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DomainService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace DomainService.Services
{
    public class FileService : IFileService
    {
        private IFileProcess FileMapper { get; set; }

        public FileService(IFileProcess fileMapper)
        {
            FileMapper = fileMapper;
        }

        public IEnumerable<string> process(IFormFileCollection files)
        {
            List<string> result = new List<string>();
            string line = "";
            foreach (var file in files)
            {
                result.Add(file.FileName);
                if (FileMapper.Validate(file, out line))
                    result.AddRange(FileMapper.Generate(file));
                else
                    result.Add(line);
            }
            return result;
        }
    }
}
