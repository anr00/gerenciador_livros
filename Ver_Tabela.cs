using ConsoleTableExt;

internal class Ver_Tabela
{
    internal static void ShowTable<T>(List<T> tabela) where T : class
    {
        ConsoleTableBuilder
                .From(tabela)
                .WithTitle("Gerenciador")
                .ExportAndWriteLine();
        Console.WriteLine("\n\n");
    }
}