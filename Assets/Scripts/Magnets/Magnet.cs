using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Magnet : MonoBehaviour
{
    //public bool magnetActive;
    [SerializeField]
    MagnetData magnetData;
    Magnetizable headMagnetizable;
    [SerializeField]
    float snapOnRange = 4;

    [SerializeField]
    bool elasticMagnets = false, chainMagnets = true;
    void Start()
    {
        Assert.IsNotNull(magnetData);
        headMagnetizable = gameObject.AddComponent<Magnetizable>();
        headMagnetizable.magnetActive = true;

        magnetData.nonMagnetizedList.Remove(headMagnetizable);
    }


    private void FixedUpdate()
    {
        HandleMagnets();
    }

    void HandleMagnets()
    {
        if (headMagnetizable.nextMagnet == null)
        {
            List<Magnetizable> _nonMagnetizedList = new List<Magnetizable>(magnetData.nonMagnetizedList);
            foreach (Magnetizable magnet in _nonMagnetizedList)
            {
                if (magnet == null)
                {
                    magnetData.DestroyMagnetizable(magnet);
                    continue;
                }

                float _distance = (headMagnetizable.transform.position - (magnet.transform.position)).magnitude;
                if (_distance < snapOnRange)
                {
                    if (magnet.magnetActive)
                    {
                        magnet.Magnetize(headMagnetizable);
                        break;
                    }
                        
                }
                else if(_distance < snapOnRange + 3)
                {
                    if (!magnet.magnetActive)
                    {
                        magnet.magnetActive = true;
                    }

                }
            }
        }

        if (magnetData.magnetizedList.Count > 0)
        {
            //List<Magnetizable> _nonMagnetizedList = new List<Magnetizable>(magnetData.nonMagnetizedList);
            //foreach (Magnetizable m in _nonMagnetizedList)
            //{
            //    if (!m.magnetActive && (magnetData.magnetizedList.GetLast().transform.position - m.transform.position).magnitude < snapOnRange)
            //    {
            //        m.Magnetize();
            //    }
            //}

            List<Magnetizable> _nonMagnetizedList = new List<Magnetizable>(magnetData.nonMagnetizedList);
            
            foreach (Magnetizable m in _nonMagnetizedList)
            {
                Magnetizable _mag = magnetData.magnetizedList.GetLast();
                while (_mag.magnetizedTo == null && magnetData.magnetizedList.Count > 0)
                {
                    magnetData.RemoveMagnetizable(_mag);
                    _mag = magnetData.magnetizedList.GetLast();
                }
                if (m == null)
                {
                    magnetData.DestroyMagnetizable(m);
                    continue;
                }
                else if(m.magnetizedTo != null || m.nextMagnet != null)
                {
                    m.magnetizedTo = null;
                    m.nextMagnet = null;
                }

                float _distance = (_mag.transform.position - m.transform.position).magnitude;
                if (_distance < snapOnRange)
                {
                    if (m.magnetActive)
                    {
                        m.Magnetize(_mag);
                    }
                }
                else if(_distance > snapOnRange + 3)
                {
                    if(!m.magnetActive)
                    {
                        m.magnetActive = true;
                    }
                }
            }

            List<Magnetizable> _magnetizedList = new List<Magnetizable>(magnetData.magnetizedList);
            foreach (Magnetizable m in _magnetizedList)
            {
                if (m.magnetizedTo == null)
                {
                    Debug.Log("CALLED");
                    m.DeMagnetize();
                    continue;
                }
                float _distance = ((m.magnetizedTo.transform.position/* - m.magnetizedTo.transform.rotation.eulerAngles.normalized*/) - (m.transform.position/* + m.transform.rotation.eulerAngles.normalized*/)).magnitude;
                if (_distance < snapOnRange + 1)
                {
                    if (m.magnetActive)
                    {
                        #region ElasticRopeMethod
                        if (elasticMagnets)
                        {
                            Vector3 moveVector = (m.magnetizedTo.posMag.position - m.negMag.position) * magnetData.magnetForceScalar;
                            m.rigidBody.AddForce(moveVector);
                        }
                        #endregion

                        #region ChainMethod
                        //m.transform.parent.GetComponent<Rigidbody2D>().velocity = headMagnetizable.transform.parent.GetComponent<Rigidbody2D>().velocity / snapOnRange;
                        #endregion

                        #region AbsoluteMovementMethod
                        if (chainMagnets)
                        {
                            m.transform.parent.position = m.transform.parent.position + (m.magnetizedTo.posMag.position - m.negMag.position) / 3;
                            m.rigidBody.AddForce(Vector2.down * m.rigidBody.drag * m.rigidBody.mass - m.rigidBody.gravityScale * Vector2.down);
                        }
                        #endregion

                        Vector3 rotateVector = (m.magnetizedTo.transform.position - m.negMag.position).normalized;

                        if (rotateVector.y >= 0)
                            m.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Acos(rotateVector.x) * (180 / Mathf.PI) - 90));
                        else
                            m.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Acos(rotateVector.x) * (180 / Mathf.PI) - 90));
                    }
                }
                else if(_distance > snapOnRange + 3)
                {
                    if (!m.magnetActive)
                    {
                        m.magnetActive = true;
                    }
                        
                }
            }
        }
    }
}

