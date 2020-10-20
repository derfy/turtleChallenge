using System;
using System.Linq;

namespace TurtleChallenge
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    public class TurtleState
    {
        private (int x, int y) _position;
        private Direction _direction;

        public TurtleState((int x,int y) initialPosition, Direction direction)
        {
            _position = initialPosition;
            _direction = direction;
        }

        public TurtleState Rotate()
        {
            return _direction switch
            {
                Direction.North => new TurtleState(_position, Direction.East),
                Direction.East => new TurtleState(_position, Direction.South),
                Direction.South => new TurtleState(_position, Direction.West),
                Direction.West => new TurtleState(_position, Direction.North),
                _ => this,
            };
        }
        public TurtleState Move()
        {
            return _direction switch
            {
                Direction.North => new TurtleState((_position.x,_position.y-1), _direction),
                Direction.East => new TurtleState((_position.x+1, _position.y), _direction),
                Direction.South => new TurtleState((_position.x, _position.y+1), _direction),
                Direction.West => new TurtleState((_position.x-1, _position.y), _direction),
                _ => this,
            };
        }

        internal TurtleState Play(Action action)
        {
            if (action == Action.Rotate)
                return Rotate();
            return Move();
        }

        internal bool HasExit((int x, int y) exit)
        {
            return _position==exit;
        }

        internal bool HasMine((int x, int y)[] mines)
        {
            return mines.Contains(_position);
        }
    }
    public enum Action
    {
        Rotate,
        Move
 
    }
    public enum GameResult
    {
        Exit,
        HitMine,
        None
    }
    public class Game
    {
        private (int x, int y)[] _mines;
        private (int x, int y) _exit;

        public Game((int x,int y)[] mines,(int x,int y) exit)
        {
            _mines = mines;
            _exit = exit;
        }
        public GameResult Play(Action[] actions, TurtleState turtle)
        {
            var newState = turtle;
            foreach(var action in actions)
            {
                newState = newState.Play(action);
                if (newState.HasExit(_exit))
                    return GameResult.Exit;
                if (newState.HasMine(_mines))
                    return GameResult.HitMine;
            }
            return GameResult.None;
        }

        
    }
    class Program
    {
        static void Main(string[] args)
        {
            var turtle = new TurtleState((0, 1), Direction.North);
            var game = new Game(new[] { (1, 1), (3, 1), (3, 3) }, (4, 2));
            var success = game.Play(new[] { Action.Rotate, Action.Rotate, Action.Move, Action.Rotate, Action.Rotate, Action.Rotate, Action.Move, Action.Move, Action.Move,Action.Move }, turtle);
            Console.WriteLine(success.ToString());
        }
    }
}
