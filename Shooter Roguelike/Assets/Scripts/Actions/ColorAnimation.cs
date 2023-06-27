using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility.EasingEquations;

public class ColorAnimation : MonoBehaviour
{
	Coroutine routine;
  Graphic graphic;
	public Color color { get { return graphic.color; } set { graphic.color = value; }	}
	public float red { get { return color.r; } set { Color c = color; c.r = value; color = c; } }
	public float green { get { return color.g; } set { Color c = color; c.g = value; color = c; } }
	public float blue { get { return color.b; } set { Color c = color; c.b = value; color = c; } }
	public float alpha { get { return color.a; } set { Color c = color; c.a = value; color = c; } }

	[SerializeField] float speed;
	[SerializeField] bool pingpong;

	private void Awake()
	{
    graphic = GetComponent<Graphic>();
  }

	public void Animate(Color target)
	{
		if (routine != null)
		{ StopCoroutine(routine); }
		routine = StartCoroutine(Cycle(target));
	}

	public void Stop(Color end)
	{
		Stop();
		color = end;
	}

	public void Stop()
	{
		if (routine != null)
		{ StopCoroutine(routine); }
	}

	IEnumerator Cycle(Color end)
	{
		Color start = color;
		if (pingpong)
		{
			while (true)
			{
				yield return Animation(end);
				yield return Animation(start);
			}
		}
		else
		{
			yield return Animation(end);
		}
	}

	IEnumerator Animation(Color end)
	{
		Color start = color;
		float t = 0;
		while(t < 1.01f)
		{
			color = EasingColorEquations.Linear(start, end, t);
			t += speed * Time.deltaTime;
			yield return null;
		}
		color = end;
	}
}
