using System.Collections;
using UnityEngine;
using Utility.EventCommunication;

public class StartSplash : SplashState
{
    public override IEnumerator OnEnterIntervaled()
    {
        EventHub.Publish(EventList.TransitionOff);
        yield return new WaitForSeconds(0.8f);
        splashMachine.ChangeState<WaitSplash>();
    }
}
