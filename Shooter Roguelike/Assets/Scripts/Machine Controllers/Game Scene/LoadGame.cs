using System.Collections;
using UnityEngine;
using Utility.EventCommunication;

public class LoadGame : GameState
{
	void Awake()
	{

	}

	void WaitGeneration()
	{
		EventHub.Publish(EventList.TransitionOff);
	}
}