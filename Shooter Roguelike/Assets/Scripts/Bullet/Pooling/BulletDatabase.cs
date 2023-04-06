using UnityEngine;
using Utility.Pooling;

[CreateAssetMenu(fileName = "Bullet Database",
	menuName = "Scriptable Objects/Bullet Database")]
public class BulletDatabase : ScriptableObject
{
	[SerializeField] BulletEntry bullet;

	public ObjectPooler<PoolableBullet> LoadTable()
	{
		Transform poolParent = new GameObject("Bullet Container").transform;

		return new ObjectPooler<PoolableBullet>(bullet.prefab, bullet.startSize,
		new Vector2(5800, 0), false, poolParent);
	}
}