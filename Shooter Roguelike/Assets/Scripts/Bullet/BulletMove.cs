using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(BulletUnit))]
public class BulletMove : MonoBehaviour
{
  BulletUnit bulletUnit;  
  Rigidbody2D body;
  float speed = 3f;

  void Awake()
  {
    bulletUnit = GetComponent<BulletUnit>();
    body = GetComponent<Rigidbody2D>();

    bulletUnit.PhysicsAction += Move;
  }

  void OnDestroy()
  {
    bulletUnit.PhysicsAction -= Move;
  }

  public void Move()
  {
    body.MovePosition(body.position +
      (Vector2)transform.up * speed * Time.fixedDeltaTime);
  }
}