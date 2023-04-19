using Utility.EventCommunication;
using UnityEngine;

[RequireComponent(typeof(UnitLife))]
public class PlayerUnit : UnitObject
{
	public static PlayerUnit player { get; private set; }
	UnitLife life;

	void Awake()
	{
		player = this;
		life = GetComponent<UnitLife>();
	}

	void Start()
	{
		EventHub.Publish(EventList.AddUpdatable, new EventData(this));
		
		life.OnDeath -= EndGame;
		life.OnDeath += EndGame;
	}

	private void OnDisable()
	{
		life.OnDeath -= EndGame;
	}

	public override void FrameUpdate()
	{
		if (FrameAction != null)
		{ FrameAction(); }
	}

	public override void PhysicsUpdate()
	{
		if (PhysicsAction != null)
		{ PhysicsAction(); }
		life.lifeCanvas.transform.position = transform.position + Vector3.up * 0.5f;
	}

	public override void PostUpdate()
	{
		if (PostAction != null)
		{ PostAction(); }
	}

	void EndGame()
	{
		EventHub.Publish(EventList.EndGame);
	}
}
