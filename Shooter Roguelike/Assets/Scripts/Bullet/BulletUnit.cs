using UnityEngine;
using Utility.EventCommunication;

public class BulletUnit : UnitObject
{
	public PoolableBullet poolable { get; private set; }

	public float damage;
	public int layer
	{
		get { return gameObject.layer; }
		set
		{
			Transform[] t = GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < t.Length; ++i)
			{ t[i].gameObject.layer = value; }
		}
	}

	private void Awake()
	{
		poolable = GetComponent<PoolableBullet>();
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