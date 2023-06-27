using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;

public class ShieldItem : Item
{
	public override void Catch(PlayerUnit player)
	{
		EventHub.Publish(EventList.ItemCatch, new EventData("Shield"));

		player.SetShield();
		pool.Deactivate();
	}
}
