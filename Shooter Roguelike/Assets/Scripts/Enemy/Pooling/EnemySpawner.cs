using System.Collections;
using UnityEngine;
using Utility.EventCommunication;
using Utility.Random;

public class EnemySpawner : MonoBehaviour, IUpdatable
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
		if(spawns == PublicData.enemiesPerLevel)
		{ gameObject.SetActive(false); }

		if (pauseSpawn) { return; }

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
		if (position.y <= -5000) { PrintConsole.Warning("No space for enemies"); return; }

		distance = (position - (Vector2)PlayerUnit.player.transform.position).magnitude;
		if (distance <= 6f) { return; }

		EnemyType type = (EnemyType)RandomStream.NextInt(0, (int)EnemyType.Count);

		PoolableEnemy next = EnemyPooler.pool.NextEnemy(type);
		if (next == null) { PrintConsole.Warning("No more enemies"); return; }

		Vector2 random = RandomStream.CirclePosition(position, 2f);
		Collider2D col = Physics2D.OverlapCircle(random, 1f);
		if(col != null) { return; }

		next.transform.position = random;
		next.Activate();
		spawns++;
	}

	public void PhysicsUpdate() { }

	public void PostUpdate() { }
}