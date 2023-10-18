using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Utility.SceneCamera;

public class PlayerAim : MonoBehaviour
{
	PlayerUnit player;
	[SerializeField] Transform cannon;

	Vector2 clickPosition { get { return ClickHandler.clickHandler.position; } }

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
		if(ClickHandler.clickHandler.touchInput && !ClickHandler.clickHandler.touching) { return; }

		angularDirection = (clickPosition - (Vector2)cannon.position).normalized;
		angleRotation = (Mathf.Atan2(angularDirection.y, angularDirection.x) * Mathf.Rad2Deg) - 90f;
	}

	public void Rotate()
	{
		cannon.rotation = Quaternion.Euler(Vector3.forward * angleRotation);
	}
}