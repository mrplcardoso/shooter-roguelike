using System;
using UnityEngine;
using Utility.Pooling;

[Serializable]
public class BulletEntry : PoolEntry<PoolableBullet>
{
	[SerializeField] BulletType type;
	public BulletType bulletType {  get { return type; } }
	[SerializeField] int ammunition;
	public int ammunitionCount { get { return ammunition; } }
}