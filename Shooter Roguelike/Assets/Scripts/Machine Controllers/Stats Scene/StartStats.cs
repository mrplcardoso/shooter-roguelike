using System;
using System.Collections;
using UnityEngine;
using Utility.EventCommunication;
using Utility.EasingEquations;
using TMPro;

public class StartStats : StatsState
{
  [SerializeField] TextMeshProUGUI[] stats;

  public override IEnumerator OnEnterIntervaled()
  {
    EventHub.Publish(EventList.TransitionOff);
    yield return new WaitForSeconds(0.8f);
    StartCoroutine(StatsView());
  }

  IEnumerator StatsView()
  {
    yield return LerpSeconds(stats[0], PublicData.totalSeconds);
    yield return LerpText(stats[1], PublicData.totalShoots);
    yield return LerpText(stats[2], PublicData.totalItens);
    yield return LerpText(stats[3], PublicData.seedUsed);
    yield return LerpText(stats[4], PublicData.currentMainPath);

    statsMachine.ChangeStateCoroutine<UpdateStats>();
  }

  IEnumerator LerpText(TextMeshProUGUI text, int end)
  {
    int start = 0;
    float t = 0;
    while (t < 1.01f)
    {
      text.text = ((int)EasingFloatEquations.Linear(start, end, t)).ToString();
      t += Time.deltaTime;
      yield return null;
    }
    text.text = end.ToString();
  }

  IEnumerator LerpSeconds(TextMeshProUGUI text, int end)
  {
    int start = 0;
    float t = 0;
    TimeSpan ts;
    while (t < 1.01f)
    {
      int i = (int)EasingFloatEquations.Linear(start, end, t);
      t += Time.deltaTime;

      ts = TimeSpan.FromSeconds(i);
      text.text = ts.ToString(@"hh\:mm\:ss");
      yield return null;
    }

    ts = TimeSpan.FromSeconds(end);
    text.text = ts.ToString(@"hh\:mm\:ss");
  }
}
