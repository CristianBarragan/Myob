using System;
using System.Collections.Generic;

namespace Storage
{
    public class Data : IData
    {
        public List<Tax> getTaxes()
        {
            List<Tax> taxes = new List<Tax>();
            taxes.Add(new Tax { bottomRange = 1, upperRange = 18200, BaseTax = 0, variableTax = 0 });
            taxes.Add(new Tax { bottomRange = 18201, upperRange = 37000, BaseTax = 0, variableTax = (decimal)0.19 });
            taxes.Add(new Tax { bottomRange = 37001, upperRange = 80000, BaseTax = 3572, variableTax = (decimal)0.325 });
            taxes.Add(new Tax { bottomRange = 80001, upperRange = 180000, BaseTax = 17547, variableTax = (decimal)0.37 });
            taxes.Add(new Tax { bottomRange = 180001, upperRange = 100000000, BaseTax = 54547, variableTax = (decimal)0.45 });
            return taxes;
        }
    }
}
