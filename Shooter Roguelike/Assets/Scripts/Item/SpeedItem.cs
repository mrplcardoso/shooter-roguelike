using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;

public class SpeedItem : Item
{
	[SerializeField] float toAdd = 0.1f;
	public override void Catch(PlayerUnit player)
	{
		EventHub.Publish(EventList.ItemCatch, new EventData("Speed Up"));

		PlayerMove move = player.GetComponent<PlayerMove>();
		move.AddSpeed(toAdd);

		pool.Deactivate();
	}
}
