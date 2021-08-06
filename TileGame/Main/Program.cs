using System;
using TileGame.Main;
using TileGame.Game;
using TileGame.Tiles;

namespace TileGame
{
    static class Program
    {
        static void Main(string[] args)
        {
            var window = new GameWindow();
            window.Run();
        }
    }
}
