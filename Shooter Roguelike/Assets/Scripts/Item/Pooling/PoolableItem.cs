using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Pooling;

public class PoolableItem : MonoBehaviour, IPoolableObject
{
	#region Pooling

	public int poolIndex { get { return id; } set { if (id < 0) { id = value; } } }
	int id = -1;

	public bool activeInScene { get { return gameObject.activeInHierarchy; } }

	public void Activate(float duration = 0)
	{
		gameObject.SetActive(true);

		if (duration > 0)
		{ StartCoroutine(Counter(duration)); }
	}

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}

	IEnumerator Counter(float time)
	{
		yield return new WaitForSeconds(time);
		Deactivate();
	}

	#endregion
}
