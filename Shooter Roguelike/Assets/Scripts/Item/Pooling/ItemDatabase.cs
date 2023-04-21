using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utility.Pooling;
using ItemArray = System.Collections.Generic.List<Utility.Pooling.ObjectPooler<PoolableItem>>;

[CreateAssetMenu(fileName = "Item Database",
	menuName = "Scriptable Objects/Item Database", order = 3)]
public class ItemDatabase : ScriptableObject
{
	[SerializeField] ItemEntry[] itens;

	public ItemArray LoadArray()
	{
		ItemArray array = new ItemArray(itens.Length);

		Transform arrayParent = new GameObject("Itens").transform;
		Transform poolParent = null;
		ItemEntry entry;

		for(int i = 0; i < itens.Length; ++i)
		{
			entry = itens[i];
			poolParent = new GameObject(entry.prefab.name + " Container").transform;
			poolParent.parent = arrayParent;

			array.Add(new ObjectPooler<PoolableItem>(entry.prefab, entry.startSize,
				new Vector2(5800, 0), false, poolParent));
		}
		return array;
	}
}