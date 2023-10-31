using System.Globalization;

namespace Atividade_5
{
    internal class Program
    {
        struct Gado
        {
            public int codigo;
            public int leite;
            public int alimento;
            public char abate;
            public Nascimento nascimento;
        }
        struct Nascimento
        {
            public int mes;
            public int ano;
        }

        static void CadastrarGado(List<Gado> list)
        {
            Gado novoGado = new Gado();
            Console.Write("Entre com o codigo do Gado:");
            novoGado.codigo = int.Parse(Console.ReadLine());
            Console.Write("Entre com a quantidade de leite/semana do Gado:");
            novoGado.leite = int.Parse(Console.ReadLine());
            Console.Write("Entre com a quantide de alimento ingerida pelo Gado:");
            novoGado.alimento = int.Parse(Console.ReadLine());
            novoGado.abate = 'N';

            Console.Write("Entre com a data de nascimento do gado entre com o mes e em seguida o ano:");
            novoGado.nascimento.mes = int.Parse(Console.ReadLine());
            novoGado.nascimento.ano = int.Parse(Console.ReadLine());
            DateTime nascimento = new DateTime(novoGado.nascimento.ano, novoGado.nascimento.mes, 1);
            TimeSpan diferenca = DateTime.Now - nascimento;

            if (novoGado.leite < 40 || diferenca.TotalDays > 1825)
            {
                novoGado.abate = 'S';
            }

            list.Add(novoGado);
            Console.WriteLine("Novo gado adicionado");

        }
        static void ListarGados(List<Gado> list)
        {
            foreach (Gado gados in list)
            {
                Console.WriteLine(gados.codigo);
                Console.WriteLine(gados.leite);
                Console.WriteLine(gados.alimento);
                Console.WriteLine(gados.abate);
            }
        }

        static void totalLeite(List<Gado> list)
        {
            int total = 0;
            foreach (Gado gado in list)
            {
                total += gado.leite;
            }
            Console.Write("Total de leite produzido em litros: " + total);
        }

        static void totalAlimento(List<Gado> list)
        {
            int total = 0;
            foreach (Gado gado in list)
            {
                total += gado.alimento;
            }
            Console.Write("O consumo de alimentos dos gados e de::" + total);
        }

        static void listaAbate(List<Gado> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].abate == 'S')
                {
                    Console.WriteLine($"Codigo do gado: {list[i].codigo}\nData de nascimento: {list[i].nascimento.mes}/{list[i].nascimento.ano}\n{list[i].leite} litros de leite");
                }
            }
        }

        static int menu()
        {
            Console.WriteLine("1-Quantidade total leite/semana");
            Console.WriteLine("2-Quantidade total de consumo alimento/semana");
            Console.WriteLine("3-Listar os animais que devem ser abatidos");
            Console.WriteLine("4-Salvar e Carregar Dados");
            Console.WriteLine("5-Cadastrar gado");
            Console.WriteLine("6-Sair do programa");
            Console.Write("Entre com a opção desejada: ");
            int op = int.Parse(Console.ReadLine());
            return op;
        }

        static void salvarDados(List<Gado> list, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (Gado gados in list)
                {
                    writer.WriteLine($"{gados.codigo},{gados.leite},{gados.alimento}" +
                        $",{gados.nascimento.mes},{gados.nascimento.ano},{gados.abate}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");


        }

        static void carregarDados(List<Gado> list, string nomeArquivo)
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(',');
                    Nascimento data = new Nascimento
                    {
                        mes = int.Parse(campos[3]),
                        ano = int.Parse(campos[4])
                    };
                    Gado gado = new Gado
                    {

                        codigo = int.Parse(campos[0]),
                        leite = int.Parse(campos[1]),
                        alimento = int.Parse(campos[2]),
                        abate = char.Parse(campos[5]),
                        nascimento = data
                    };


                    list.Add(gado);
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado :(");
            }
        }

        static void Main(string[] args)
        {
            
            List<Gado> list = new List<Gado>();
            carregarDados(list,"DadosGado.txt");
            int op;
            do
            {
                op = menu();
                switch (op)
                {
                    case 1:
                        totalLeite(list);
                        break;
                    case 2:
                        totalAlimento(list);
                        break;
                    case 3:
                        listaAbate(list);
                        break;
                    case 4:
                        salvarDados(list, "DadosGado.txt");
                        carregarDados(list, "DadosGado.txt");
                        break;
                    case 5:
                        CadastrarGado(list);
                        break;
                    case 6:
                        op = 6;
                        break;


                }
                Console.ReadKey();
                Console.Clear();
            } while (op != 6);
        }
    }
}