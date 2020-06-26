using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Mouselook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;


    RaycastHit hit;
    GameObject grabbedOBJ;
    public Transform grabPos;
    public float maxDistance = 5;
    public LayerMask smallObj;

    public bool holding;
    private GameObject temp;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {


        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.green);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance) && hit.transform.GetComponent<Rigidbody>())
            {
                grabbedOBJ = hit.transform.gameObject;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (holding == true)
            {
                grabbedOBJ.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                holding = false;
                grabbedOBJ = null;
            }

        }


        HoldingFunction();


        if (grabbedOBJ)
        {
            grabbedOBJ.GetComponent<Rigidbody>().velocity = 20 * (grabPos.position - grabbedOBJ.transform.position);
            holding = true;
        }

    }

    void HoldingFunction()
    {
        if (holding == true)
        {
            grabbedOBJ.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        }
        else if (holding == false)
        {

        }
        if (holding == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Rotateingg");
        }

        else if (!Input.GetKeyDown(KeyCode.E))
        {

        }
    }
}


