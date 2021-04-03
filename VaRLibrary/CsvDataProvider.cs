using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaRLib
{
    public class CsvDataProvider : DataProvider
    {
        public CsvDataProvider(string path, int days=-1) : base(days)
        {

        }
        protected override void readCompanyData()
        {
            throw new NotImplementedException();
        }
    }
}
