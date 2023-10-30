using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text;

internal class Program
{
    struct emprestimo
    {
        public DateTime date;
        public string nome;
        public bool emprestado;

    }
    struct jogos
    {
        public string titulo;
        public string console;
        public int ano;
        public int ranking;
        public emprestimo emprestimo;
    }

    static int menu()
    {
        Console.WriteLine("1-Cadastro");
        Console.WriteLine("2-Busca por titulos");
        Console.WriteLine("3-Busca por console");
        Console.WriteLine("4-Emprestar jogos");
        Console.WriteLine("5-Devolver jogo");
        Console.WriteLine("6-Listar jogos emprestados");
        Console.Write("Entre com a operção desejada: ");
        int op = int.Parse(Console.ReadLine());
        return op;
    }
    static void cadastro(List<jogos> list)
    {
        jogos novoJogo = new jogos();
        Console.WriteLine("Entre com o titulo do jogo");
        novoJogo.titulo = Console.ReadLine();
        Console.WriteLine("Entre com o console do jogo");
        novoJogo.console = Console.ReadLine();
        Console.WriteLine("Entre com o ano de lançamento do jogo");
        novoJogo.ano = int.Parse(Console.ReadLine());
        Console.WriteLine("Entre com o ranking do jogo");
        novoJogo.ranking = int.Parse(Console.ReadLine());
        novoJogo.emprestimo.emprestado = false;

        list.Add(novoJogo);
    }
    static void buscaPorTitulo(List<jogos> list, string titulo)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].titulo.ToUpper().Contains(titulo.ToUpper()))
            {
                Console.WriteLine($"{list[i].titulo}\n" +
                                  $"{list[i].console}\n{list[i].ano}\n" +
                                  $"{list[i].ranking}\n");
            }
        }
    }
    static void buscaPorConsole(List<jogos> list, string console)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].console.ToUpper().Equals(console))
            {
                Console.WriteLine($"{list[i].titulo}\n" +
                                  $"{list[i].console}\n{list[i].ano}\n" +
                                  $"{list[i].ranking}\n");
            }
        }
    }

    static void emprestarJogo(List<jogos> list, string titulo)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].titulo.ToUpper().Equals(titulo.ToUpper()))
            {
                jogos empJogo = new jogos();
                empJogo = list[i];
                empJogo.emprestimo.date = DateTime.Now;
                Console.Write("Entre com o nome do requerente:");
                empJogo.emprestimo.nome = Console.ReadLine();
                empJogo.emprestimo.emprestado = true;
                list[i] = empJogo;
                break;
            }
        }
    }

    static void devolverJogo(List<jogos> list, string titulo)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].titulo.ToUpper().Equals(titulo.ToUpper()))
            {
                jogos empJogo = new jogos();
                empJogo = list[i];
                empJogo.emprestimo.date = DateTime.Now;
                empJogo.emprestimo.emprestado = false;
                list[i] = empJogo;
                break;
            }
        }
    }
    static void listaEmprestimo(List<jogos>list )
    {
        foreach (jogos jogo in list)
        {
            if(jogo.emprestimo.emprestado == true)
            {
                Console.WriteLine($"{jogo.titulo} \n{jogo.emprestimo.nome}\n{jogo.emprestimo.date}");
            }
        }
    }
    static void Main(string[] args)
    {
        List<jogos> listajogos = new List<jogos>();

        int op;
        do
        {
            
            op = menu();
            
            switch (op)
            {
                
                case 1:
                    cadastro(listajogos);
                    break;
                case 2:
                    Console.Write("Entre com o titulo do jogo:");
                    string titulo = Console.ReadLine();
                    buscaPorTitulo(listajogos, titulo);
                    break;
                case 3:
                    string console = Console.ReadLine();
                    buscaPorConsole(listajogos, console);
                    break;
                case 4:
                    Console.Write("Entre com o nome do titulo desejado:");
                    string empTitulo = Console.ReadLine();
                    emprestarJogo(listajogos, empTitulo);
                    break;
                case 5:
                    Console.Write("Entre com o nome do titulo que deseja devolver:");
                    empTitulo = Console.ReadLine();
                    devolverJogo(listajogos, empTitulo);
                    break;
                case 6:
                    Console.WriteLine("Jogos emprestados: ");
                    listaEmprestimo(listajogos);
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (op != 0);
    }
}