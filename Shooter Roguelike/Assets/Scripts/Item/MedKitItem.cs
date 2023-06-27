using System.Collections;
using UnityEngine;
using Utility.EventCommunication;

public class MedKitItem : Item
{
	[SerializeField] float recovery;
	public override void Catch(PlayerUnit player)
	{
		EventHub.Publish(EventList.ItemCatch, new EventData("MedKit"));

		player.life.ChangeBy(recovery);
		Destroy(gameObject);
	}
}