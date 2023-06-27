using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
	[SerializeField] ParticleSystem shoot;
	[SerializeField] LayerMask bulletLayer;
	int layer;

	void Awake()
	{
		layer = (int)Mathf.Log(bulletLayer.value, 2);
		shoot = Instantiate(shoot);
		shoot.gameObject.SetActive(false);
	}

	public void Shoot(BulletType type, float damage)
	{
		PoolableBullet next = BulletPooler.pool.NextBullet(type);
		if (next == null) { PrintConsole.Warning("No more bullets"); return; }

		Vector3 p = transform.position;
		p.z = 1;
		shoot.transform.position = p;
		shoot.transform.rotation = transform.rotation;
		shoot.gameObject.SetActive(true);

		next.transform.position = transform.position;
		next.transform.rotation = transform.rotation;
		next.bullet.layer = layer;
		next.bullet.damage = damage;
		next.Activate();
	}
}