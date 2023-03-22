using Utility.FSM;

public abstract class GameState : AbstractState
{
    public GameController gameMachine
    { get { return (GameController)stateMachine; } }
}
