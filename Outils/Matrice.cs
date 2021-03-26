using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public class Matrice : IIndexable
    {
        // Attributs
        private int lignes;
        private int colonnes;
        private double[,] tableau;

        // Constructeurs
        public Matrice(double[,] tableau)
        {
            this.lignes = (int)tableau.GetLongLength(0);
            this.colonnes = (int)tableau.GetLongLength(1);
            this.tableau = tableau;
        }

        public Matrice(int lignes, int colonnes)
        {
            this.lignes = lignes;
            this.colonnes = colonnes;
            tableau = new double[lignes, colonnes];

        }

        public Matrice(int lignes)
        {
            this.lignes = lignes;
            this.colonnes = 1;
            tableau = new double[lignes, colonnes];

        }
        // Méthodes
        public void SetSafe(int i, int j, double valeur)
        {
            if (i >= 0 && i < lignes && j >= 0 && j < colonnes)
            {
                tableau[i, j] = valeur;
            }

        }

        public void SetUnsafe(int i, int j, double valeur)
        {
            tableau[i, j] = valeur;


        }


        public override string ToString()
        {
            string txt = "";

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    txt += tableau[i, j] + " "; // Il détecte le double et le convertit implicitement en string

                }
                txt += "\n"; // retourne à la ligne
            }

            return txt;
        }

        public double GetUnsafe(int i, int j)
        {
            return tableau[i, j];
        }

        public double GetSafe(int i, int j)
        {
            if (i >= 0 && i < lignes && j >= 0 && j < colonnes)
            {
                return tableau[i, j];
            }
            else
            {
                throw new Exception("Index out of range");
            }
        }

        public Matrice Add(Matrice B)
        {

            if (this.lignes == B.lignes && this.colonnes == B.colonnes)
            {
                Matrice C = new Matrice(lignes, colonnes);

                for (int i = 0; i < lignes; i++)
                {
                    for (int j = 0; j < colonnes; j++)
                    {
                        C.tableau[i, j] = tableau[i, j] + B.tableau[i, j];

                    }

                }
                return C;
            }
            else
            {
                throw new Exception("Les matrices non pas les mêmes dimensions");
            }



        }

        public Matrice Substract(Matrice B)
        {

            if (this.lignes == B.lignes && this.colonnes == B.colonnes)
            {
                Matrice C = new Matrice(lignes, colonnes);

                for (int i = 0; i < lignes; i++)
                {
                    for (int j = 0; j < colonnes; j++)
                    {
                        C.tableau[i, j] = tableau[i, j] - B.tableau[i, j];

                    }

                }
                return C;
            }
            else
            {
                throw new Exception("Les matrices non pas les mêmes dimensions");
            }

        }


        public Matrice Mult(Matrice B)
        {
            if (this.colonnes == B.lignes)
            {
                Matrice C = new Matrice(this.lignes, B.colonnes);

                for (int i = 0; i < C.lignes; i++)
                {
                    for (int j = 0; j < C.colonnes; j++)
                    {
                        double res = 0;

                        for (int k = 0; k < colonnes; k++)
                        {
                            res += tableau[i, k] * B.tableau[k, j];
                        }
                        C.tableau[i, j] = res;
                    }
                }

                return C;
            }
            throw new Exception("Dimensions incorrectes pour la multiplication");

        }


        public Matrice Transposition()
        {
            Matrice transpose = new Matrice(colonnes, lignes);

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    transpose[j, i] = tableau[i, j];
                }
            }
            return transpose;
        }

        public void ScalarMult(double lambda)
        {
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    tableau[i, j] *= lambda;
                }
            }
        }


        public Matrice Inverse()
        {
            if (lignes == colonnes)
            {
                double det = this.DeterminantRecur();

                if (det != 0)
                {
                    Matrice inverse = this.Comatrice().Transposition();
                    inverse.ScalarMult(1.0 / det);
                    return inverse;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                throw new Exception("Dimensions incorrectes pour la multiplication");
            }

        }

        public Matrice Comatrice()
        {
            if (lignes != colonnes)
            {
                throw new Exception("Dimension error");
            }

            Matrice comat = new Matrice(lignes, colonnes);

            if (lignes == 1)
            {
                comat[0, 0] = tableau[0, 0];
                return comat;
            }



            double signeL = 1;
            for (int i = 0; i < lignes; i++)
            {
                double signeC = signeL; // changement de signe de ligne ne dépend pas de changemetn de colonne
                for (int j = 0; j < colonnes; j++)
                {
                    Matrice sousMat = GetDimInf(i, j);
                    comat[i, j] = signeC * sousMat.DeterminantRecur();
                    signeC = -signeC;
                }
                signeL = -signeL;
            }


            return comat;
        }

        public double DeterminantRecur()
        {
            if (lignes != colonnes)
            {
                throw new Exception("Dimension error");
            }

            if (lignes == 1)
            {
                return tableau[0, 0];
            }

            if (lignes == 2)
            {
                return tableau[0, 0] * tableau[1, 1] - tableau[0, 1] * tableau[1, 0];
            }

            double deter = 0;
            double signe = 1;
            for (int j = 0; j < colonnes; j++)
            {
                Matrice sousMat = GetDimInf(0, j);
                deter += signe * tableau[0, j] * sousMat.DeterminantRecur();
                signe = -signe;
            }

            return deter;

        }

        private Matrice GetDimInf(int i0, int j0)
        {
            if (lignes > 1 && colonnes > 1)
            {
                int i1 = 0;
                int j1 = 0;
                Matrice mat = new Matrice(lignes - 1, colonnes - 1);
                for (int i = 0; i < lignes; i++)
                {
                    if (i != i0)
                    {
                        j1 = 0;
                        for (int j = 0; j < colonnes; j++)
                        {
                            if (j != j0)
                            {
                                mat[i1, j1] = tableau[i, j]; // Je copie tout sauf la ligne i0 et colonne j0
                                j1++;
                            }
                        }
                        i1++;
                    }

                }
                return mat;
            }
            else
            {
                return null;
            }
        }

        public Matrice[] Decomposer()
        {
            Matrice[] LU = new Matrice[2];

            Matrice L = new Matrice(lignes, colonnes);
            Matrice U = new Matrice(lignes, colonnes);

            LU[0] = L;
            LU[1] = U;

            // Boucle sur la diagonale de L
            for (int i = 0; i < lignes; i++)
            {
                L[i, i] = 1;
            }

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    if (j >= i)
                    {


                        double u = tableau[i, j];

                        for (int k = 0; k < i - 1; k++)
                        {
                            u -= U[k, j] * L[i, k];
                        }

                        U[i, j] = u;


                    }

                    if (j > i)

                    {
                        double l = tableau[j, i];

                        for (int k = 0; k < i - 1; k++)
                        {
                            l -= U[k, i] * L[j, k];
                        }

                        if (U[i, i] == 0)
                        {
                            Console.WriteLine("Division par 0");
                        }
                        l /= U[i, i];
                        L[j, i] = l;
                    }


                }
            }

            return LU;
        }

        public Matrice Permutation()
        {
            Matrice P = new Matrice(lignes, colonnes);
            int[] ancienMax = new int[colonnes];
            int indice = 0;

            for (int j = 0; j < colonnes; j++)
            {
                ancienMax[j] = -1; // -1 pour dire que la case est vide
            }

            for (int j = 0; j < colonnes; j++)
            {


                int ligneMax = getMaxSurColonne(j, ancienMax);
                ancienMax[indice] = ligneMax;
                indice++;
                P.tableau[ligneMax, j] = 1;
            }
            return P;
        }

        private static bool Contient(int[] tab, int i) // renvoit un boolean, static car ne dépend pas d'un attribut 
        {
            for (int k = 0; k < tab.Length; k++)
            {
                if (tab[k] == i)
                {
                    return true;
                }
            }

            return false;
        }

        private int getMaxSurColonne(int j, int[] ancienMax) // position des anciens Max(ligne)
        {
            int ligneMax = 0;
            double max = 0;
            for (int i = 0; i < lignes; i++)
            {
                if (!Contient(ancienMax, i)) //: if not, on aurait pu mettre aussi Contient == False
                {
                    max = tableau[i, j];
                    ligneMax = i;
                    break;
                }
            }

            for (int i = 0; i < lignes; i++)
            {
                if (!Contient(ancienMax, i)) //: if not, on aurait pu mettre aussi Contient == False
                {
                    if (tableau[i, j] > max)
                    {
                        max = tableau[i, j];
                        ligneMax = i;
                    }

                }

            }
            return ligneMax;
        }

        // Propriétés
        public int Lignes
        {
            get { return lignes; }
        }

        public int Colonnes
        {
            get { return colonnes; }
        }

        // Implementation de l'interface
        public double this[int i, int j] // C'est un genre d'override, les interfaces = abstract
        {
            get
            {
                return GetUnsafe(i, j);
            }

            set
            {
                SetUnsafe(i, j, value);
            }
        }
    }
}
