using System;

namespace TurtleChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var turtle = new Turtle((0, 1), Direction.North);
            var game = new Game(new[] { (1, 1), (3, 1), (3, 3) }, (4, 2));
            var success = game.Play(new[] { TurtleAction.Rotate, TurtleAction.Rotate, TurtleAction.Move, TurtleAction.Rotate, TurtleAction.Rotate, TurtleAction.Rotate, TurtleAction.Move, TurtleAction.Move, TurtleAction.Move,TurtleAction.Move }, turtle);
            Console.WriteLine(success.ToString());
        }
    }
}
