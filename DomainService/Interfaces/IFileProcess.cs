using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DomainService.Interfaces
{
    public interface IFileProcess
    {
        bool Validate(IFormFile file, out string line);
        IEnumerable<string> Generate(IFormFile file);
    }
}
