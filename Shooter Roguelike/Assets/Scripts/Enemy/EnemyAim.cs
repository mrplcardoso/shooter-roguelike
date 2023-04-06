using System.Collections;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
	EnemyUnit enemy;
	[SerializeField] Transform cannon;

	private void Awake()
	{
		enemy = GetComponent<EnemyUnit>();
	}

	private void Start()
	{
		enemy.FrameAction += ReadDirection;
		enemy.PhysicsAction += Rotate;
	}

	private void OnDestroy()
	{
		enemy.FrameAction -= ReadDirection;
		enemy.PhysicsAction -= Rotate;
	}

	#region Rotation

	Vector2 targetPosition { get { return PlayerUnit.player.transform.position; } }
	Vector2 angularDirection;
	float angleRotation;

	public void ReadDirection()
	{
		angularDirection = (targetPosition - (Vector2)cannon.position).normalized;
		angleRotation = (Mathf.Atan2(angularDirection.y, angularDirection.x) * Mathf.Rad2Deg) - 90f;
	}

	public void Rotate()
	{
		cannon.rotation = Quaternion.Euler(Vector3.forward * angleRotation);
	}

	#endregion
}