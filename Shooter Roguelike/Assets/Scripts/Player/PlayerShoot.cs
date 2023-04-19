using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;

public class PlayerShoot : MonoBehaviour
{
	Cannon[] cannons;
	float damage = 1f;

	private void Awake()
	{
		cannons = GetComponentsInChildren<Cannon>();
		EventHub.Subscribe(EventList.OnClickEnemy, Shoot);
	}

	private void Shoot(EventData data)
	{
		//TODO: add interval
		for (int i = 0; i < cannons.Length; ++i)
		{
			cannons[i].Shoot(damage);
		}
	}

	private void OnDestroy()
	{
		EventHub.UnSubscribe(EventList.OnClickEnemy, Shoot);
	}
}