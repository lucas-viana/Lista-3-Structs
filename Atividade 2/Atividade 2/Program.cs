using System;
using System.Collections;
using System.Collections.Generic;

internal class Program
{

    struct Livro
    {
        public string titulo;
        public string autor;
        public int ano;
        public string nPrateleira;
    }

    static void addLivro(List<Livro> list)
    {
        Livro novoLivro = new Livro();
        Console.Write("Digite o titulo do livro a ser cadastrado: ");
        novoLivro.titulo = Console.ReadLine();
        Console.Write("Digite o autor do livro a ser cadastrado: ");
        novoLivro.autor = Console.ReadLine();
        Console.Write("Digite o ano de lançamento do livro a ser cadastrado: ");
        novoLivro.ano = int.Parse(Console.ReadLine());
        Console.Write("Digite o numero da prateleira onde o livro sera armazenado: ");
        novoLivro.nPrateleira = Console.ReadLine();

        list.Add(novoLivro);
    }

    static void buscaTitulo(List<Livro> list, string titulo)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].titulo.ToUpper().Contains(titulo.ToUpper()))
            {
                Console.WriteLine($"O livro {list[i].titulo} se encontra na prateleira {list[i].nPrateleira}");
            }
        }
    }

    static void buscaAno(List<Livro> list, int ano)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (ano < list[i].ano)
            {
                Console.WriteLine($"{list[i].titulo}\n{list[i].autor}\n{list[i].ano}\n{list[i].nPrateleira}\n");
            }
        }
    }

    static void listarLivros(List<Livro> list)
    {
        foreach (Livro livro in list)
        {
            Console.WriteLine(livro.titulo);
            Console.WriteLine(livro.autor);
            Console.WriteLine(livro.ano);
            Console.WriteLine(livro.nPrateleira);
            Console.WriteLine();
        }
    }


    static int menu()
    {
        Console.WriteLine("1 - Cadastrar livro");
        Console.WriteLine("2 - Buscar livro por titulo");
        Console.WriteLine("3 - Mostrar livros cadastrados");
        Console.WriteLine("4 - Entre com um ano de lançamento para obter os lançamentos posteriores a ele");
        Console.WriteLine("0 - Para finalizar o programa");
        Console.Write("Entre com a opção desejada:");
        int op = int.Parse(Console.ReadLine());
        return op;
    }

    static void Main(string[] args)
    {
        List<Livro> listaLivro = new List<Livro>();
        int op;
        do
        {
            op = menu();
            switch (op)
            {
                case 0:
                    break;
                case 1:
                    addLivro(listaLivro);
                    break;
                case 2:
                    string tituto = Console.ReadLine();
                    buscaTitulo(listaLivro, tituto);
                    break;
                case 3:
                    listarLivros(listaLivro);
                    break;
                case 4:
                    Console.Write("Entre com o ano desejado:");
                    int ano = int.Parse(Console.ReadLine());
                    buscaAno(listaLivro, ano);
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        }
        while (op != 0);
    }
}