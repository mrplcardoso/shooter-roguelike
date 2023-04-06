using System.Collections;
using UnityEngine;
using Utility.EventCommunication;

public class StartGame : GameState
{
  public override IEnumerator OnEnterIntervaled()
  {
    EventHub.Publish(EventList.TransitionOff);
    yield return new WaitForSeconds(0.8f);
    gameMachine.ChangeStateCoroutine<UpdateGame>();
  }
}
