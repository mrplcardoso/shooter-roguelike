using Utility.FSM;

public class StatsController : AbstractMachine
{
    void Start()
    {
        ChangeStateCoroutine<StartStats>(0.5f);
    }
}
