using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility.EventCommunication;
using Utility.EasingEquations;

public class TransitionScreen : MonoBehaviour
{
	GraphicRaycaster raycaster;
	Image transitionImage;
	public float alpha { get { return transitionImage.color.a; } 
		set { Color c = transitionImage.color; c.a = value; transitionImage.color = c; } }

	private void Awake()
	{
		if (!SetInstance()) { return; }

		transitionImage = GetComponentInChildren<Image>();
		raycaster = GetComponent<GraphicRaycaster>();
		alpha = 1f;
		Subscribing();
	}

	private void OnDisable()
	{
		UnSubscribing();
	}

	#region Singleton
	public static TransitionScreen instance { get; private set; }
	bool SetInstance()
	{
		TransitionScreen[] t = FindObjectsOfType<TransitionScreen>();
		if(t.Length > 1) { Destroy(gameObject); return false; }
		DontDestroyOnLoad(gameObject); instance = this; return true;
	}
	#endregion

	#region Events
	void Subscribing()
	{
		EventHub.Subscribe(EventList.TransitionOn, TransitionOn);
		EventHub.Subscribe(EventList.TransitionOff, TransitionOff);
	}

	void UnSubscribing()
	{
		EventHub.UnSubscribe(EventList.TransitionOn, TransitionOn);
		EventHub.UnSubscribe(EventList.TransitionOff, TransitionOff);
	}
	#endregion

	#region Transition
	Coroutine transition;
	float speed = 1;
	public bool inTransition { get; private set; }

	IEnumerator Transition(float end)
	{
		inTransition = true;
		float start = alpha;
		float t = 0;
		while (t < 1.01f)
		{
			alpha = EasingFloatEquations.Linear(start, end, t);
			t += speed * Time.unscaledDeltaTime;
			yield return null;
		}
		alpha = end;
		inTransition = false;
	}

	void TransitionOn(EventData data)
	{
		TransitionOn();
	}

	public void TransitionOn()
	{
		if(transition != null)
		{ StopCoroutine(transition); }
		raycaster.enabled = true;
		transition = StartCoroutine(Transition(1));
	}

	void TransitionOff(EventData data)
	{
		TransitionOff();
	}

	public void TransitionOff()
	{
		if (transition != null)
		{ StopCoroutine(transition); }
		raycaster.enabled = false;
		transition = StartCoroutine(Transition(0));
	}
	#endregion
}
