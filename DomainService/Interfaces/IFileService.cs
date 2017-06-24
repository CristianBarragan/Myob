using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DomainService.Interfaces
{
    public interface IFileService
    {
        IEnumerable<string> process(IFormFileCollection files);
    }
}
