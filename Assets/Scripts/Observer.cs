using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    //! Reference to Player
    //! We are accessing transform component of the player.
    [SerializeField]
    public Transform m_player = null;   
    
    //! Indicate whether player is in range or not.
    bool m_IsPlayerInRange = false;

    //! When Player enters the visibility range of the avatar set the m_IsPlayerInRange flag to true.
    void OnTriggerEnter(Collider other)
    {
        if(other.transform == m_player) 
        {            
            m_IsPlayerInRange = true;                   
        }
    }

    //! When Player leaves the visibility range of the avatar set the m_IsPlayerInRange flag to false.
    void OnTriggerExit(Collider other)
    {         
       m_IsPlayerInRange = false;
    }

    //! Getter ti return the value of m_IsPlayerInRange
    public bool GetPlayerRangeFlag()
    {
        return m_IsPlayerInRange;
    }

}
