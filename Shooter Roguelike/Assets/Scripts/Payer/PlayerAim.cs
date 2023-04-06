using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Utility.SceneCamera;

public class PlayerAim : MonoBehaviour
{
	PlayerUnit player;
	[SerializeField] Transform cannon;

	Vector2 mousePosition { get { return ClickHandler.clickHandler.position; } }

	private void Awake()
	{
		player = GetComponent<PlayerUnit>();
	}

	private void Start()
	{
		player.FrameAction += ReadInput;
		player.FrameAction += Rotate;
	}

	private void OnDestroy()
	{
		player.FrameAction -= ReadInput;
		player.FrameAction -= Rotate;
	}

	Vector2 angularDirection;
	float angleRotation;

	public void ReadInput()
	{
		angularDirection = (mousePosition - (Vector2)cannon.position).normalized;
		angleRotation = (Mathf.Atan2(angularDirection.y, angularDirection.x) * Mathf.Rad2Deg) - 90f;
	}

	public void Rotate()
	{
		cannon.rotation = Quaternion.Euler(Vector3.forward * angleRotation);
	}
}