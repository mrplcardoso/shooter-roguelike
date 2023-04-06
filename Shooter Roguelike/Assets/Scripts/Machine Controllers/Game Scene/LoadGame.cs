using Utility.EventCommunication;

public class LoadGame : GameState
{
	void Awake()
	{
		EventHub.Subscribe(EventList.RoomGenerationCompleted, WaitGeneration);
	}

	void WaitGeneration(EventData data)
	{
		gameMachine.ChangeStateCoroutine<StartGame>(0.1f);
	}

	private void OnDestroy()
	{
		EventHub.UnSubscribe(EventList.RoomGenerationCompleted, WaitGeneration);
	}
}