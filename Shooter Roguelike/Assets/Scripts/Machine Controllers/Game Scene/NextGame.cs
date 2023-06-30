using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.EventCommunication;
using Utility.Audio;

public class NextGame : GameState
{
	[SerializeField] ColorAnimation completeMessage;

	void Start()
	{
		completeMessage.color = Color.clear;
	}

	public override IEnumerator OnEnterIntervaled()
	{
		completeMessage.Animate(Color.white);
		AudioHub.instance.PlayOneTime(AudioList.LevelComplete);
		yield return new WaitForSeconds(1.5f);

		if(PublicData.currentMainPath >= PublicData.maxMainPath)
		{
			gameMachine.ChangeStateCoroutine<EndGame>();
			yield break;
		}

		yield return new WaitForSeconds(0.2f);
		EventHub.Publish(EventList.TransitionOn);
		yield return new WaitForSeconds(2f);
		PublicData.AddLevel();
		SceneManager.LoadScene("Game Scene");
	}
}