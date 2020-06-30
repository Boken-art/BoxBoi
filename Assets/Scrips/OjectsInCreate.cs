using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjectsInCreate : MonoBehaviour
{
    [HideInInspector] public PlayerInteractions playerInteractions;

    private bool inBox = true;

    GameObject OBJinBox;
    public Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        inBox = false;
        rb = GetComponent<Rigidbody>();
        
        

    }

   

    


    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Boxed")
        {



            inBox = true;
            //other.GetComponent<Rigidbody>().drag = 20;
            //other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;



        }



    }

    void OnTriggerExit(Collider other)
    {
        inBox = false;
    }

    void Update()
    {
        if (inBox == false)
        {
            Debug.Log("Nothing In");
        }
        else if (inBox == true)
        {
            Debug.Log("ItsIN");
        }
    }
            

        
   


    public void ObjectInbox()
    {
        
       
    }
}
