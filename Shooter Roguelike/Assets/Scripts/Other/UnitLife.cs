﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitLife : MonoBehaviour
{
	[SerializeField] Image bar;
	[SerializeField] float maxLife;

	public Action OnDeath;

	public float life { get; private set; }
	public float current { get { return life; } 
		set { life = Mathf.Clamp(value, 0f, maxLife); bar.fillAmount = life / maxLife; } }

	private void Start()
	{
		bar = GetComponentInChildren<Image>();
		current = maxLife;
	}

	public void ChangeBy(float value)
	{
		current = life + value;
	}

	public void Set(float value)
	{
		current = value;
	}

	public void SetMaxLife(float value)
	{
		value = Mathf.Abs(value) + 1; //if <= 0
		maxLife = life = value;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Bullet"))
		{
			BulletUnit bullet = collision.GetComponent<BulletUnit>();
			ChangeBy(-bullet.damage);
			bullet.poolable.Deactivate();

			if(current <= 0)
			{
				if(OnDeath != null)
				{ OnDeath(); }
			}
		}
	}
}