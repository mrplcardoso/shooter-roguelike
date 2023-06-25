using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBulletItem : Item
{
	BulletType type = BulletType.Area;
	public override void Catch(PlayerUnit player)
	{
		PlayerShoot shoot = player.GetComponent<PlayerShoot>();
		BulletEntry entry = BulletPooler.pool.GetEntry(type);
		shoot.AddBullet(entry);
		pool.Deactivate();
	}
}
