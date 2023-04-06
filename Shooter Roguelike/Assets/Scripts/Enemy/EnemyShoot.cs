using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
	EnemyUnit enemy;
	[SerializeField] Cannon[] cannons;
	public float fireRate = 1f;

	private void Awake()
	{
		enemy = GetComponent<EnemyUnit>();
	}

	private void Start()
	{
		enemy.FrameAction += Shoot;
		cannons = GetComponentsInChildren<Cannon>();
	}

	#region Shoot

	[SerializeField] float shootRange;
	bool inRange { get { return shootRange > 
				(PlayerUnit.player.transform.position - transform.position).magnitude; } }

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
			cannons[i].Shoot(damage);
		}
	}

	#endregion
}