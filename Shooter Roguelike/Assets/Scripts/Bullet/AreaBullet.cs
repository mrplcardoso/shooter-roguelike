using System.Collections;
using UnityEngine;

public class AreaBullet : MonoBehaviour
{
	//TODO: change explosion VFX
	BulletUnit unit;
	[SerializeField] float range;

	private void Awake()
	{
		unit = GetComponent<BulletUnit>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
		for(int i = 0; i < cols.Length; i++)
		{
			if(col == cols[i]) { continue; }

			//TODO: improve null check
			UnitLife life = cols[i].GetComponent<UnitLife>();
			if(life)
			{ life.Damage(unit.damage); }
		}
	}
}