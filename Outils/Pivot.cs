using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public class Pivot : Decomposition
    {
        public Pivot(Matrice A) : base(A)
        {
        }

        public override void Decomposer()
        {
            Console.WriteLine("A = \n" + A);
            int n = _A.Lignes;
            _L = new Matrice(n,n);
            _U = new Matrice(n,n);
            Console.WriteLine("L = \n" + _L);
            Console.WriteLine("U = \n" + _U);

            Matrice PA = Pivoter(_A);
            Console.WriteLine("PA = \n" + PA);

            for (int j = 0; j < n; j++)
            {
                _L[j, j] = 1;

                for (int i = 0; i < j+1; i++)
                {
                    double x = 0;

                    for (int k = 0; k < i; k++)
                    {
                        x += _U[k, j] * _L[i, k];
                    }

                    _U[i, j] = PA[i, j] - x;
                }

                for (int i = j+1; i < n; i++)
                {
                    double x = 0;

                    for (int k = 0; k < j; k++)
                    {
                        x += _U[k, j] * _L[i, k];
                    }

                    _L[i, j] = (PA[i, j] - x)/_U[j,j];
                }
            }
        }

        private Matrice Pivoter(Matrice A)
        {
            Matrice Id = new Matrice(A.Lignes, A.Lignes);
            for (int i = 0; i < A.Lignes; i++)
            {
                Id[i, i] = 1;
            }

            for (int i = 0; i < A.Lignes; i++)
            {
                double max = A[i, i]; 
                int ligne=i;

                for (int j = i; j < A.Colonnes; j++)
                {
                    if (A[j,i]>max)
                    {
                        max = A[j, i];
                        ligne = j;
                    }

                    if (i != ligne)
                    {
                        for (int k = 0; k < Id.Lignes; k++)
                        {
                            double tmp = Id[i, k];
                            Id[i, k] = Id[ligne, k];
                            Id[ligne, k] = tmp;
                        }   
                    }
                }
                

            }

            return Id;
        }
    }
}
