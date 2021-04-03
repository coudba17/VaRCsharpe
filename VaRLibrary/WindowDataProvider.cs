using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaRLib
{
    public class WindowDataProvider : DataProvider
    {
        protected override void readCompanyData()
        {
            
        }

        public void SetCompanyList(List<Company> companies)
        {
            this.companies = companies;
        }

        public WindowDataProvider(Matrice actions,int i0,int days):base(days)
        {
            matriceAction = new Matrice(days, actions.Colonnes);

            for (int i = i0; i < i0+days; i++)
            {
                for (int j = 0; j < actions.Colonnes; j++)
                {
                    matriceAction[i - i0, j] = actions[i, j];
                }
            }
        }


    }
}
