using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;
using Utility.EasingEquations;
using TMPro;

public class ItemCatchMessage : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    ColorAnimation colorAnimation;
    [SerializeField] float speed;
    Vector3 position { get => transform.localPosition; set => transform.localPosition = value; }
    Vector3 startPosition;
    Vector3 endPosition;

    void Awake()
    {
        startPosition = position;
        endPosition = startPosition + Vector3.up * 1f;
        
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "";
        colorAnimation = GetComponent<ColorAnimation>();
        EventHub.Subscribe(EventList.ItemCatch, OnItemCatch);
    }

    void OnItemCatch(EventData data)
    {
        string s = (string)data.eventInformation;
        textMesh.text = s;
        StartCoroutine(MessageTransition());
    }

    IEnumerator MessageTransition()
    {
        float t = 0;
        colorAnimation.color = Color.white;
        colorAnimation.Animate(Color.clear);
        while(t < 1.01f)
        {
            position = EasingVector3Equations.Linear(startPosition, endPosition, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        position = endPosition;
        textMesh.text = "";
    }

    void OnDestroy()
    {
        EventHub.UnSubscribe(EventList.ItemCatch, OnItemCatch);
    }
}
