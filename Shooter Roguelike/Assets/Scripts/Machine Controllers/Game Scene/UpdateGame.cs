using System.Collections;
using System.Collections.Generic;
using Utility.EventCommunication;

public class UpdateGame : GameState
{
	private void Awake()
	{
		killCount = 0;
		update = false;
		updatables = new List<IUpdatable>();
		EventHub.Subscribe(EventList.AddUpdatable, AddUpdatable);
		EventHub.Subscribe(EventList.EnemyKilled, OnKill);
		EventHub.Subscribe(EventList.EndGame, EndGame);
	}

	private void OnDestroy()
	{
		EventHub.UnSubscribe(EventList.AddUpdatable, AddUpdatable);
		EventHub.UnSubscribe(EventList.EnemyKilled, OnKill);
		EventHub.UnSubscribe(EventList.EndGame, EndGame);
	}

	#region Update
	List<IUpdatable> updatables;
	bool update;

	void AddUpdatable(EventData data)
	{
		IUpdatable up = (IUpdatable)data.eventInformation;
		if (!updatables.Contains(up))
		{ updatables.Add(up); }
	}

	private void Update()
	{
		if (!update) { return; }

		for(int i = 0; i < updatables.Count; i++)
		{
			if (!updatables[i].isActive) { continue; }

			updatables[i].FrameUpdate();
		}
	}

	private void FixedUpdate()
	{
		if (!update) { return; }

		for (int i = 0; i < updatables.Count; i++)
		{
			if (!updatables[i].isActive)
			{ continue; }

			updatables[i].PhysicsUpdate();
		}
	}

	private void LateUpdate()
	{
		if (!update) { return; }

		for (int i = 0; i < updatables.Count; i++)
		{
			if (!updatables[i].isActive)
			{ continue; }

			updatables[i].PostUpdate();
		}
	}
	#endregion

	#region State

	public override IEnumerator OnEnterIntervaled()
	{
		yield return null;
		update = true;
	}

	public override IEnumerator OnExitIntervaled()
	{
		update = false;
		yield return null;
	}

	void EndGame(EventData data)
	{
		gameMachine.ChangeStateCoroutine<EndGame>();
	}

	#endregion

	#region Kills
	int killCount;

	void OnKill(EventData data)
	{
		killCount++;
		
		if(killCount >= PublicData.enemiesPerLevel)
		{
			gameMachine.ChangeStateCoroutine<NextGame>();
		}
	}
	#endregion
}