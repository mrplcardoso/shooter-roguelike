using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateStats : StatsState
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
        statsMachine.ChangeStateCoroutine<ExitStats>(0.1f);
    }
}
