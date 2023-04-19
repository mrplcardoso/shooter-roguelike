using System;
using UnityEngine;
using Utility.EventCommunication;

public class EnemyUnit : UnitObject, IPlayerReact
{
	SpriteRenderer[] renderers;
	EnemyShoot shoot;
	PoolableEnemy pool;
	UnitLife life;

	bool standby;

	void Awake()
	{
		renderers = GetComponentsInChildren<SpriteRenderer>();
		shoot = GetComponent<EnemyShoot>();
		pool = GetComponent<PoolableEnemy>();
		life = GetComponent<UnitLife>();

		standby = false;
	}

	private void Start()
	{
		EventHub.Publish(EventList.AddUpdatable, new EventData(this));
		EventHub.Publish(EventList.ReactionToPlayer, new EventData(this));

		life.OnDeath -= DeActivate;
		life.OnDeath += DeActivate;
	}

	public override void FrameUpdate()
	{
		if (standby) { return; }

		if (FrameAction != null)
		{ FrameAction(); }
	}

	public override void PhysicsUpdate()
	{
		if (standby)	{ return; }

		if (PhysicsAction != null)
		{ PhysicsAction(); }
	}

	public override void PostUpdate()
	{
		if (standby) { return; }

		if (PostAction != null)
		{ PostAction(); }
	}

	void DeActivate()
	{
		pool.Deactivate();
		EventHub.Publish(EventList.EnemyKilled, new EventData(this));
	}

	public void Reaction(Vector2 position)
	{
		if ((position - (Vector2)transform.position).sqrMagnitude < 144)
		{
			standby = false; 
		}
		else
		{
			standby = true; 
		}
	}

	public void SetStat(float fireRate, float damage, float life)
	{
		shoot.SetStat(fireRate, damage);
		this.life.SetMaxLife(life);
	}
}