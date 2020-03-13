using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D col = null;
    int health = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Instantiated");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Missile")
        {
            MissileGuidance _misGuidance = collision.GetComponent<MissileGuidance>();
            
            if(_misGuidance != null)
            {
                if (_misGuidance.target != null && _misGuidance.target.tag != "Flare")
                {
                    _misGuidance.ChangeTarget(gameObject);
                }
                else if (_misGuidance.target == null)
                {
                    _misGuidance.ChangeTarget(gameObject);
                }
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == "Missile")
        {
            
            if ((collision.transform.position - transform.position).magnitude < 0.3f)
            {
                if (health > 0)
                {
                    health--;
                    Destroy(collision.gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
