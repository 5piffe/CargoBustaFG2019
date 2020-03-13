using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Magnetizable : MonoBehaviour
{
    public bool magnetActive = true;

    MagnetData magnetData;
    public Rigidbody2D rigidBody;

    public Magnetizable nextMagnet;
    public Magnetizable magnetizedTo;

    public Transform posMag;
    public Transform negMag;


    private void Awake()
    {
        magnetData = Resources.Load<MagnetData>("ScriptableObjects/MagnetData");
        rigidBody = transform.parent.GetComponent<Rigidbody2D>();
        magnetData.nonMagnetizedList.Add(this);

        Assert.IsTrue(gameObject.transform.childCount > 0);
        posMag = gameObject.transform.GetChild(0);
        if(gameObject.transform.childCount > 1)
            negMag = gameObject.transform.GetChild(1);
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.tag == "Magnet")
    //    {
    //        transform.parent.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * 50);
    //        //gameObject.
    //    }
    //    if (collision.tag == "Cargo")
    //    {
    //        transform.parent.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * 50);
    //    }
    //}
    
    //public void DeMagnetize()
    //{
    //    if (rigidBody == null)
    //        return;
    //    rigidBody.velocity = Vector2.zero;
    //    magnetData.RemoveMagnetizable(this);
    //    magnetActive = false;

        

    //    if (magnetizedTo != null)
    //    {
    //        magnetizedTo.nextMagnet = null;
    //        if (magnetizedTo.magnetizedTo == null)
    //        {
    //            magnetizedTo.DeMagnetize();
    //        }
    //        magnetizedTo = null;
    //    }

    //    if (nextMagnet != null)
    //    {
    //        nextMagnet.DeMagnetize();
    //        nextMagnet = null;
    //    }
    //}

    public void DeMagnetize(Magnetizable mag)
    {
        if (rigidBody == null)
            return;
        if(magnetData.magnetizedList.Count == 0)
        {
            return;
        }
        Magnetizable _last = magnetData.magnetizedList.GetLast();
        while(_last != this)
        {
            _last.rigidBody.velocity = Vector2.zero;
            _last.magnetActive = false;
            magnetData.RemoveMagnetizable(_last);
            _last.magnetizedTo = null;
            _last.nextMagnet = null;
            //if (magnetData.magnetizedList.Count == 0)
            //    break;
            if (magnetData.magnetizedList.Count == 0)
                break;
            _last = magnetData.magnetizedList.GetLast();
        }
        rigidBody.velocity = Vector2.zero;
        magnetActive = false;
        magnetData.RemoveMagnetizable(this);
        if (magnetizedTo != null)
            magnetizedTo.nextMagnet = null;
        magnetizedTo = null;
        nextMagnet = null;
    }

    public void DeMagnetize()
    {        
        DeMagnetize(this);
    }


    public void Magnetize()
    {
        AudioManager.instance.Play("Pickcargo");
        magnetActive = true;
        Magnetizable _mag = magnetData.magnetizedList.GetLast();
        magnetizedTo = _mag;
        _mag.nextMagnet = this;
        magnetData.AddMagnetizable(this);
    }
    
    public void Magnetize(Magnetizable toMagnet)
    {
        AudioManager.instance.Play("Pickcargo");
        magnetActive = true;
        magnetizedTo = toMagnet;
        toMagnet.nextMagnet = this;
        magnetData.AddMagnetizable(this);
    }


}
