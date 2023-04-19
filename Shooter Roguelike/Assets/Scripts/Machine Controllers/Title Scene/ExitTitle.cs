using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.EventCommunication;

public class ExitTitle : TitleState
{
  public override IEnumerator OnEnterIntervaled()
  {
    yield return new WaitForSeconds(0.2f);
    EventHub.Publish(EventList.TransitionOn);
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene("Game Scene");
  }
}
