using System.Collections;
using UnityEngine;
using Utility.Audio;
using Utility.EventCommunication;

public class StartGame : GameState
{
  public override IEnumerator OnEnterIntervaled()
  {
    EventHub.Publish(EventList.TransitionOff);
    yield return new WaitForSeconds(0.8f);
    AudioHub.instance.PlayLoop(AudioList.GameBGM);
    gameMachine.ChangeStateCoroutine<UpdateGame>();
  }
}
