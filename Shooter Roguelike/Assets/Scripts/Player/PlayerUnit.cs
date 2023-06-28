using System.Collections;
using Utility.EventCommunication;
using UnityEngine;

[RequireComponent(typeof(UnitLife))]
public class PlayerUnit : UnitObject
{
	public static PlayerUnit player { get; private set; }
	public UnitLife life { get; private set; }
	bool dying;

	void Awake()
	{
		player = this;
		life = GetComponent<UnitLife>();
		shield = Instantiate(prefab);
		shield.gameObject.SetActive(false);
		dying = false;
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
		if(dying) return;

		if (FrameAction != null)
		{ FrameAction(); }
	}

	public override void PhysicsUpdate()
	{
		if(dying) return;

		if (PhysicsAction != null)
		{ PhysicsAction(); }
	}

	public override void PostUpdate()
	{
		if(dying) return;

		if (PostAction != null)
		{ PostAction(); }
	}

	void EndGame()
	{
		dying = true;
		StartCoroutine(WaitForDie());
	}

	IEnumerator WaitForDie()
	{
		yield return new WaitForSeconds(0.5f);
		EventHub.Publish(EventList.EndGame);
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		Item i = col.GetComponent<Item>();
		if(i != null) { i.Catch(this); PublicData.totalItens++; }
	}

	[SerializeField] ShieldObject prefab;
	ShieldObject shield;

	public void SetShield()
	{
		if(shield.isActive)
		{
			shield.Damage(-shield.maxPoint);
			return;
		}
		shield.center = transform;
		shield.Activate();
	}
}
