using System;
using System.Collections.Generic;
using System.Text;
using DataModelEntities;

namespace DomainModel
{
    public interface IFileDataAccessProvider
    {
        File Add(File file);
        void Update(File file);
        void Delete(long fileId);
        File Get(long fileId);
        File Get(string Name);
        List<File> Get(bool withChildren);
    }
}
