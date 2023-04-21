using System.Collections;
using UnityEngine;

public class MedKitItem : Item
{
	[SerializeField] float recovery;
	public override void Catch(PlayerUnit player)
	{
		player.life.ChangeBy(recovery);
		Destroy(gameObject);
	}
}