using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        splashMachine.ChangeStateCoroutine<ExitSplash>(0.1f);
    }
}
