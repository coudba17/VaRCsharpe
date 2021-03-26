using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meta.Numerics.Statistics.Distributions;

namespace Outils
{
    public class Statistique
    {
        // Calcul de l'espérance de la colonne j d'une matrice X
        public static double Moyenne(Matrice X, int j0)
        {
            double moy_X = 0;
            double x = 0;

            for (int i = 0; i < X.Lignes; i++)
            {
                x = X[i, j0];
                moy_X += x;

            }

            moy_X /= X.Lignes;


            return moy_X;
        }

        // Calcul de la variance de la colonne j d'une matrice X
        public static double Variance(Matrice X,int j0)
        {
            double moy_X = 0;
            double moy_X2 = 0;
            double x = 0;

            for (int i = 0; i < X.Lignes; i++)
            {
                x = X[i, j0];
                moy_X += x;
                moy_X2 += x*x;
            }

            moy_X /= X.Lignes;
            moy_X2 /= X.Lignes;

            return moy_X2 - moy_X*moy_X;
        }

        // Calcul de l'écart-type de la colonne j d'une matrice X
        public static double EcartType(Matrice X, int j0)
        {
            
            return Math.Sqrt(Variance(X,j0));
        }

        // Calcul de la corrélation entre deux colonnes
        public static double Correlation(Matrice rendements, int j, int k)
        {
            double moyenne_j = 0;
            double moyenne_k = 0;

            for (int i = 0; i < rendements.Lignes; i++)
            {
                moyenne_j += rendements[i, j];
                moyenne_k += rendements[i, k];

            }

            moyenne_j /= rendements.Lignes;
            moyenne_k /= rendements.Lignes;


            double ecart_jk = 0;
            double variance_jk = 0;

            double variance_j = 0;
            double variance_k = 0;

            for (int i = 0; i < rendements.Lignes; i++)
            {
                double diff_j = rendements[i, j] - moyenne_j;
                double diff_k = rendements[i, k] - moyenne_k;

                ecart_jk += diff_j * diff_k;

                variance_j += diff_j * diff_j;
                variance_k += diff_k * diff_k;
            }

            variance_jk = Math.Sqrt(variance_j * variance_k);

            return ecart_jk / variance_jk;
        }

        public static double NormInv(double Er,double EcartType,double proba)
        {
            NormalDistribution normalDistribution = new NormalDistribution(Er,EcartType);

            return normalDistribution.InverseLeftProbability(proba);
        }

    }
}
