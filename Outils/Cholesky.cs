using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public class Cholesky : Decomposition
    {
        public Cholesky(Matrice A) : base(A)
        {

        }

        public override void Decomposer()
        {
            if (A.Lignes == A.Colonnes)
            {
                _L = new Matrice(_A.Lignes, _A.Lignes);
                for (int j = 0; j < A.Colonnes; j++)
                {

                    double s = 0;
                    for (int k = 0; k <=j-1; k++)
                    {   
                        s +=( _L[j, k]* _L[j, k]);
                    }

                    _L[j, j] = _A[j, j] - s;
                   
                    if (_L[j,j]<=0 )
                    {
                        break;
                    }

                    _L[j, j] = Math.Sqrt(_L[j, j]);

                    for (int i = j+1; i < _A.Lignes; i++)
                    {
                        s = 0;
                        for (int k = 0; i <= j-1; k++)
                        {
                            s += (_L[i, k] * _L[j, k]);
                        }
                        _L[i, j] = (_A[i, j] - s) / _L[j, j];   
                    }

                }

                for (int i = 0; i < _L.Lignes; i++)
                {
                    for (int j = i+1; j < _A.Lignes; j++)
                    {
                        _L[i, j] = 0;
                    }

                }

                _U = _L.Transposition();
            }
            else 
            {
                throw new Exception("Matrice not squared");
            }
        }
    }
}
