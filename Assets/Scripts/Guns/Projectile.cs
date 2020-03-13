using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [System.NonSerialized]
    public float spread = 0f, speed = 8f;
    private float timer = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb, "Rigidbody not found");
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, Random.Range(-spread, spread)));
    }


    void FixedUpdate()
    {
        transform.position = transform.position + transform.up * Time.fixedDeltaTime * speed;
        timer += Time.fixedDeltaTime;
        if (timer > 5)
            Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger || collision.tag == "Shield")
        {
            Character c = collision.gameObject.GetComponent<Character>();
            if (c != null)
                c.TakeDamage(gameObject.tag);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.isTrigger)
        {
            Character c = collision.gameObject.GetComponent<Character>();
            if (c != null)
                c.TakeDamage(gameObject.tag);
            Destroy(gameObject);
        }
    }

    public void UpdateColliderIgnore(List<Collider2D> colliders)
    {
        foreach (Collider2D c in colliders)
        {
            if(c != null)
                Physics2D.IgnoreCollision(c, GetComponent<Collider2D>());
        }
    }
}
