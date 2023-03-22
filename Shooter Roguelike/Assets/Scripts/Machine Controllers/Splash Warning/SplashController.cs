using Utility.FSM;

public class SplashController : AbstractMachine
{
    void Start()
    {
        ChangeStateCoroutine<StartSplash>(0.5f);
    }
}
