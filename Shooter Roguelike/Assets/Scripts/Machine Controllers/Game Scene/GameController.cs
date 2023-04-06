using Utility.FSM;

public class GameController : AbstractMachine
{
    void Start()
    {
        ChangeState<LoadGame>();
    }
}
