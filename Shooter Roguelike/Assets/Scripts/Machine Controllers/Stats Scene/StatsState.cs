using Utility.FSM;

public abstract class StatsState : AbstractState
{
    public StatsController statsMachine
    { get { return (StatsController)stateMachine; } }
}
