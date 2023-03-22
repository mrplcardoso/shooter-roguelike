using Utility.FSM;

public abstract class SplashState : AbstractState
{
    public SplashController splashMachine
    { get { return (SplashController)stateMachine; } }
}
