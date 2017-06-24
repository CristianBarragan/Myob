using System;
using System.Collections.Generic;
using System.Text;
using DataModelEntities;

namespace DomainModel
{
    public interface IFilePayrollDataAccessProvider
    {
        FilePayRoll Add(FilePayRoll payRoll);
        void Update(FilePayRoll payRoll);
        void Delete(long payRollId);
        FilePayRoll Get(long payRollId);
        List<FilePayRoll> Get();
        List<FilePayRoll> GetByFile(long fileId);
    }
}
