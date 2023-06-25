using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{
	public override void Catch(PlayerUnit player)
	{
		player.SetShield();
		pool.Deactivate();
	}
}
