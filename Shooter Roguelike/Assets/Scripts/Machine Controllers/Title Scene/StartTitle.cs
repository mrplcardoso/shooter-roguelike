using System.Collections;
using UnityEngine;
using Utility.EventCommunication;

public class StartTitle : TitleState
{
    public override IEnumerator OnEnterIntervaled()
    {
        EventHub.Publish(EventList.TransitionOff);
        yield return new WaitForSeconds(0.8f);
        titleMachine.ChangeState<UpdateTitle>();
    }
}
