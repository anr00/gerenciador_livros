using Microsoft.Data.Sqlite;

internal class Gerenciador_Banco_Dados
{
    internal void NovaTabela(string conexao_string)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela = conexao.CreateCommand())
            {
                conexao.Open();

            tabela.CommandText =
                @"CREATE TABLE IF NOT EXISTS livros (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    Livro TEXT, 
                    Autor TEXT,
                    Data TEXT
                )";

                tabela.ExecuteNonQuery();

            }
        }
    }
}