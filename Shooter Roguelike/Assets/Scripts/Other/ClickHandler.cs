using System.Collections;
using UnityEngine;
using Utility.EventCommunication;
using Utility.SceneCamera;

public class ClickHandler : MonoBehaviour, IUpdatable
{
	public static ClickHandler clickHandler { get; private set; }

	[SerializeField] LayerMask clickMask;
	public Vector3 position { get { 
			return SceneCamera.instance.camera.ScreenToWorldPoint(Input.mousePosition); } }
	public bool isActive { get { return gameObject.activeInHierarchy; } }

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
	}

	void Start()
	{
		EventHub.Publish(EventList.AddUpdatable, new EventData(this));
	}

	public void FrameUpdate()
	{
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

	public void PhysicsUpdate() { }

	public void PostUpdate() { }
}