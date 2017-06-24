using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DomainService.Interfaces
{
    public interface IFileProcess
    {
        string Validate(IFormFile file);
        IEnumerable<string> Generate(IFormFile file);
    }
}
