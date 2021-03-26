using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaR
{
    class VarParametrique : CalculVar
    {
        public VarParametrique(DataProvider dp, double alpha = 0.01) : base(dp, alpha)
        {
        }


        public override Tuple<double, double> Calcul()
        {
            double VaR_result = 0;
            double Cvar_result = 0;
            int position = 0;
            if (Math.Floor(alpha * Actions.Lignes) == alpha * Actions.Lignes)
            {
                position = (int)(alpha * Actions.Lignes);
            }
            else
            {
                position = (int)Math.Floor(alpha * Actions.Lignes) + 1;
            }

            rendements = new Matrice(Actions.Lignes - 1, Actions.Colonnes);

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



            double[] VaR_actif = new double[Actions.Colonnes];
            double[] Cvar_actif = new double[Actions.Colonnes];
            for (int j = 0; j < Actions.Colonnes; j++)
            {

                VaR_actif[j] = Statistique.NormInv(Er[j,0], Vol[j,0], alpha);
                Cvar_actif[j] = Statistique.Moyenne(rendements,j)-(1/alpha)*(1/Math.Sqrt(Math.PI))*Math.Exp(-0.5*Statistique.NormInv(0,1,alpha)* Statistique.NormInv(0, 1, alpha))*Statistique.EcartType(rendements, j);
                VaR_result += VaR_actif[j] * VaR_actif[j] * CompanyTab[j].Weight * CompanyTab[j].Weight;
                Cvar_result += Cvar_actif[j] * Cvar_actif[j] * CompanyTab[j].Weight * CompanyTab[j].Weight;
            }

            // Calcul des corrélations entre les actifs
            for (int j = 0; j < Actions.Colonnes; j++)
            {
                for (int k = j + 1; k < Actions.Colonnes; k++)
                {
                    // Corrélation entre l'entreprise j et l'entreprise k
                    VaR_result += 2 * Statistique.Correlation(rendements, j, k) * VaR_actif[j] * CompanyTab[j].Weight * VaR_actif[k] * CompanyTab[k].Weight;
                    Cvar_result += 2 * Statistique.Correlation(rendements, j, k) * Cvar_actif[j] * CompanyTab[j].Weight * Cvar_actif[k] * CompanyTab[k].Weight;
                }

            }



            return new Tuple <double,double>  (-Math.Sqrt(VaR_result),-Math.Sqrt(Cvar_result));
        }
    }
}
