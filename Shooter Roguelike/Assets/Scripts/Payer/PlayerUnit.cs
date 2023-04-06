using Utility.EventCommunication;

public class PlayerUnit : UnitObject
{
	public static PlayerUnit player { get; private set; }

	void Awake()
	{
		player = this;
	}

	void Start()
	{
		EventHub.Publish(EventList.AddUpdatable, new EventData(this));
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
	}

	public override void PostUpdate()
	{
		if (PostAction != null)
		{ PostAction(); }
	}
}
