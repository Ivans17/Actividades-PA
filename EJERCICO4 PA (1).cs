using System;

class JacobiAutovalores
{
    static void Main()
    {
       
        double[,] matriz = {
            { 4, -2, 2 },
            { -2, 6, -1 },
            { 2, -1, 5 }
        };

        int maxIteraciones = 100; 
        double toleranciaConvergencia = 1e-10; 

        double[] autovalores = CalcularAutovaloresJacobi(matriz, maxIteraciones, toleranciaConvergencia);

        Console.WriteLine("Autovalores aproximados:");
        foreach (var autovalor in autovalores)
        {
            Console.WriteLine(autovalor);
        }
    }

    static double[] CalcularAutovaloresJacobi(double[,] matriz, int maxIteraciones, double toleranciaConvergencia)
    {
        int tamaño = matriz.GetLength(0);
        double[,] matrizTransformada = (double[,])matriz.Clone();
        double[] autovalores = new double[tamaño];

        for (int iteracion = 0; iteracion < maxIteraciones; iteracion++)
        {
            int filaIndice = 0, columnaIndice = 1;
            double maxFueraDiagonal = 0.0;

          
            for (int i = 0; i < tamaño; i++)
            {
                for (int j = i + 1; j < tamaño; j++)
                {
                    if (Math.Abs(matrizTransformada[i, j]) > maxFueraDiagonal)
                    {
                        maxFueraDiagonal = Math.Abs(matrizTransformada[i, j]);
                        filaIndice = i;
                        columnaIndice = j;
                    }
                }
            }

            if (maxFueraDiagonal < toleranciaConvergencia)
                break;

            double diferencia = matrizTransformada[columnaIndice, columnaIndice] - matrizTransformada[filaIndice, filaIndice];
            double theta = 0.5 * Math.Atan2(2 * matrizTransformada[filaIndice, columnaIndice], diferencia);
            double coseno = Math.Cos(theta);
            double seno = Math.Sin(theta);


            for (int i = 0; i < tamaño; i++)
            {
                if (i != filaIndice && i != columnaIndice)
                {
                    double tempFila = matrizTransformada[i, filaIndice];
                    double tempColumna = matrizTransformada[i, columnaIndice];
                    matrizTransformada[i, filaIndice] = matrizTransformada[filaIndice, i] = coseno * tempFila - seno * tempColumna;
                    matrizTransformada[i, columnaIndice] = matrizTransformada[columnaIndice, i] = seno * tempFila + coseno * tempColumna;
                }
            }

            double diagFila = matrizTransformada[filaIndice, filaIndice];
            double diagColumna = matrizTransformada[columnaIndice, columnaIndice];
            double fueraDiagonal = matrizTransformada[filaIndice, columnaIndice];

            matrizTransformada[filaIndice, filaIndice] = coseno * coseno * diagFila - 2 * seno * coseno * fueraDiagonal + seno * seno * diagColumna;
            matrizTransformada[columnaIndice, columnaIndice] = seno * seno * diagFila + 2 * seno * coseno * fueraDiagonal + coseno * coseno * diagColumna;
            matrizTransformada[filaIndice, columnaIndice] = matrizTransformada[columnaIndice, filaIndice] = 0;
        }

        for (int i = 0; i < tamaño; i++)
            autovalores[i] = matrizTransformada[i, i];

        return autovalores;
    }
}

