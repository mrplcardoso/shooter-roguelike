using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Utility.Audio;

public class UpdateTitle : TitleState
{
  [SerializeField] Button button;
  [SerializeField] TMP_InputField input;
  [SerializeField] Toggle seedToggle;

  void Start()
  {
    input.interactable = false;
    seedToggle.interactable = false;
    seedToggle.onValueChanged.AddListener(OnToggleChage);
    button.interactable = false;
    button.onClick.AddListener(OnButtonPressed);
  }

  public override void OnEnter()
  {
    AudioHub.instance.PlayLoop(AudioList.TitleBGM, false);
    seedToggle.interactable = true;
    input.interactable = true;
    button.interactable = true;
  }

  void OnToggleChage(bool value)
  {
    AudioHub.instance.PlayOneTime(AudioList.Click);
    if (value)
    { input.text = ((int)DateTime.Now.Ticks).ToString(); input.interactable = false; }
    else
    { input.text = ""; input.interactable = true; }
  }

  void OnButtonPressed()
  {
    AudioHub.instance.PlayOneTime(AudioList.Click);
    seedToggle.interactable = false;
    input.interactable = false;
    button.interactable = false;

    int seed = string.IsNullOrEmpty(input.text) ? (int)DateTime.Now.Ticks : int.Parse(input.text);

    PublicData.Initialize(seed);

    titleMachine.ChangeStateCoroutine<ExitTitle>(0.1f);
  }
}
