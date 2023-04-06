using System.Collections.Generic;
using UnityEngine;
using Utility.Pooling;

public class BulletPooler : MonoBehaviour
{
	public static BulletPooler pool { get; private set; }

	[SerializeField] BulletDatabase database;
	ObjectPooler<PoolableBullet> bullets;

	private void Awake()
	{
		BulletPooler[] b = FindObjectsOfType<BulletPooler>();
		if(b.Length > 1) { Destroy(this); return; }
		pool = this;

		bullets = database.LoadTable();
	}

	public PoolableBullet NextBullet()
	{
		return bullets.GetObject();
	}
}