using System.Collections;
using Utility.EventCommunication;
using UnityEngine;

public class EnemyShoot : MonoBehaviour, IPlayerReact
{
	EnemyUnit enemy;
	[SerializeField] Cannon[] cannons;
	public float fireRate;

	private void Awake()
	{
		enemy = GetComponent<EnemyUnit>();
	}

	private void Start()
	{
		enemy.FrameAction += Shoot;
		cannons = GetComponentsInChildren<Cannon>();
		EventHub.Publish(EventList.ReactionToPlayer, new EventData(this));
	}

	public void SetStat(float fireRate, float damage)
	{
		this.fireRate = Mathf.Abs(fireRate) + 0.5f;
		this.damage = Mathf.Abs(damage) + 1f;
	}

	public void Reaction(Vector2 position)
	{
		playerPosition = position;
	}

	#region Shoot

	Vector3 playerPosition;
	[SerializeField] float shootRange;
	bool inRange { get { return shootRange > (playerPosition - transform.position).magnitude; } }

	float elapsedTime;
	public float damage;
	
	private void Shoot()
	{
		elapsedTime += Time.deltaTime;
		if (inRange)
		{
			if (elapsedTime > fireRate)
			{
				Fire();
				elapsedTime = 0;
			}
		}
	}

	void Fire()
	{
		for (int i = 0; i < cannons.Length; ++i)
		{
			cannons[i].Shoot(BulletType.Standard, damage);
		}
	}

	#endregion
}