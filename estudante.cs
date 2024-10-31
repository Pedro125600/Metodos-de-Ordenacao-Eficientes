using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace att2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool rep = true;
            while (rep)
            {
                Console.WriteLine("Escolha o tipo de ordenação:");
                Console.WriteLine("1 - Mergesort");
                Console.WriteLine("2 - Quicksort");
                Console.WriteLine("3 - Heapsort");
                int tipoOrdenacao = int.Parse(Console.ReadLine());
                string TipoOrdenação = "";
                if (tipoOrdenacao == 1)
                    TipoOrdenação = "Mergesort";
                else if (tipoOrdenacao == 2)
                    TipoOrdenação = "Quicksort";
                else if (tipoOrdenacao == 3)
                    TipoOrdenação = "Heapsort";

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


                Estudante[] Original = new Estudante[tamanho];
                Estudante[] Copia = new Estudante[tamanho];
                long[] temp = new long[10];
                long numMovimentacoes = 0;
                long numComparacoes = 0;

                PreencherVetor(Original, tipoPreenchimento);

                for (int j = 0; j < Original.Length; j++)
                {
                    Copia[j] = Original[j];
                }

                Stopwatch stopwatch = new Stopwatch();

                for (int i = 0; i < temp.Length; i++)
                {
                    long tempMov = 0;
                    long tempComp = 0;

                    if (tipoOrdenacao == 1)
                    {
                        stopwatch.Restart();
                        MergeSort(Copia, 0, tamanho - 1, ref tempMov, ref tempComp);
                        stopwatch.Stop();
                    }
                    else if (tipoOrdenacao == 2)
                    {
                        stopwatch.Restart();
                        Quicksort(Copia, 0, tamanho - 1, ref tempMov, ref tempComp);
                        stopwatch.Stop();
                    }
                    else if (tipoOrdenacao == 3)
                    {
                        stopwatch.Restart();
                        Heapsort(Copia, tamanho - 1, ref tempMov, ref tempComp);
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

                Console.WriteLine($"Tipo de ordenação: {TipoOrdenação}");
                Console.WriteLine($"Tamanho do vetor: {tamanho}");
                Console.WriteLine($"Tipo de preenchimento: {TipoPreenchimento}");
                Console.WriteLine($"Tempo médio gasto: {mediaTempo} ms");
                Console.WriteLine($"Número de movimentações: {numMovimentacoes}");
                Console.WriteLine($"Número de comparações: {numComparacoes}");
                Console.ReadLine();

                Console.WriteLine("Sair aperte 1");
                string resp = Console.ReadLine();
                if (resp == "1")
                    rep = false;

                Console.Clear();

            }
        }

        static void PreencherVetor(Estudante[] array, int tipoPreenchimento)
        {
            Random random = new Random();

            switch (tipoPreenchimento)
            {
                case 1:
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = GerarEstudanteAleatorio();
                    }
                    break;
                case 2:
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = new Estudante(i, GerarStringAleatoria(6), GerarStringAleatoria(8), random.Next(0, 101), i);
                    }
                    break;
                case 3:
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = new Estudante(array.Length - i, GerarStringAleatoria(6), GerarStringAleatoria(8), random.Next(0, 101), array.Length - i);
                    }
                    break;
                default:
                    Console.WriteLine("Tipo de preenchimento inválido!");
                    break;
            }
        }

        static Estudante GerarEstudanteAleatorio()
        {
            Random random = new Random();
            return new Estudante(random.Next(1000, 9999), GerarStringAleatoria(6), GerarStringAleatoria(8), random.Next(0, 101), random.Next(1, 11));
        }

        static string GerarStringAleatoria(int tamanho)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            char[] stringAleatoria = new char[tamanho];
            for (int i = 0; i < tamanho; i++)
            {
                stringAleatoria[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringAleatoria);
        }



        class Estudante
        {
            public int Matricula { get; set; }
            public string Nome { get; set; }
            public string Curso { get; set; }
            public int CoeficienteRendimento { get; set; }
            public int PeriodoAtual { get; set; }

            public Estudante(int matricula, string nome, string curso, int coeficienteRendimento, int periodoAtual)
            {
                Matricula = matricula;
                Nome = nome;
                Curso = curso;
                CoeficienteRendimento = coeficienteRendimento;
                PeriodoAtual = periodoAtual;
            }

        }




        static void MergeSort(Estudante[] v, int esq, int dir, ref long tempMov, ref long tempComp)
        {
            if (esq < dir)
            {


                int meio = (esq + dir) / 2;
                MergeSort(v, esq, meio, ref tempMov, ref tempComp);
                MergeSort(v, meio + 1, dir, ref tempMov, ref tempComp);
                Intercala(v, esq, meio, dir, ref tempMov, ref tempComp);
            }
        }


        static void Intercala(Estudante[] v, int esq, int meio, int dir, ref long tempMov, ref long tempComp)
        {
            int n1 = meio - esq + 1;
            int n2 = dir - meio;
            Estudante[] v_esq = new Estudante[n1];
            Estudante[] v_dir = new Estudante[n2];
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
                if (v_esq[cont1].Matricula <= v_dir[cont2].Matricula)
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
                tempComp += 3;
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

        static void Quicksort(Estudante[] array, int esq, int dir, ref long tempMov, ref long tempComp)
        {
            int i = esq, j = dir, pivo = array[(esq + dir) / 2].Matricula;
            while (i <= j)
            {
                while (array[i].Matricula < pivo) { i++; tempComp++; }

                while (array[j].Matricula > pivo) { j--; tempComp++; }

                if (i <= j)
                { Trocar(array, i, j, ref tempMov, ref tempComp); i++; j--; tempComp++; }
                tempComp += 3;
            }
            if (esq < j)
                Quicksort(array, esq, j, ref tempMov, ref tempComp);

            if (i < dir)
                Quicksort(array, i, dir, ref tempMov, ref tempComp);

            tempComp += 2;
        }

        static void Trocar(Estudante[] array, int i, int j, ref long tempMov, ref long tempComp)
        {
            int temp = array[i].Matricula;
            array[i].Matricula = array[j].Matricula;
            array[j].Matricula = temp;
            tempMov += 3;
        }

        static void Reconstruir(Estudante[] array, int tam, ref long tempMov, ref long tempComp)
        {
            int i = 1;
            while (HasFilho(i, tam, ref tempComp) == true)
            {
                int filho = GetMaiorFilho(array, i, tam, ref tempComp);
                if (array[i].Matricula < array[filho].Matricula)
                {
                    Trocar(array, i, filho, ref tempMov, ref tempComp);
                    i = filho;
                }
                else
                {
                    i = tam;

                }

                tempComp++;
            }
        }

        static bool HasFilho(Estudante[] i, int tam, ref long tempcomp)
        {
            tempcomp++;
            return (i.Length <= (tam / 2));
        }

        static bool HasFilho(int i, int tam, ref long tempcomp)
        {
            tempcomp++;
            return (i <= (tam / 2));
        }

        static int GetMaiorFilho(Estudante[] array, int i, int tam, ref long tempcomp)
        {
            int filho;
            if (2 * i == tam || array[2 * i].Matricula > array[2 * i + 1].Matricula)
            {
                filho = 2 * i;
            }
            else
            {
                filho = 2 * i + 1;
            }
            tempcomp += 2;
            return filho;
        }

        static void Heapsort(Estudante[] array, int n, ref long tempMov, ref long tempComp)
        {
            int tam;
            for (tam = 2; tam <= n; tam++)
            {
                Construir(array, tam, ref tempMov, ref tempComp);
            }

            tam = n;
            while (tam > 1)
            {
                Trocar(array, 1, tam--, ref tempMov, ref tempComp);
                Reconstruir(array, tam, ref tempMov, ref tempComp);
                tempComp++;
            }
        }


        static void Construir(Estudante[] array, int tam, ref long tempMov, ref long tempComp)
        {
            for (int i = tam; i > 1 && array[i].Matricula > array[i / 2].Matricula; i /= 2)
            {
                Trocar(array, i, i / 2, ref tempMov, ref tempComp);
            }
        }


    }




}

