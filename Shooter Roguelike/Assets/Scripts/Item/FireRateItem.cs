using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateItem : Item
{
	[SerializeField] float toSub = 0.1f;
	public override void Catch(PlayerUnit player)
	{
		PlayerShoot shoot = player.GetComponent<PlayerShoot>();
		shoot.SubRate(toSub);

		pool.Deactivate();
	}
}
