using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 8f;
    public float gravity = -9.81f;
    
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 15;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8;
        }

       
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed  *Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

       

    }
}
