using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility.Audio;
using TMPro;

public class UpdateStats : StatsState
{
  [SerializeField] Button button;

  void Start()
  {
    button.interactable = false;
    button.onClick.AddListener(OnButtonPressed);
  }

  public override IEnumerator OnEnterIntervaled()
  {
    yield return null;
    button.interactable = true;
  }

  void OnButtonPressed()
  {
    AudioHub.instance.PlayOneTime(AudioList.Click);
    button.interactable = false;
    statsMachine.ChangeStateCoroutine<ExitStats>(0.1f);
  }
}
