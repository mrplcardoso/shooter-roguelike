using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	[SerializeField] LayerMask bulletLayer;
	int layer;

	void Awake()
	{
		layer = (int)Mathf.Log(bulletLayer.value, 2);
	}

	public void Shoot(float damage)
	{
		PoolableBullet next = BulletPooler.pool.NextBullet();
		if (next == null) { PrintConsole.Warning("No more bullets"); return; }

		next.transform.position = transform.position;
		next.transform.rotation = transform.rotation;
		next.bullet.layer = layer;
		next.bullet.damage = damage;
		next.Activate();
	}
}