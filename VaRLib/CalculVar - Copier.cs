using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaRLib
{   
    // Input : Matrice de la valeurs des actions du portefeuille
    abstract class CalculVar
    {
        protected Matrice Actions;
        protected Company[] CompanyTab; 
        protected double alpha; // seuil de risque
        protected Matrice rendements; 
             
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

        public Matrice GetRendement()
        {
            return rendements;
        }

        protected double CalculCVar(Matrice rendements, int j)
        {
            List<double> rendements_j = new List<double>();
            for (int i = 0; i < rendements.Lignes; i++)
            {
                rendements_j.Add(rendements[i, j]);
            }

            rendements_j.Sort();



            return CalculCvarFromSorted(rendements_j);
        }


        protected double CalculCvarFromSorted(List<double> rendements_j)
        {

            double Cvar = 0;

            int position = 0;
            if (Math.Floor(alpha * rendements_j.Count) == alpha * rendements_j.Count)
            {
                position = (int)(alpha * rendements_j.Count);
            }
            else
            {
                position = (int)Math.Floor(alpha * rendements_j.Count) + 1;
            }

            for (int i = 0; i < position; i++)
            {
                Cvar += rendements_j[i];
            }

            Cvar /= position;

            return Cvar;

        }

        protected double[] CalculCvarAll(Matrice rendements)
        {
            double[] CvarActifs = new double[rendements.Colonnes];
            for (int j = 0; j < rendements.Colonnes; j++)
            {
                CvarActifs[j] = CalculCVar(rendements, j);
            }

            return CvarActifs;
        }

        public abstract Tuple<double, double> Calcul();
        

    }
}
