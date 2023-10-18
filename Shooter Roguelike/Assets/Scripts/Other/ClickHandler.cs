using System;
using System.Collections;
using UnityEngine;
using Utility.EventCommunication;
using Utility.SceneCamera;

public class ClickHandler : MonoBehaviour, IUpdatable
{
	public static ClickHandler clickHandler { get; private set; }
	public bool touchInput { get; private set; }
	public bool touching { get; private set; }

	Action InputRoutine;

	[SerializeField] LayerMask clickMask;

	public bool isActive { get { return gameObject.activeInHierarchy; } }
	public Vector3 position { get; private set; }

	void Awake()
	{
		ClickHandler[] clicks = FindObjectsOfType<ClickHandler>();
		for(int i = 0; i < clicks.Length; i++)
		{
			if (this != clicks[i])
			{ 
				Destroy(this.gameObject);
				return; 
			}
		}
		clickHandler = this;
		
		if(Application.platform == RuntimePlatform.Android)
		{
			touchInput = true;
			InputRoutine += TouchRoutine;
		}
		else
		{
			touchInput = false;
			InputRoutine += MouseRoutine;
		}
	}

	void Start()
	{
		EventHub.Publish(EventList.AddUpdatable, new EventData(this));
	}

	public void FrameUpdate()
	{
		InputRoutine();
	}

	public void PhysicsUpdate() { }

	public void PostUpdate() { }

	void MouseRoutine()
	{
		position = SceneCamera.instance.camera.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButtonDown(1))
		{ EventHub.Publish(EventList.OnClickEnemy, new EventData(position)); return; }

		if(Input.GetMouseButtonDown(0))
		{
			Collider2D col = Physics2D.OverlapCircle(position, 0.25f, clickMask);
			if(col != null)
			{
				if(col.CompareTag("Enemy"))
				{
					EventHub.Publish(EventList.OnClickEnemy, new EventData(col.transform.position));
					return;
				}
				/*Otherwise, collider is a wall. Wall collision do nothing*/
				return;
			}
			EventHub.Publish(EventList.OnClickGround, new EventData(position));
		}
	}

	void TouchRoutine()
	{
		if(Input.touchCount > 0)
		{
			touching = true;
			Touch touch = Input.GetTouch(0);
			position = SceneCamera.instance.camera.ScreenToWorldPoint(touch.position);

			if(touch.phase == TouchPhase.Ended)
			{
				Collider2D col = Physics2D.OverlapCircle(position, 0.25f, clickMask);
				if(col != null)
				{
					if(col.CompareTag("Enemy"))
					{
						EventHub.Publish(EventList.OnClickEnemy, new EventData(col.transform.position));
						return;
					}
					/*Otherwise, collider is a wall. Wall collision do nothing*/
					return;
				}
				EventHub.Publish(EventList.OnClickGround, new EventData(position));
			}
		}
		else { touching = false; }
	}
}