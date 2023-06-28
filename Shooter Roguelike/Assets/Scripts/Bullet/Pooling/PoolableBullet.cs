using System;
using System.Collections;
using UnityEngine;
using Utility.Pooling;
using Utility.Audio;

public class PoolableBullet : MonoBehaviour, IPoolableObject
{
	[SerializeField] ParticleSystem explosion;
	public BulletUnit bullet;
	public Action OnActivate, OnDeactivate;

	void Awake()
	{
		bullet = GetComponent<BulletUnit>();
		explosion = Instantiate(explosion, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		AudioHub.instance.PlayOneTime(AudioList.Hit);
		explosion.transform.position = transform.position;
		explosion.gameObject.SetActive(true);
		Deactivate();
	}

	#region Pooling

	public int poolIndex { get { return id; } set { id = id == -1 ? value : id; } }
	int id = -1;

	[SerializeField] float lifeSpan;
	public float life { get { return lifeSpan; } set { lifeSpan = value; } }

	public bool activeInScene { get { return gameObject.activeInHierarchy; } }

	public void Activate(float duration = 0)
	{
		if(duration > 0)
		{ lifeSpan = duration; }

		gameObject.SetActive(true);

		if(OnActivate != null)
		{ OnActivate(); }

		StartCoroutine(Counter());
	}

	public void Deactivate()
	{
		if(OnDeactivate != null)
		{ OnDeactivate(); }
		
		gameObject.SetActive(false);
	}

	IEnumerator Counter()
	{
		yield return new WaitForSeconds(lifeSpan);
		Deactivate();
	}

	#endregion
}