using System;
using System.Collections;
using UnityEngine;
using Utility.EventCommunication;
using Utility.SceneCamera;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
	CameraSlide camera;
	PlayerUnit player;
	Rigidbody2D body;
	Action<Vector2> reactions;

	Vector2 target;
	Vector2 direction { get { return (target - (Vector2)transform.position); } }
	float speed = 1;
	readonly float distanceOffset = 0.0001f;
	float targetDistance { get { return direction.magnitude; } }
	bool moving;
	bool onTarget { get { return targetDistance <= distanceOffset; } }

	private void Awake()
	{
		player = GetComponent<PlayerUnit>();
		body = GetComponent<Rigidbody2D>();

		EventHub.Subscribe(EventList.OnClickGround, OnClickGround);
		EventHub.Subscribe(EventList.ReactionToPlayer, AddReactions);
	}

	private void Start()
	{
		camera = SceneCamera.instance.GetComponent<CameraSlide>();
		player.PostAction += MoveCamera;
	}

	private void OnDestroy()
	{
		EventHub.UnSubscribe(EventList.OnClickGround, OnClickGround);
		EventHub.UnSubscribe(EventList.ReactionToPlayer, AddReactions);
	}

	void OnClickGround(EventData data)
	{
		target = (Vector3)data.eventInformation;

		if(moving) { return; }

		moving = true;
		player.PhysicsAction -= Move;
		player.PhysicsAction += Move;
	}

	private void Move()
	{
		if (onTarget) { player.PhysicsAction -= Move; moving = false; return; }

		body.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		body.MovePosition(Vector2.MoveTowards(body.position,
			target, speed * Time.fixedDeltaTime));
	}

	void MoveCamera()
	{
		reactions(transform.position);
		camera.Move(body.position, Time.deltaTime);
	}

	void AddReactions(EventData data)
	{
		IPlayerReact reactant = (IPlayerReact)data.eventInformation;

		if(reactant != null)
		{
			reactions -= reactant.Reaction;
			reactions += reactant.Reaction;
		}
	}
}