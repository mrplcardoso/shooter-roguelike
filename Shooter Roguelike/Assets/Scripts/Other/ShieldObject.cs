using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EventCommunication;
using Utility.Audio;

public class ShieldObject : MonoBehaviour, IUpdatable, IDamageable
{
    public Transform center;
    
    Vector3 position { set { Vector3 v = value; v.z = transform.position.z; transform.position = v;} }
    public bool isActive { get { return gameObject.activeInHierarchy; } }

    private void Awake()
    {
        colorAnimation = GetComponent<ColorAnimation>();
        scaleAnimation = GetComponent<ScaleAnimation>();
        transform.localScale = Vector3.zero;
        GetComponentInChildren<SpriteRenderer>().color = Color.clear;
    }

    void Start() 
    {
        EventHub.Publish(EventList.AddUpdatable, new EventData(this));
    }

    public void FrameUpdate()
    {
        ToCenter();
    }

    public void PhysicsUpdate() {}

    public void PostUpdate() {}

    public void ToCenter()
    {
        position = center.position;
    }

    ColorAnimation colorAnimation;
    ScaleAnimation scaleAnimation;

    public void Activate()
    {
        currentPoints = hitPoints;
        gameObject.SetActive(true);
        scaleAnimation.Animate(Vector3.one);
        colorAnimation.Animate(Color.white);
    }

    public void Deactivate()
    {
        scaleAnimation.Animate(Vector3.zero);
        colorAnimation.Animate(Color.clear);
        StartCoroutine(WaitAnimation());
    }

    IEnumerator WaitAnimation()
    {
        yield return new WaitWhile(() => scaleAnimation.animating);
        gameObject.SetActive(false);
    }

    [SerializeField] float hitPoints;
    public float maxPoint => hitPoints;
    float currentPoints;
    float porcentage { get => currentPoints / hitPoints; }

    public void Damage(float value)
    {
        currentPoints = Mathf.Clamp(currentPoints - value, 0, hitPoints);
        colorAnimation.color = Color.white;
        colorAnimation.Animate(new Color(1, 0, 0, 0), 1 - porcentage);

        if(currentPoints <= 0) { Deactivate(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            BulletUnit bullet = collision.GetComponent<BulletUnit>();
            Damage(bullet.damage);
        }
    }
}
