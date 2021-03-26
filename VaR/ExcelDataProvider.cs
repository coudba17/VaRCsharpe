using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Outils;



namespace VaR
{
    class ExcelDataProvider : DataProvider
    {
        private Excel.Application xapp = null;
        private Excel.Workbook xlWorkBook = null;
        private Excel.Worksheet xlWorksheet = null;
        private Excel.Range range=null;
        private int nrows = 0;
        private int ncols = 0;

        public ExcelDataProvider(string path,int days=-1):base(days)
        {
            // va créer un objet qui peut interagir avec un document excel
            
            xapp = new Excel.Application();
            xlWorkBook = xapp.Workbooks.Open(path);
            xlWorksheet = xlWorkBook.Sheets[1] as Excel.Worksheet;

            range = xlWorksheet.UsedRange;
            nrows = range.Rows.Count;
            ncols = range.Columns.Count;

            if (days == -1)
            {
                days = nrows;
            }

            Console.WriteLine("Reading company data");
            readCompanyData();
            Console.WriteLine("Finish data reading");
            matriceAction = new Matrice(days,ncols-1);
            Console.WriteLine("Reading matrice");



            int i0 = (nrows - days + 1);
            for (var i =i0 ; i <= nrows; i++)
            {
                for (var j = 2; j <= ncols; j++)
                {
                    matriceAction[i-i0,j-2] = (range.Cells[i, j] as Excel.Range).Value;
                    
                }
            }
            Console.WriteLine("End reading matrice");
            //cleanup
            Console.WriteLine("Closing excel app");
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad


            //close and release
            xlWorkBook.Close();


            //quit and release
            xapp.Quit();
            Console.WriteLine("Excel app  closed");

        }

        protected override void readCompanyData()
        {
            for (int j = 2; j <= ncols; j++)
            {   
                string name= (range.Cells[1, j] as Excel.Range).Value;
                double weight = (range.Cells[2, j] as Excel.Range).Value;
                companies.Add(new Company(name,weight));
            }
        }
    }
}
