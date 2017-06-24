using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DomainService.IServices
{
    public interface IFileService
    {
        IEnumerable<string> process(IFormFileCollection files);
    }
}
