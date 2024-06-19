using System.Globalization;

internal class Input
{
    Controle_Programa controle_programa = new();

    internal void Menu()
    {
        bool rodando = false;
        while (rodando == false)
        {
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Ver livros");
            Console.WriteLine("2 - Adicionar livros");
            Console.WriteLine("3 - Excluir livros");
            Console.WriteLine("4 - Modificar registro");
            Console.WriteLine("0 - Fechar o programa");

            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Comando inválido, por favor, digite um número entre 0 e 4.");
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "0":
                    rodando = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    controle_programa.Get();
                    break;
                case "2":
                    Add();
                    break;
                case "3":
                    Excluir();
                    break;
                case "4":
                    Atualizar();
                    break;
                case "5":
                    //ProcessReport();
                    break;

                default:
                    Console.WriteLine("Comando inválido, por favor, digite um número entre 0 e 4.");
                    break;
            }

        }
    }

    internal string GetLivro()
    {
        Console.WriteLine("Qual o nome do livro lido?");
        string input = Console.ReadLine();
        if (input == "0") Menu();
        return input;
    }

    internal string GetAutor()
    {
        Console.WriteLine("Qual o nome do autor que escreveu o livro?");
        string input = Console.ReadLine();
        if (input == "0") Menu();
        return input;
    }

    internal string GetData()
    {
        Console.WriteLine("Quando você terminou de ler o livro? Por favor, digite no formato: dd-mm-yy");
        string input = Console.ReadLine();
        if (input == "0") Menu();
        while (!DateTime.TryParseExact(input, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("Data inserida no formato incorreto. Favor inserir no formato: dd-mm-yy");
            input = Console.ReadLine();
        }
        return input;
    }

    private void Add()
    {
        var livro = GetLivro();
        var autor = GetAutor();
        var data = GetData();

        Gerenciador gerenciador = new();

        gerenciador.Livro = livro;
        gerenciador.Autor = autor;
        gerenciador.Data = data;

        controle_programa.Criar(gerenciador);
    }

    private void Excluir()
    {
        controle_programa.Get();
        Console.WriteLine("Por favor, digite o id do livro que gostaria de excluir. 0 para retornar ao menu inicial");
        string input = Console.ReadLine();

        while (!Int32.TryParse(input, out _) || string.IsNullOrEmpty(input) || Convert.ToInt32(input) < 0);
        {   
            Console.WriteLine("Por favor, selecione um id válido");
            input = Console.ReadLine();
        }

        var id = Convert.ToInt32(input);
        if (id == 0) Menu();
        var gerenciador = controle_programa.GetId(id);

        while (gerenciador.Id == 0)
        {
            Console.WriteLine($"O registro com o id: {id} não existe");
            Excluir();
        }

        controle_programa.Excluir(id);
    }

    private void Atualizar()
    {
        controle_programa.Get();
        Console.WriteLine("Por favor, selecione o id do livro que deseja atualizar");
        string input = Console.ReadLine();

        while (!Int32.TryParse(input, out _) || string.IsNullOrEmpty(input) || Convert.ToInt32(input) < 0);
        {   
            Console.WriteLine("Por favor, selecione um id válido");
            input = Console.ReadLine();
        }

        var id = Convert.ToInt32(input);
        if (id == 0) Menu();
        var gerenciador = controle_programa.GetId(id);

        while (gerenciador.Id == 0)
        {
            Console.WriteLine($"O registro com o id: {id} não existe");
            Atualizar();
        }

        bool atualizar = true;
        while (atualizar == true)
        {
            Console.WriteLine("Digite n para o nome do livro");
            Console.WriteLine("Digite a para o autor do livro");
            Console.WriteLine("Digite d para a data do livro");
            Console.WriteLine("Digite s para salvar");
            Console.WriteLine("Digite 0 para voltar ao menu inicial");
            input = Console.ReadLine();

            switch (input)
            { 
                case "n":
                    gerenciador.Livro = GetLivro();
                    break;
                case "a":
                    gerenciador.Autor = GetAutor();
                    break;
                case "d":
                    gerenciador.Data = GetData();
                    break;
                case "s":
                    atualizar = false;
                    break;
                case "0":
                    Menu();
                    atualizar = false;
                    break;
                default:
                    Console.WriteLine("Digite uma opção válida");
                    break;
            }
        }
        controle_programa.Atualizar(gerenciador);
        Menu();

    }

}