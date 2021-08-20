using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    //! Reference to Observer Script
    Observer m_myObserver = null;

    //! Reference to Animator
    Animator m_Animator = null;

    //! Reference to Player transform
    [SerializeField]
    public Transform m_player = null;

    //! Reference to Avatar transform
    [SerializeField]
    public Transform m_avatar = null;

    [SerializeField]
    float avatarWalkSpeed = 0.021f;

    //! Start is called before the first frame update
    void Start()
    {
        m_myObserver = GetComponentInChildren<Observer>();
        m_Animator = GetComponent<Animator>();
    }

    //! Update is called once per frame
    void Update()
    {

        if (m_myObserver.GetPlayerRangeFlag() == true)
        {
            m_Animator.SetBool("IsWalking", true);                   
            Vector3 direction = m_avatar.position - m_player.position;
            direction.y = 0;
            direction = Vector3.Normalize(direction);
            m_avatar.rotation = Quaternion.Euler(direction);
            m_avatar.Translate(transform.forward * avatarWalkSpeed);       

        }
        else if(m_myObserver.GetPlayerRangeFlag() == false)
        {
            m_Animator.SetBool("IsWalking", false);
            Vector3 lookAt = m_player.position;
            transform.LookAt(lookAt);
        }
        else
        {
            m_Animator.SetBool("IsWalking", false);
        }
    }

}
