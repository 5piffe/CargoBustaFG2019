using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileGuidance : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject target = null;
    Rigidbody2D rb = null;
    public GameObject missileExplosion;
    private MissileTargetFinder mtf = null;
    public GameObject crosshair = null;

    private float speed = 30;
    private float rotationSpeed = 600f;
    private float theta = 0;
    private float lifeTime = 0.7f;
    private float timeExisted = 0f;
    [System.NonSerialized]
    public bool thetaPos = true;
    [System.NonSerialized]
    public float swirlSpeed = 10;
    [SerializeField]
    private bool swirl = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb, "RigidBody2D was not found");
        Assert.IsNotNull(missileExplosion, "Missile Explision animation not found");
    }

    private void Start()
    {
        Assert.IsNotNull(crosshair, "Crosshair prefab not found");
        mtf = gameObject.AddComponent<MissileTargetFinder>();
        mtf.crosshair = crosshair;
        if(target != null)
            target.GetComponent<MissileTrackable>().trackedList.Add(this);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeExisted > lifeTime)
            Destroy(gameObject);
        timeExisted += Time.fixedDeltaTime;
        if (target == null)
        {
            if (!mtf.GetNearestTrackable(out target))
            {
                transform.position = transform.position + (transform.up / 200) * speed;
                return;
            }
        }

        rb.velocity = Vector2.zero;
        transform.position = transform.position + (transform.up / 200) * speed * 1.75f;
        Vector2 _direction = target.transform.position - transform.position;
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90 + theta;
        Quaternion q = Quaternion.AngleAxis(_angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed);
        rotationSpeed += 120 * Time.fixedDeltaTime;
        speed += 6 * Time.fixedDeltaTime;

        if (swirl)
        {
            if (thetaPos)
            {
                if (theta > 80)
                {
                    swirlSpeed *= -1;
                    thetaPos = false;
                }
            }
            else
            {
                if (theta < -80)
                {
                    swirlSpeed *= -1;
                    thetaPos = true;
                }
            }
            theta += swirlSpeed;
        }
    }
    private void OnDestroy()
    {
        if (target != null)
        {
            target.GetComponent<MissileTrackable>()?.trackedList.Remove(this);
            //target.GetComponent<MissileTrackable>()?.trackedList.RemoveNull();
        }
        Instantiate(missileExplosion, transform.position, transform.rotation);
    }

    public void ChangeTarget(GameObject target)
    {
        if(this.target != null)
            this.target.GetComponent<MissileTrackable>()?.trackedList.Remove(this);
        this.target = target;
        target.GetComponent<MissileTrackable>()?.trackedList.Add(this);
    }

    public void UpdateColliderIgnore(List<Collider2D> colliders)
    {
        foreach (Collider2D c in colliders)
            Physics2D.IgnoreCollision(c, GetComponent<Collider2D>());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.isTrigger && collision.collider.tag != "Missile")
        {
            Destroy(gameObject);
        }
    }
}
