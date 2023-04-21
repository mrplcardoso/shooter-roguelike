using UnityEngine;
using Utility.EventCommunication;
using Utility.Random;

public class ItemSpawner : MonoBehaviour, IUpdatable
{
	int spawns = 0;
	float elapsedTime;

	[SerializeField] float distance;
	[SerializeField] float spawnInterval;
	[SerializeField] bool pauseSpawn;

	public bool isActive { get { return gameObject.activeInHierarchy; } }

	private void Start()
	{
		EventHub.Publish(EventList.AddUpdatable, new EventData(this));
	}

	public void FrameUpdate()
	{
		if (spawns == PublicData.itensPerLevel)
		{ gameObject.SetActive(false); }

		if (pauseSpawn)
		{ return; }

		elapsedTime += Time.deltaTime;
		if (elapsedTime > spawnInterval)
		{
			Spawn();
			elapsedTime = 0;
		}
	}

	void Spawn()
	{
		Vector2 position = DungeonLevel.dungeon.RandomRoomPosition();
		if (position.y <= -5000)
		{ PrintConsole.Warning("No space for itens"); return; }

		distance = (position - (Vector2)PlayerUnit.player.transform.position).magnitude;
		if (distance <= 6f)
		{ return; }

		PoolableItem next = ItemPooler.pool.NextItem();
		if (next == null)
		{ PrintConsole.Warning("No more itens"); return; }

		next.transform.position = RandomStream.CirclePosition(position, 2f);
		next.Activate();
		spawns++;
	}

	public void PhysicsUpdate() { }

	public void PostUpdate() { }
}