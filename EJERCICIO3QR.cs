using System.Linq;

using System;

class QRDecomposition
{
    public static void GramSchmidt(double[,] A, out double[,] Q, out double[,] R)
    {
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        Q = new double[rows, cols];
        R = new double[cols, cols];

        for (int j = 0; j < cols; j++)
        {
            double[] v = new double[rows];
            for (int i = 0; i < rows; i++)
                v[i] = A[i, j];

            for (int k = 0; k < j; k++)
            {
                double dot = 0;
                for (int i = 0; i < rows; i++)
                    dot += Q[i, k] * A[i, j];

                R[k, j] = dot;
                for (int i = 0; i < rows; i++)
                    v[i] -= dot * Q[i, k];
            }

            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm < 1e-10) throw new Exception("Matriz con columnas linealmente dependientes");

            R[j, j] = norm;
            for (int i = 0; i < rows; i++)
                Q[i, j] = v[i] / norm;
        }
    }

    public static void PrintMatrix(double[,] matrix, string name)
    {
        Console.WriteLine($"Matriz {name}:");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                Console.Write($"{matrix[i, j]:F4} ");
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void Main()
    {
        double[,] A = {
            { 1, 1, 0 },
            { 1, 0, 1 },
            { 0, 1, 1 }
        };

        try
        {
            GramSchmidt(A, out double[,] Q, out double[,] R);
            PrintMatrix(Q, "Q");
            PrintMatrix(R, "R");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
