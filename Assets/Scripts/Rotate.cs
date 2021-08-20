using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    //! Reference to Observer Script
    Observer m_myObserver = null;

    //! Avatar direction flag.
    bool m_faceForward = false;

    //! Start is called before the first frame update
    void Start()
    {
        m_myObserver = GetComponentInChildren<Observer>();
    }

    //! Update is called once per frame
    void Update()
    {
        if (m_myObserver.GetPlayerRangeFlag() == true)
        {   

            if(m_faceForward == false)
            {
                m_faceForward = !m_faceForward;
                transform.rotation *= Quaternion.Euler(0, 180f, 0);
            }          

        }
        else if(m_myObserver.GetPlayerRangeFlag() == false)
        {
            if( m_faceForward == true )
            {
                m_faceForward = !m_faceForward;
                transform.rotation *= Quaternion.Euler(0, 180f, 0);
            }

        }
    }

}
