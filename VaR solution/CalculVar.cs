using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaR_solution
{   
    // Input : Matrice de la valeurs des actions du portefeuille
    abstract class CalculVar
    {
        protected Matrice Actions;
        protected Company[] CompanyTab; 
        protected double VaR_result; // Résultat final de notre VaR
        protected double alpha; // seuil de risque

        public CalculVar(DataProvider dp,double alpha=0.01)
        {
            this.alpha = alpha;

            Actions = dp.GetMatriceAction();
            List<Company> companies= dp.GetCompanyList();
            CompanyTab = new Company[companies.Count()];

            int i = 0;
            foreach (Company item in companies)
            {
                CompanyTab[i]=item;
                i++;
            }

        }

        public abstract double Calcul();
        

    }
}
