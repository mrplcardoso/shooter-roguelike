using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
	[SerializeField] float toAdd = 0.1f;
	public override void Catch(PlayerUnit player)
	{
		PlayerMove move = player.GetComponent<PlayerMove>();
		move.AddSpeed(toAdd);

		pool.Deactivate();
	}
}
