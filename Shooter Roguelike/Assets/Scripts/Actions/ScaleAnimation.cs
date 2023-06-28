using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility.EasingEquations;

public class ScaleAnimation : MonoBehaviour
{
  Coroutine routine;
	public Vector3 scale { get { return transform.localScale; } set { transform.localScale = value; } }
	[SerializeField] float speed;
	public bool animating { get; private set; }

	public void Animate(Vector3 target)
	{
		animating = true;
		if (routine != null)
		{ StopCoroutine(routine); }
		routine = StartCoroutine(Animation(target));
	}

	public void Stop(Vector3 end)
	{
		Stop();
		scale = end;
	}

	public void Stop()
	{
		if (routine != null)
		{ StopCoroutine(routine); }
		animating = false;
	}

	IEnumerator Animation(Vector3 end)
	{
		Vector3 start = scale;
		float t = 0;
		while (t < 1.01f)
		{
			scale = EasingVector3Equations.Linear(start, end, t);
			t += speed * Time.deltaTime;
			yield return null;
		}
		scale = end;
		animating = false;
	}
}
