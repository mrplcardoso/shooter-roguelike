using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.EventCommunication;
using Utility.Audio;

public class ExitTitle : TitleState
{
  public override IEnumerator OnEnterIntervaled()
  {
    AudioHub.instance.Stop(AudioList.TitleBGM);
    yield return new WaitForSeconds(0.2f);
    EventHub.Publish(EventList.TransitionOn);
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene("Game Scene");
  }
}
