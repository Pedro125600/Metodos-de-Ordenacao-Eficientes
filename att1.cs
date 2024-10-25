
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Escolha o tipo de ordenação:");
        Console.WriteLine("1 - Mergesort");
        Console.WriteLine("2 - Inserção");
        Console.WriteLine("3 - Seleção");
        int tipoOrdenacao = int.Parse(Console.ReadLine());
        string TipoOrdenação = "";
        if (tipoOrdenacao == 1)
            TipoOrdenação = "Mergesort";
        else if (tipoOrdenacao == 2)
            TipoOrdenação = "Inserção";
        else if (tipoOrdenacao == 3)
            TipoOrdenação = "Seleção";

        Console.WriteLine("Escolha o tamanho do vetor:");
        int tamanho = int.Parse(Console.ReadLine());

        Console.WriteLine("Escolha como o vetor será preenchido:");
        Console.WriteLine("1 - Aleatório");
        Console.WriteLine("2 - Ordenado");
        Console.WriteLine("3 - Reversamente Ordenado");
        int tipoPreenchimento = int.Parse(Console.ReadLine());
        string TipoPreenchimento = "";
        if (tipoPreenchimento == 1)
            TipoPreenchimento = "Aleatorio";
        else if (tipoPreenchimento == 2)
            TipoPreenchimento = "ordenação";
        else if (tipoPreenchimento == 3)
            TipoPreenchimento = "Reversamente Ordenado";


        int[] Original = new int[tamanho];
        int[] Copia = new int[tamanho];
        long[] temp = new long[10];
        long numMovimentacoes = 0;
        long numComparacoes = 0;

        PreencherVetor(Original, tipoPreenchimento);

        for (int j = 0; j < Original.Length; j++)
        {
            Copia[j] = Original[j];
        }

      


        Random random = new Random();
        Stopwatch stopwatch = new Stopwatch();

        for (int i = 0; i < temp.Length; i++)
        {
            long tempMov = 0;
            long tempComp = 0;



            if (tipoOrdenacao == 1)
            {
                stopwatch.Restart();
                MergeSort(Copia, 0, tamanho-1, ref tempMov, ref tempComp);
                stopwatch.Stop();
            }
            else if (tipoOrdenacao == 2)
            {
                stopwatch.Restart();
                Insercao(Copia, ref tempMov, ref tempComp);
                stopwatch.Stop();
            }
            else if (tipoOrdenacao == 3)
            {
                stopwatch.Restart();
               
                stopwatch.Stop();
            }




            temp[i] = stopwatch.ElapsedMilliseconds;

            if (numComparacoes < tempComp)
            {
                numComparacoes = tempComp;
            }

            if (numMovimentacoes < tempMov)
            {
                numMovimentacoes = tempMov;
            }

            for (int j = 0; j < Original.Length; j++)
            {
                Copia[j] = Original[j];
            }
        }

        long totalTempo = 0;
        for (int i = 0; i < temp.Length; i++)
        {
            totalTempo += temp[i];
        }

        long mediaTempo = totalTempo / temp.Length;




        Console.WriteLine();
        Console.WriteLine($"Tipo de ordenação: {TipoOrdenação}");
        Console.WriteLine($"Tamanho do vetor: {tamanho}");
        Console.WriteLine($"Tipo de preenchimento: {TipoPreenchimento}");
        Console.WriteLine($"Tempo médio gasto: {mediaTempo} ms");
        Console.WriteLine($"Número de movimentações: {numMovimentacoes}");
        Console.WriteLine($"Número de comparações: {numComparacoes}");
        Console.ReadLine();

       
    

    }

    static void PreencherVetor(int[] array, int tipoPreenchimento)
    {
        Random random = new Random();

        switch (tipoPreenchimento)
        {
            case 1:
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = random.Next(0, 101);
                }
                break;
            case 2:
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = i;
                }
                break;
            case 3:
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array.Length - i;
                }
                break;
            default:
                Console.WriteLine("Tipo de preenchimento inválido!");
                break;
        }
    }

    static void MergeSort(int[] v, int esq, int dir, ref long tempMov, ref long tempComp)
    {
        if (esq < dir)
        {
           

            int meio = (esq + dir) / 2;
            MergeSort(v, esq, meio,ref tempMov,ref tempComp);
            MergeSort(v, meio + 1, dir, ref tempMov, ref tempComp);
            Intercala(v, esq, meio, dir, ref tempMov, ref tempComp);
        }
    }

    static void Insercao(int[] array, ref long mov, ref long comp)
    {
        int n = array.Length;

        for (int i = 1; i < n; i++)
        {
            int tmp = array[i];
            int j = i - 1;
            while ((j >= 0) && (array[j] > tmp))
            {
                array[j + 1] = array[j];
                j--;
                mov++;
            }
            array[j + 1] = tmp;
            mov += 2;
            comp += 2;

        }
    }

    static void Intercala(int[] v, int esq, int meio, int dir, ref long tempMov, ref long tempComp)
    {
        int n1 = meio - esq + 1;
        int n2 = dir - meio;
        int[] v_esq = new int[n1];
        int[] v_dir = new int[n2];
        tempMov += 2;
        for (int i = 0; i < n1; i++)
            v_esq[i] = v[esq + i];
        tempComp++;
        for (int j = 0; j < n2; j++)
            v_dir[j] = v[meio + 1 + j];
        int cont1 = 0, cont2 = 0;
        int k = esq;
        tempMov += 3;
        while (cont1 < n1 && cont2 < n2)
        {
            if (v_esq[cont1] <= v_dir[cont2])
            {
                v[k] = v_esq[cont1];
                cont1++;
              
            }
            else
            {
                v[k] = v_dir[cont2];
                cont2++;
            }
            k++;
            tempMov++;
            tempComp+=3;
        }
        while (cont1 < n1)
        {
            v[k] = v_esq[cont1];
            cont1++;
            k++;
            tempMov++;
            tempComp++;
        }
        while (cont2 < n2)
        {
            v[k] = v_dir[cont2];
            cont2++;
            k++;
            tempMov++;
            tempComp++;
        }
    }
}
