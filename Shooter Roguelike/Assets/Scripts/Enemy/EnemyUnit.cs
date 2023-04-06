using System;
using UnityEngine;
using Utility.EventCommunication;

public class EnemyUnit : UnitObject, IPlayerReact
{
	EnemyShoot shoot;

	void Awake()
	{
		shoot = GetComponent<EnemyShoot>();
	}

	private void Start()
	{
		EventHub.Publish(EventList.AddUpdatable, new EventData(this));
		EventHub.Publish(EventList.ReactionToPlayer, new EventData(this));
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

	public void Reaction(Vector2 position)
	{
		if ((position - (Vector2)transform.position).sqrMagnitude < 144)
		{ gameObject.SetActive(true); }
		else
		{ gameObject.SetActive(false); }
	}

	public void SetStat(float fireRate, float damage)
	{

	}
}