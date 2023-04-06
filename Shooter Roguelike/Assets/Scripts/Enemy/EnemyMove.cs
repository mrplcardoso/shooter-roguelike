using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
	EnemyUnit enemy;
	Rigidbody2D body;
	float speed = 1f;

	private void Awake()
	{
		enemy = GetComponent<EnemyUnit>();
		body = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		enemy.PhysicsAction += Move;
	}

	private void OnDestroy()
	{
		enemy.PhysicsAction -= Move;
	}

	#region Move

	Vector2 direction;

	public void Move()
	{
		body.MovePosition(body.position + speed * Time.fixedDeltaTime * direction.normalized);
	}

	#endregion
}