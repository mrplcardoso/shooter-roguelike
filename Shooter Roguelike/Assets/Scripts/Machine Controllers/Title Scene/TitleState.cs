using Utility.FSM;

public abstract class TitleState : AbstractState
{
    public TitleController titleMachine
    { get { return (TitleController)stateMachine; } }
}
