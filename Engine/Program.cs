namespace Engine;

internal class Program
{
    private static void Main(string[] args)
    {
        using var game = new Engine.Game1();
        game.Run();
    }
}
