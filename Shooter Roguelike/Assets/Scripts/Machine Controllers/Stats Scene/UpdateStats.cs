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

    public override IEnumerator OnEnterIntervaled()
    {
        yield return null;
        button.interactable = true;
    }

    void OnButtonPressed()
    {
        button.interactable = false;
        statsMachine.ChangeStateCoroutine<ExitStats>(0.1f);
    }
}
