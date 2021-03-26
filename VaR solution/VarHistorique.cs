using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaR_solution
{   
    class VarHistorique : CalculVar
    {
        public VarHistorique(DataProvider dp,double alpha=0.01) : base(dp,alpha)
        {
        }

        public override double Calcul()
        {
            VaR_result = 0;
            int position = 0;
            if (Math.Floor(alpha * Actions.Lignes)==alpha*Actions.Lignes)
            {
                position = (int)(alpha * Actions.Lignes);
            }
            else
            {
                position = (int)Math.Floor(alpha * Actions.Lignes) + 1;
            }



            Matrice  rendements = new Matrice(Actions.Lignes - 1, Actions.Colonnes);
            double[] VaR_actif = new double[Actions.Colonnes];
 
            // Calcul des VaR individuelles
            for (int j = 0; j < Actions.Colonnes; j++)
            {
                for (int i = 1; i < Actions.Lignes; i++)
                {
                    rendements[i - 1,j] = (Actions[i, j] - Actions[i - 1, j]) / Actions[i - 1, j];
                }

                List<double> rendement_tri = new List<double>();

                for (int i = 0; i < Actions.Lignes-1; i++)
                {
                    rendement_tri.Add(rendements[i, j]);
                }

                rendement_tri.Sort();
               
                VaR_actif[j]= rendement_tri[position - 1]; // On récupére la VaR de l'actif seul; -1 car le tableau est indexé à 0
                Console.WriteLine("VaR actif " + j +" : "+ VaR_actif[j]);
                VaR_result += VaR_actif[j]* VaR_actif[j]*CompanyTab[j].Weight* CompanyTab[j].Weight;

            };
 

            // Calcul des corrélations entre les actifs
            for (int j = 0; j < Actions.Colonnes; j++)
            {
                for (int k = j+1; k < Actions.Colonnes; k++)
                {   
                    // Corrélation entre l'entreprise j et l'entreprise k
                   VaR_result +=2*Statistique.Correlation(rendements, j, k)* VaR_actif[j] * CompanyTab[j].Weight*VaR_actif[k] * CompanyTab[k].Weight;
                }

            }
     
            return -Math.Sqrt(VaR_result);

        }
    }
}
