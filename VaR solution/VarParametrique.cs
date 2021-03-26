using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaR_solution
{
    class VarParametrique : CalculVar
    {
        public VarParametrique(DataProvider dp, double alpha = 0.01) : base(dp, alpha)
        {
        }


        public override double Calcul()
        {
            int position = 0;
            if (Math.Floor(alpha * Actions.Lignes) == alpha * Actions.Lignes)
            {
                position = (int)(alpha * Actions.Lignes);
            }
            else
            {
                position = (int)Math.Floor(alpha * Actions.Lignes) + 1;
            }

            Matrice rendements = new Matrice(Actions.Lignes - 1, Actions.Colonnes);

            Matrice Er = new Matrice(Actions.Lignes);
            Matrice Vol = new Matrice(Actions.Lignes);
            Matrice Corr = new Matrice(Actions.Colonnes, Actions.Colonnes);

            for (int j = 0; j < Actions.Colonnes; j++)
            {
                for (int i = 1; i < Actions.Lignes; i++)
                {
                    rendements[i - 1, j] = (Actions[i, j] - Actions[i - 1, j]) / Actions[i - 1, j];
                }

                Er[j, 0] = Statistique.Moyenne(rendements, j);
                Vol[j, 0] = Statistique.EcartType(rendements, j);

            }

            // Calcul de la partie supérieure de la matrice de corrélation
            for (int j = 0; j < Actions.Colonnes; j++)
            {
                for (int k = j + 1; k < Actions.Colonnes; k++)
                {
                    // Corrélation entre l'entreprise j et l'entreprise k
                    Corr[j, k] = Statistique.Correlation(rendements, j, k);
                }

            }

            // Génération de la partie infèrieure par symétrie
            for (int j = 0; j < Actions.Colonnes; j++)
            {
                for (int k = j + 1; k < Actions.Colonnes; k++)
                {
                    Corr[k, j] = Corr[j, k];
                }

            }

            // Remplissage de la diagonale avec la valeur 1
            for (int j = 0; j < Actions.Colonnes; j++)
            {
                Corr[j, j] = 1;
            }



            Statistique.NormInv(0, 1, p);

            return -Math.Sqrt(VaR_result);
        }
    }
}
