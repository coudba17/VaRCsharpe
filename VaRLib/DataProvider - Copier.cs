using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaRLib
{
    abstract class DataProvider
    {
        protected List<Company> companies = new List<Company>();
        protected Matrice matriceAction;
        protected int days; 
        protected abstract void readCompanyData();

        public DataProvider(int days=-1) 
        {   
            this.days = days;
        }
        public Matrice GetMatriceAction()
        {
            return matriceAction;
        }

        public List<Company> GetCompanyList()
        {
            return companies;
        }
        // récupére les données à partir de la source ( csv,excel, yahoo) et les transforme en matrice

        public int Days
        {
            get { return days; }
        }

        public Matrice MatriceAction
        {
            get { return matriceAction; }
        }
    }
}
