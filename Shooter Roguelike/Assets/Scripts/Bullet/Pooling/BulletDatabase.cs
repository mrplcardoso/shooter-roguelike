using System.Collections.Generic;
using UnityEngine;
using Utility.Pooling;

using BulletTable = System.Collections.Generic.Dictionary<BulletType,
	Utility.Pooling.ObjectPooler<PoolableBullet>>;

[CreateAssetMenu(fileName = "Bullet Database",
	menuName = "Scriptable Objects/Bullet Database")]
public class BulletDatabase : ScriptableObject
{
	[SerializeField] BulletEntry[] bullets;

	public Dictionary<BulletType, BulletEntry> LoadEntries()
	{
		Dictionary<BulletType, BulletEntry> entries = new Dictionary<BulletType, BulletEntry>();
		for(int i = 0; i < bullets.Length; i++)
		{
			entries.Add(bullets[i].bulletType, bullets[i]);
		}
		return entries;
	}

	public Dictionary<BulletType, ObjectPooler<PoolableBullet>> LoadTable()
	{
		BulletTable table = new BulletTable();
		Transform bulletParent = new GameObject("Bullet Container").transform;

		for (int i = 0; i < bullets.Length; ++i)
		{
			BulletEntry current = bullets[i];
			Transform poolParent = new GameObject(current.name).transform;
			poolParent.parent = bulletParent;

			ObjectPooler<PoolableBullet> pool = new ObjectPooler<PoolableBullet>(current.prefab, current.startSize,
			new Vector2(5800, 0), false, poolParent);
			table.Add(current.bulletType, pool);
		}
		return table;
	}
}