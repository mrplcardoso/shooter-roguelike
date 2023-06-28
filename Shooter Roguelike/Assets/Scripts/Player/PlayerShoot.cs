using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;
using Utility.Audio;

public class PlayerShoot : MonoBehaviour
{
	PlayerUnit player;
	[SerializeField] BulletType current;
	public Dictionary<BulletType, int> bullets;

	Cannon[] cannons;
	float damage = 1f;
	[SerializeField] float interval = 1f;
	float timer = 0f;
	bool shoot;

	private void Awake()
	{
		player = GetComponent<PlayerUnit>();
		bullets = new Dictionary<BulletType, int>();
		bullets.Add(current, -1);
		cannons = GetComponentsInChildren<Cannon>();
		EventHub.Subscribe(EventList.OnClickEnemy, Shoot);
	}

	private void Start()
	{
		player.FrameAction += Timer;
	}

	public void AddBullet(BulletEntry entry)
	{
		if(!bullets.ContainsKey(entry.bulletType))
		{
			bullets.Add(entry.bulletType, 0);
		}
		bullets[entry.bulletType] = entry.ammunitionCount;
		current = entry.bulletType;
	}

	public void SubRate(float value)
	{
		interval = Mathf.Clamp(interval - value, 0.2f, 1);
	}

	void Timer()
	{
		if(timer < 0f && !shoot)
		{
			shoot = true;
			return;
		}
		timer -= Time.deltaTime;
	}

	private void Shoot(EventData data)
	{
		if(shoot)
		{
			AudioHub.instance.PlayOneTime(AudioList.Launch);
			for (int i = 0; i < cannons.Length; ++i)
			{
				PublicData.totalShoots++;
				cannons[i].Shoot(current, damage);
				CheckAmmo();
			}
			shoot = false;
			timer = interval;
		}
	}

	void CheckAmmo()
	{
		if (bullets[current] < 0) { return; }
		
		bullets[current]--;
		if (bullets[current] <= 0)
		{
			bullets.Remove(current);
			int c = (int)current;
			c--;
			current = (BulletType)c;
		}
	}

	private void OnDestroy()
	{
		EventHub.UnSubscribe(EventList.OnClickEnemy, Shoot);
	}
}