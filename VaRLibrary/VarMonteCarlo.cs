using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaRLib
{
    public class VarMonteCarlo : CalculVar
    {
        public VarMonteCarlo(DataProvider dp, double alpha = 0.01) : base(dp, alpha)
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

            // Calcul de la partie supérieure de la matrice de corrélation
            for (int j = 0; j < Actions.Colonnes; j++)
            {
                for (int k = j + 1; k < Actions.Colonnes; k++)
                {
                    // Corrélation entre l'entreprise j et l'entreprise k
                    Corr[j,k]= Statistique.Correlation(rendements, j, k);
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


            // Décomposition de Cholesky
            Decomposition decompo = new Cholesky(Corr);
            decompo.Decomposer();
            Matrice L = decompo.L;


            rendements = Simulation(Er, Vol, decompo, rendements.Lignes);

            double[] VaR_actif = new double[Actions.Colonnes];
            double[] Cvar_actif = new double[Actions.Colonnes];
            // Calcul des VaR individuelles
            for (int j = 0; j < Actions.Colonnes; j++)
            {

                List<double> rendement_tri = new List<double>();

                for (int i = 0; i < Actions.Lignes - 1; i++)
                {
                    rendement_tri.Add(rendements[i, j]);
                }

                rendement_tri.Sort();

                Cvar_actif[j] = CalculCvarFromSorted(rendement_tri);

                VaR_actif[j] = rendement_tri[position - 1]; // On récupére la VaR de l'actif seul; -1 car le tableau est indexé à 0
                // Console.WriteLine("VaR actif " + j + " : " + VaR_actif[j]);
                // Console.WriteLine("CVaR actif " + j + " : " + Cvar_actif[j]);
                VaR_result += VaR_actif[j] * VaR_actif[j] * CompanyTab[j].Weight * CompanyTab[j].Weight;
                Cvar_result += Cvar_actif[j] * Cvar_actif[j] * CompanyTab[j].Weight * CompanyTab[j].Weight;

            };


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

            return new Tuple<double, double> (- Math.Sqrt(VaR_result),-Math.Sqrt(Cvar_result));
        }


        private Matrice Simulation(Matrice Er,Matrice Vol,Decomposition decompo,int observ)
        {
            int n = decompo.L.Colonnes; // nombre de titres
            Matrice Historique = new Matrice(observ, n);

            Random rand = new Random();

            for (int i= 0; i < observ; i++)
            {
                for (int j = 0; j <n ; j++)
                {
                    double p = (double)rand.Next(1, int.MaxValue)/int.MaxValue;
                    Historique[i, j] = Statistique.NormInv(0, 1, p);
                }
            }

            Historique=Historique.Mult(decompo.U);

            for (int i = 0; i < observ; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Historique[i, j] = Er[j, 0] + Historique[i, j] *Vol[j, 0];
                }

            }

            return Historique;
        }

    }
}
