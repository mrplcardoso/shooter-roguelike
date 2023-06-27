using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;

public class AreaBulletItem : Item
{
	BulletType type = BulletType.Area;

	public override void Catch(PlayerUnit player)
	{
		EventHub.Publish(EventList.ItemCatch, new EventData("Area Bullet"));

		PlayerShoot shoot = player.GetComponent<PlayerShoot>();
		BulletEntry entry = BulletPooler.pool.GetEntry(type);
		shoot.AddBullet(entry);
		pool.Deactivate();
	}
}
