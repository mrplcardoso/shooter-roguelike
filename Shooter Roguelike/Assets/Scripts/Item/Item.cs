using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Item : MonoBehaviour
{
	public abstract void Catch(PlayerUnit player);
}
