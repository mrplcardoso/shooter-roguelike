using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.EventCommunication;

public class EndGame : GameState
{
	public override IEnumerator OnEnterIntervaled()
	{
		PublicData.totalSeconds = ((int)Time.realtimeSinceStartup - PublicData.totalSeconds);
		yield return new WaitForSeconds(0.5f);
		EventHub.Publish(EventList.TransitionOn);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("Stats Scene");
	}
}