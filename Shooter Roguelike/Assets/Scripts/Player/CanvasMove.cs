using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;

public class CanvasMove : MonoBehaviour, IPlayerReact
{
    void Start()
    {
        EventHub.Publish(EventList.ReactionToPlayer, new EventData(this));
    }

    public void Reaction(Vector2 position)
    {
        transform.position = position;
    }
}
