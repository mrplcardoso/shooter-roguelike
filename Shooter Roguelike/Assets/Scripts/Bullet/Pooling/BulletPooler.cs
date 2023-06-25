using System.Collections.Generic;
using UnityEngine;
using Utility.Pooling;

using BulletTable = System.Collections.Generic.Dictionary<BulletType,
	Utility.Pooling.ObjectPooler<PoolableBullet>>;

public class BulletPooler : MonoBehaviour
{
	public static BulletPooler pool { get; private set; }

	[SerializeField] BulletDatabase database;
	Dictionary<BulletType, BulletEntry> entries;
	BulletTable bullets;

	private void Awake()
	{
		BulletPooler[] b = FindObjectsOfType<BulletPooler>();
		if(b.Length > 1) { Destroy(this); return; }
		pool = this;

		bullets = database.LoadTable();
		entries = database.LoadEntries();
	}

	public BulletEntry GetEntry(BulletType type)
	{
		return entries[type];
	}

	public PoolableBullet NextBullet(BulletType type)
	{
		return bullets[type].GetObject();
	}
}