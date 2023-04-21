using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utility.Random;

public class UpdateTitle : TitleState
{
  [SerializeField] Button button;
  [SerializeField] TMP_InputField input;

  void Start()
  {
    input.interactable = false;
    button.interactable = false;
    button.onClick.AddListener(OnButtonPressed);
  }

  public override void OnEnter()
  {
    input.interactable = true;
    button.interactable = true;
  }

  void OnButtonPressed()
  {
    input.interactable = false;
    button.interactable = false;

    int seed = int.Parse(input.text);
    if(seed <= 0) { seed = (int)Time.realtimeSinceStartup; }
		
    PublicData.Initialize(seed);

    titleMachine.ChangeStateCoroutine<ExitTitle>(0.1f);
  }
}
