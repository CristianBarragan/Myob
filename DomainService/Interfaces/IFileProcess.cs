using System.Collections.Generic;
using DataModelEntities;
using Microsoft.AspNetCore.Http;

namespace DomainService.IMappers
{
    public interface IFileProcess
    {
        string Validate(IFormFile file);
        IEnumerable<string> Generate(IFormFile file);
    }
}
