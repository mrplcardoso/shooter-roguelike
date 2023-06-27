using System;
using System.Collections;
using UnityEngine;

public class AreaBullet : MonoBehaviour
{
	//TODO: add SFX
	BulletUnit unit;
	[SerializeField] ParticleSystem areaExplosion;
	[SerializeField] float range;

	private void Awake()
	{
		unit = GetComponent<BulletUnit>();
		areaExplosion = Instantiate(areaExplosion, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//DrawCircle(transform.position, range, Color.red);
		areaExplosion.transform.position = transform.position;
		areaExplosion.gameObject.SetActive(true);
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
		for(int i = 0; i < cols.Length; i++)
		{
			if(col == cols[i]) { continue; }
			print(cols[i].name);

			UnitLife life = cols[i].GetComponent<UnitLife>();
			if(life != null) { life.Damage(unit.damage); }
		}
	}

	public void DrawCircle(Vector3 pos, float radius, Color color)
	{
		for(int i = 0; i < 360; ++i)
		{
			Vector3 p = Vector3.zero;
			p.x = pos.x + radius * Mathf.Cos(Mathf.Deg2Rad * i);
			p.y = pos.y + radius * Mathf.Sin(Mathf.Deg2Rad * i);
			p.z = -2;
			Debug.DrawLine(transform.position, p, color, 2);
		}
	}
}