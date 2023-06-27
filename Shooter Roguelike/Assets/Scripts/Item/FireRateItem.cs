using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;

public class FireRateItem : Item
{
	[SerializeField] float toSub = 0.1f;
	public override void Catch(PlayerUnit player)
	{
		EventHub.Publish(EventList.ItemCatch, new EventData("Fire Rate Up"));

		PlayerShoot shoot = player.GetComponent<PlayerShoot>();
		shoot.SubRate(toSub);

		pool.Deactivate();
	}
}
