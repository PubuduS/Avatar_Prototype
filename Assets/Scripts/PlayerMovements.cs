using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    //! Instead of instant directional changes we slow it down to make it look natural.
    //! Angle in radians the character turn per seconds.
    public float turnSpeed = 20f;

    //! Reference to the Animator.
    //! Used to change between animation states.
    Animator m_Animator;

    //! Reference to the Rigidbody.
    //! Use to move character while interacting with physics.
    Rigidbody m_Rigidbody;

    //! Holds x, y, z axies
    Vector3 m_Movement;

    //! Quaternions stores rotation data.
    //! They get around some problems with storing rotations as a 3D vector.
    //! Quaternion.identity is the default value.
    Quaternion m_Rotation = Quaternion.identity;

    [SerializeField]
    public float m_SprintSpeed = 20f;
    bool isRunning = false;
    bool isWalking = false;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {  
      
        if( Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) )
        {
            isWalking = false;
            isRunning = true;
            PlayerSprint();
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isWalking = true;
            isRunning = false;
            m_Animator.SetBool("IsRunning", isRunning);
        }
        else
        {
            isWalking = false;
            isRunning = false;
            m_Animator.SetBool("IsRunning", isRunning);
            m_Animator.SetBool("IsWalking", isWalking);
        }

        MovementsAndRotation();
    }

    //! This method allows you to apply root motion as you want, which means that movement and rotation can be applied separately.
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    //! Handle player movements and rotations.
    void MovementsAndRotation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);

        // To prevent character move faster in diagonally. 
        // The movement vector is made up of two numbers that can have a maximum value of 1. 
        // If they both have a value of 1, the magnitude of the vector will be greater than 1. (Pythagoras’ theorem.)
        // This means character will move at 1.414 faster at diagonally. 
        // Normalize() function keep magnitude at 1.
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        // If we detect either inputs which means character is walking.
        //isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
    
        

        // Obtain the forward vector and change m_Movements with new data sets.
        // 3rd param is change in angle
        // 4th param is change in magnitude.
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void PlayerSprint()
    {        
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * m_SprintSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * m_SprintSpeed;
        transform.Translate(xValue, 0, zValue);
        m_Animator.SetBool("IsRunning", isRunning);
    }
}
