using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleChallenge
{
    public class Turtle
    {
        private (int x, int y) _position;
        private Direction _direction;

        private Dictionary<Direction, Func<(int x, int y), Turtle>> _rotations = new Dictionary<Direction, Func<(int x, int y), Turtle>>
        {
            { Direction.North,(position)=>new Turtle(position, Direction.East) },
            { Direction.East,(position)=>new Turtle(position, Direction.South) },
            { Direction.South,(position)=>new Turtle(position, Direction.West) },
            { Direction.West,(position)=>new Turtle(position, Direction.North) }
        };

        private Dictionary<Direction, Func<(int x, int y), Turtle>> _moves = new Dictionary<Direction, Func<(int x, int y), Turtle>>
        {
            { Direction.North,(position)=>new Turtle((position.x,position.y-1), Direction.North) },
            { Direction.East,(position)=>new Turtle((position.x+1,position.y), Direction.East) },
            { Direction.South,(position)=>new Turtle((position.x,position.y+1), Direction.South) },
            { Direction.West,(position)=>new Turtle((position.x-1,position.y), Direction.West) }
        };

        public Turtle((int x,int y) initialPosition, Direction direction)
        {
            _position = initialPosition;
            _direction = direction;
        }

        public Turtle Rotate()
        {
            return _rotations[_direction](_position);
        }
        public Turtle Move()
        {
            return _moves[_direction](_position);
        }

        internal Turtle Play(TurtleAction action)
        {
            if (action == TurtleAction.Rotate)
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
}
