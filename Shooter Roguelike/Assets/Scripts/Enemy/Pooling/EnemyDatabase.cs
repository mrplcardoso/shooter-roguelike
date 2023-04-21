using UnityEngine;
using Utility.Pooling;
using EnemyTable = System.Collections.Generic.
	Dictionary<EnemyType, Utility.Pooling.ObjectPooler<PoolableEnemy>>;
using EnemyStatTable = System.Collections.Generic.
	Dictionary<EnemyType, System.Collections.Generic.Dictionary<string, float>>;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemy Database",
	menuName = "Scriptable Objects/Enemy Database", order = 2)]
public class EnemyDatabase : ScriptableObject
{
	[SerializeField] EnemyEntry[] enemies;

	public EnemyTable LoadTable()
	{
		EnemyTable enemyTable = new EnemyTable();
		Transform tableParent = new GameObject("Enemies").transform;
		Transform poolParent = null;
		EnemyEntry entry;

		for (int i = 0; i < enemies.Length; ++i)
		{
			entry = enemies[i];
			poolParent = new GameObject(entry.prefab.gameObject.name + " Container").transform;
			poolParent.parent = tableParent;

			enemyTable.Add(entry.type,
				new ObjectPooler<PoolableEnemy>(entry.prefab, entry.startSize,
				new Vector2(5900, 0), false, poolParent));
		}
		return enemyTable;
	}

	public EnemyStatTable LoadStat()
	{
		EnemyStatTable table = new EnemyStatTable();
		Dictionary<string, float> d;

		for (int i = 0; i < enemies.Length; ++i)
		{
			d = new Dictionary<string, float>();
			d.Add("damage", enemies[i].averageDamage);
			d.Add("fire rate", enemies[i].averageFireRate);
			d.Add("life", enemies[i].averageLife);
			table.Add(enemies[i].type, d);
		}

		return table;
	}
}