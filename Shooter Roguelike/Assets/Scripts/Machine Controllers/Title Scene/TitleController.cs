using Utility.FSM;

public class TitleController : AbstractMachine
{
    void Start()
    {
        ChangeStateCoroutine<StartTitle>(0.5f);
    }
}
