using System.Configuration;

string conexao_string = ConfigurationManager.AppSettings.Get("ConnectionString");

Gerenciador_Banco_Dados gbd= new();
Input input = new();
gbd.NovaTabela(conexao_string);
input.Menu();