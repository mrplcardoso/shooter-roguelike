using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility.Audio;

public class WaitSplash : SplashState
{
  [SerializeField] Button button;

  void Start()
  {
    button.interactable = false;
    button.onClick.AddListener(OnButtonPressed);
  }

  public override void OnEnter()
  {
    button.interactable = true;
  }

  void OnButtonPressed()
  {
    button.interactable = false;
    AudioHub.instance.PlayOneTime(AudioList.Click);
    splashMachine.ChangeStateCoroutine<ExitSplash>(0.1f);
  }
}
