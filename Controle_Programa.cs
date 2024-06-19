using System.Configuration;
using Microsoft.Data.Sqlite;

internal class Controle_Programa
{
    string conexao_string = ConfigurationManager.AppSettings.Get("ConnectionString");

    internal void Criar(Gerenciador gerenciador)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela = conexao.CreateCommand())
            {
                conexao.Open();
                tabela.CommandText = $"INSERT INTO livros (livro, autor, data) VALUES ('{gerenciador.Livro}', '{gerenciador.Autor}', '{gerenciador.Data}')";
                tabela.ExecuteNonQuery();
            }
        }
    }

    internal void Excluir(int id)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_livros = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_livros.CommandText = $"DELETE from livros WHERE Id = '{id}'";
                tabela_livros.ExecuteNonQuery();

                Console.WriteLine($"O registro id: {id} foi excluído");
            }
        }
    }

    internal void Atualizar(Gerenciador gerenciador)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_livros = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_livros.CommandText = 
                    $@"UPDATE livros SET 
                        Livro = '{gerenciador.Livro}', 
                        Autor = '{gerenciador.Autor}',
                        Data = '{gerenciador.Data}' 
                       WHERE 
                        Id = {gerenciador.Id}
                     ";

                tabela_livros.ExecuteNonQuery();
            }
        }
        Console.WriteLine($"Registo id: {gerenciador.Id} foi atualizado");

    }
    internal void Get()
    {
        List<Gerenciador> tabela = new List<Gerenciador>();
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_livros = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_livros.CommandText = "SELECT * FROM livros";

                using (var reader = tabela_livros.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tabela.Add(
                            new Gerenciador
                            {
                                Id = reader.GetInt32(0),
                                Livro = reader.GetString(1),
                                Autor = reader.GetString(2),
                                Data = reader.GetString(3)
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma coluna foi encontrada");
                    }
                }

            }
        }
        Ver_Tabela.ShowTable(tabela);

    }

    internal Gerenciador GetId(int id)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_livros = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_livros.CommandText = $"SELECT * FROM livros Where Id = '{id}'";

                using (var reader = tabela_livros.ExecuteReader())
                {
                    Gerenciador gerenciador = new();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        gerenciador.Id = reader.GetInt32(0);
                        gerenciador.Livro = reader.GetString(1);
                        gerenciador.Autor = reader.GetString(2);
                        gerenciador.Data = reader.GetString(3);
                    }

                        Console.WriteLine("\n\n");

                        return gerenciador;
                    };

            }
        }
    }

}

