using System;
using System.Globalization;

class Program
{
    struct Eletro
    {
        public string nome;
        public double potencia;
        public double tempoMedioUso;
    }

    static void addEletro(List<Eletro> lista)
    {
        Eletro novoEletro = new Eletro();// declarando uma variavel do TipoBanda
        Console.Write("Nome do eletrodomestico: ");
        novoEletro.nome = Console.ReadLine();
        Console.Write("potencia do dispositivo(Real, em Kw): ");
        novoEletro.potencia = int.Parse(Console.ReadLine());
        Console.Write("Tempo medio de uso: ");
        novoEletro.tempoMedioUso = double.Parse(Console.ReadLine());
        lista.Add(novoEletro);
    }// fim funcao 

    static void listarEletro(List<Eletro> lista)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            Console.WriteLine("\t*** Dados dos eletros ***");
            Console.WriteLine("Nome: " + lista[i].nome);
            Console.WriteLine("Potencia: " + lista[i].potencia);
            Console.WriteLine("Tempo medio de uso: " + lista[i].tempoMedioUso);
        }// fim for
    }// fim lista

    static void buscarNome(List<Eletro> lista, string nomeBusca)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Contains(nomeBusca.ToUpper()))
            {
                Console.WriteLine("\t*** Dados dos eletros ***");
                Console.WriteLine("Nome:" + lista[i].nome);
                Console.WriteLine("Potencia: " + lista[i].potencia);
                Console.WriteLine("Tempo medio de uso: " + lista[i].tempoMedioUso);
                // break;
            }// fim 

        }// fim for
    }

    static void buscaConsumo(List<Eletro> list, double potencia)
    {
        foreach (Eletro eletro in list)
        {
            if (eletro.potencia > potencia)
            {
                Console.WriteLine($"{eletro.nome}\n{eletro.potencia}\n{eletro.tempoMedioUso}\n");
            }
        }
    }

    static void calcularCustoEletro(List<Eletro> vetorEletros)//calcula custo por dia/mês
    {
        double consumoDia=0, valorGastoDia=0, valorKw=0, consumoTotalReais=0, consumoTotalKw=0;
        Console.Write("Valor do Kw em R$:");
        valorKw = Convert.ToDouble(Console.ReadLine());
        foreach (Eletro eletro in vetorEletros)
        {
            Console.WriteLine();
            consumoDia = eletro.potencia * eletro.tempoMedioUso;
            valorGastoDia = consumoDia * valorKw;
            Console.WriteLine($"{eletro.nome}\n"+
                              $"Consumo em KW por dia:" +
                              $" {Math.Round(consumoDia, 2)} e" +
                              $" por mês: {Math.Round(consumoDia * 30, 2)}\n"+
                              $"Valor gasto por dia em R$ {(valorGastoDia).ToString("F2",CultureInfo.InvariantCulture)} \n" +
                              $"Valor gasto por mês em R$ {(valorGastoDia*30).ToString("F2",CultureInfo.InvariantCulture)}\n");
            consumoTotalReais += valorGastoDia;
            consumoTotalKw += consumoDia;
        }
        Console.WriteLine($"O consumo total de todos aparelhos diários é de:\n{(consumoTotalKw)} KW " +
                          $" e {(consumoTotalKw*30)} Kw mensais. \nO valor total diário é de R$ {(consumoTotalReais).ToString("F2", CultureInfo.InvariantCulture)} e R$ {(consumoTotalReais*30).ToString("F2",CultureInfo.InvariantCulture)} mensais");
    }

    static void calcularCustoTotal(List<Eletro> vetorEletros)//calcula o custo total em Kw
    {
        double consumoDia = 0, valorKw;
        Console.Write("Valor do Kw em R$:");
        valorKw = Convert.ToDouble(Console.ReadLine());
        foreach (Eletro eletro in vetorEletros)
        {
            consumoDia += eletro.potencia * eletro.tempoMedioUso;
        }
    }

    static int menu()
    {
        int op;
        Console.WriteLine("*** Sistema de Controle de Energia C# ***");
        Console.WriteLine("1-Cadastrar");
        Console.WriteLine("2-Listar");
        Console.WriteLine("3-Busca por nome");
        Console.WriteLine("4-Busca por Consumo");
        Console.WriteLine("5- Mostrar consumo diário e mensal");
        Console.WriteLine("0-Sair");
        Console.Write("Escolha uma opção:");
        op = Convert.ToInt32(Console.ReadLine());
        return op;
    }// fim funcao menu

    static void salvarDados(List<Eletro> eletros, string nomeArquivo)
    {

        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (Eletro eletro in eletros)
            {
                writer.WriteLine($"{eletro.nome};{eletro.potencia};{eletro.tempoMedioUso}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");


    }

    static void carregarDados(List<Eletro> bandas, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(';');
                Eletro eletros = new Eletro
                {
                    nome = campos[0],
                    potencia = double.Parse(campos[1]),
                    tempoMedioUso = int.Parse(campos[2]),
                };
                bandas.Add(eletros);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static void Main()
    {
        List<Eletro> vetorEletros = new List<Eletro>();
        carregarDados(vetorEletros, "dadosEletros.txt");
        int op = 0;

        do
        {
            op = menu();
            switch (op)
            {
                case 1:
                    Console.WriteLine("Cadastrar");
                    addEletro(vetorEletros);
                    break;
                case 2:
                    Console.WriteLine("Listar");
                    listarEletro(vetorEletros);
                    break;
                case 3:
                    Console.Write("Digite o nome de busca:");
                    string nome = Console.ReadLine();
                    buscarNome(vetorEletros, nome);
                    break;
                case 4:
                    Console.WriteLine("Consulta por consumo:");
                    double potencia = double.Parse(Console.ReadLine());
                    buscaConsumo(vetorEletros, potencia);
                    break;
                case 5:
                    Console.WriteLine("Consumo diário e mensal da casa:");
                    calcularCustoEletro(vetorEletros);
                    break;
                case 0:
                    Console.WriteLine("Saindo");
                    salvarDados(vetorEletros, "dadosEletros.txt");
                    break;
            }// fim switch
            Console.ReadKey(); // pausa
            Console.Clear();

        } while (op != 0);

    }

}
