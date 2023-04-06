using System.Collections.Generic;
using UnityEngine;
using Utility.Pooling;
using Utility.Random;

public class EnemyPooler : MonoBehaviour
{
	public static EnemyPooler pool { get; private set; }

	[SerializeField] EnemyDatabase database;
	Dictionary<EnemyType, ObjectPooler<PoolableEnemy>> enemyTable;
	Dictionary<EnemyType, Dictionary<string, float>> statTable;

	private void Awake()
	{
		EnemyPooler[] b = FindObjectsOfType<EnemyPooler>();
		if (b.Length > 1)
		{ Destroy(this); return; }
		pool = this;

		enemyTable = database.LoadTable();
		statTable = database.LoadStat();
	}

	public PoolableEnemy NextEnemy(EnemyType type)
	{
		PoolableEnemy e = enemyTable[type].GetObject();
		if(e != null)
		{
			float f = RandomStream.NormalDistributionMean(statTable[type]["fire rate"], 0.2f);
			float d = RandomStream.NormalDistributionMean(statTable[type]["damage"], 2f);
			e.enemy.SetStat(f, d);
		}

		return e;
	}
}