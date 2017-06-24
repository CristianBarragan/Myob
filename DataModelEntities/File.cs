using System;
using System.Collections.Generic;
using System.Text;

namespace DataModelEntities
{
    public class File
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<FilePayRoll> FilePayRolls { get; set; }
    }
}
