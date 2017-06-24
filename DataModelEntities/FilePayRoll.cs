using System;
using System.Collections.Generic;
using System.Text;

namespace DataModelEntities
{
    public class FilePayRoll
    {
        public long Id { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long AnnualSalary { get; set; }
        public long SuperRate { get; set; }
        public string PaymentStartDate { get; set; }
        public long GrossIncome { get; set; }
        public long IncomeTax { get; set; }
        public long NetIncome { get; set; }
        public long Super { get; set; }
        public File File { get; set; }
        public long FileId { get; set; }
    }
}
