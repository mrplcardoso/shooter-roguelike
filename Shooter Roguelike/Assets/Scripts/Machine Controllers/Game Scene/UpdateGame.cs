using System.Collections;
using System.Collections.Generic;
using Utility.EventCommunication;

public class UpdateGame : GameState
{
	private void Awake()
	{
		update = false;
		updatables = new List<IUpdatable>();
		EventHub.Subscribe(EventList.AddUpdatable, AddUpdatable);
	}

	private void OnDestroy()
	{
		EventHub.UnSubscribe(EventList.AddUpdatable, AddUpdatable);
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
	#endregion
}