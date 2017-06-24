using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DomainService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Storage;

namespace DomainService.Mappers
{
    public class FileProcess : IFileProcess
    {
        public IData Data { get; set; }

        public FileProcess(IData data)
        {
            Data = data;
        }

        public IEnumerable<string> Generate(IFormFile file)
        {
            List<string> output = new List<string>();
            output.Add(initializeHeaders());
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while(!reader.EndOfStream)
                {
                    output.Add(calculatePayRoll(reader.ReadLine().Split(','), Data.getTaxes()));
                }
            }
            return output;
        }

        private string initializeHeaders()
        {
            return "name, pay, period, gross income, income tax, net income, super \n";
        }

        private string calculatePayRoll(String[] data, List<Tax> taxes)
        {
            Tax tax = null;
            string line = data[0]+ " "+data[1];
            line = line + "," + data[4];
            long grossIncome = (long)Math.Round((decimal.Parse(data[2]) / 12), MidpointRounding.AwayFromZero);
            line = line + "," + grossIncome.ToString();
            long salary = long.Parse(data[2]);
            tax = taxes.Find(t => t.bottomRange < salary && t.upperRange > salary);
            long incomeTax = (long)Math.Round(((decimal)tax.BaseTax + ((salary - (tax.bottomRange - 1))*tax.variableTax)) / 12, MidpointRounding.AwayFromZero);
            line = line + "," + incomeTax.ToString();
            line = line + "," + (grossIncome - incomeTax).ToString();
            var percentage = data[3].Split('%');
            line = line + "," + Math.Round((grossIncome*(decimal.Parse(percentage[0])/100)), MidpointRounding.AwayFromZero).ToString() + "\n";
            return line;
        }

        public bool Validate(IFormFile file, out string line)
        {
            int lineCount = 1;
            bool valid = true;
            line = "";
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    line = "";
                    var data = reader.ReadLine().Split(',');
                    if (data.Length != 5)
                    {
                        line = "Line [" + lineCount + "] has an incorrect number of columns";
                        return false;
                    }
                    long salary;
                    bool res = long.TryParse(data[2], out salary);
                    if (!res)
                    {
                        line = "Line [" + lineCount + "] is not a number";
                        return false;
                    }
                    if (salary < 0)
                    {
                        line = "Line [" + lineCount + "] has a negative salary";
                        return false;
                    }
                    var percentage = data[3].Split('%');
                    if (percentage.Length > 2 || !string.IsNullOrEmpty(percentage[1]))
                    {
                        line = "Line [" + lineCount + "] super rate is not a percentage";
                        return false;
                    }
                    lineCount++;
                }
                return valid;
            }
        }
    }
}
