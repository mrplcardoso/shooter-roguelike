using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;

public class ShieldObject : MonoBehaviour, IUpdatable
{
    //TODO: add VFX's
    //TODO: add damage
    public Transform center;
    
    Vector3 position { set { Vector3 v = value; v.z = transform.position.z; transform.position = v;} }
    public bool isActive { get { return gameObject.activeInHierarchy; } }

    void Start() 
    {
        EventHub.Publish(EventList.AddUpdatable, new EventData(this));   
    }

    public void FrameUpdate()
    {
        ToCenter();
    }

    public void PhysicsUpdate() {}

    public void PostUpdate() {}

    public void ToCenter()
    {
        position = center.position;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
