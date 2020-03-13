using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGun : MonoBehaviour
{
    
    public Transform lazerTarget;
    private LineRenderer lazerRendererer;
    private bool targetLocked = false;
    private float fireRate = 1f;
    private float fireTime = .2f;

    private void Awake()
    {
        lazerRendererer = GetComponent<LineRenderer>();
        lazerRendererer.enabled = false;
        lazerRendererer.useWorldSpace = true;
    }

    void Update()
    {
        TurretActive();
    }

    private void TurretActive()
    {
        RaycastHit2D hit = Physics2D.Raycast(lazerTarget.position, transform.up);        
        lazerRendererer.SetPosition(0, transform.position);
        lazerRendererer.SetPosition(1, lazerTarget.position);

        if (targetLocked)
        {
            Vector3 diff = lazerTarget.position - transform.position;
            float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_Z -90);

            fireRate -= Time.deltaTime;
        }

        if (fireRate < 0)
        {
            fireTime -= Time.deltaTime;

            if (fireTime > 0)
            {
                lazerRendererer.enabled = true;
            }
            else
            {
                lazerRendererer.enabled = false;
            }
            fireRate = 1f;
        }

        else if (fireRate > 0)
        {
         //   lazerRendererer.enabled = false; ___________
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetLocked = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetLocked = false;
        }
    }

   

}
