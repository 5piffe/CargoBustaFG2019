using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private float windTimer = 0;
    private float timeTilNextWind = 0;
    private void Awake()
    {
        if(!anim)
            anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim.enabled = true;
        timeTilNextWind = Random.Range(2f, 15f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (windTimer > timeTilNextWind && !(anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime))
        {
            anim.enabled = true;
            timeTilNextWind = Random.Range(2, 8f);
            windTimer = 0;
            anim.Play("Wind", -1, 0f);
        }
        else
        {
            windTimer += Time.fixedDeltaTime;
        }
    }
}
