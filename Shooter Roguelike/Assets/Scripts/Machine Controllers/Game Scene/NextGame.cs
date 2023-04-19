using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.EventCommunication;

public class NextGame : GameState
{
	public override IEnumerator OnEnterIntervaled()
	{
		if(PublicData.currentMainPath >= PublicData.maxMainPath)
		{
			gameMachine.ChangeStateCoroutine<EndGame>();
			yield break;
		}

		yield return new WaitForSeconds(0.2f);
		EventHub.Publish(EventList.TransitionOn);
		yield return new WaitForSeconds(1f);
		PublicData.AddLevel();
		SceneManager.LoadScene("Game Scene");
	}
}