using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Item : MonoBehaviour
{
	protected PoolableItem pool;

	protected virtual void Awake()
	{
		pool = GetComponent<PoolableItem>();
	}

	//TODO: add catch SFX
	public abstract void Catch(PlayerUnit player);
}
