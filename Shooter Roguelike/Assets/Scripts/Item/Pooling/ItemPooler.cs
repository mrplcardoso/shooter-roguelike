using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Pooling;
using Utility.Random;

public class ItemPooler : MonoBehaviour
{
	public static ItemPooler pool { get; private set; }

	[SerializeField] ItemDatabase database;
	List<ObjectPooler<PoolableItem>> itemArray;

	private void Awake()
	{
		ItemPooler[] b = FindObjectsOfType<ItemPooler>();
		if (b.Length > 1)
		{ Destroy(this); return; }
		pool = this;

		itemArray = database.LoadArray();
	}

	public PoolableItem NextItem()
	{
		int r = 3;//RandomStream.NextInt(0, itemArray.Count);
		PoolableItem i = itemArray[r].GetObject();
		return i;
	}
}
