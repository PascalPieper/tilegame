using System;
using TileGame.Interfaces;

namespace TileGame.Character
{
    public class Char : IMove
    {
        public bool MoveUp() => throw new NotImplementedException();

        public bool MoveDown() => throw new NotImplementedException();

        public bool MoveLeft() => throw new NotImplementedException();

        public bool MoveRight() => throw new NotImplementedException();
    }

    public class Player : Char
    {
    }
}