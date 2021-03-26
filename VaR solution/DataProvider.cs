using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaR_solution
{
    abstract class DataProvider
    {
        protected List<Company> companies = new List<Company>();
        protected Matrice matriceAction;

        protected abstract void readCompanyData();

        public Matrice GetMatriceAction()
        {
            return matriceAction;
        }

        public List<Company> GetCompanyList()
        {
            return companies;
        }
        // récupére les données à partir de la source ( csv,excel, yahoo) et les transforme en matrice


    }
}
