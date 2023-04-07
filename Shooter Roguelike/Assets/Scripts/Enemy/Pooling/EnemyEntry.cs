using System;
using Utility.Pooling;

[Serializable]
public class EnemyEntry : PoolEntry<PoolableEnemy>
{
	public EnemyType type;
	public float averageFireRate;
	public float averageDamage;
	public float averageLife;
}