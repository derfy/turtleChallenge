namespace TurtleChallenge
{
    public class Game
    {
        private (int x, int y)[] _mines;
        private (int x, int y) _exit;

        public Game((int x,int y)[] mines,(int x,int y) exit)
        {
            _mines = mines;
            _exit = exit;
        }
        public GameResult Play(TurtleAction[] actions, Turtle turtle)
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
}
