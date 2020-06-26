using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoxBoyFollow : MonoBehaviour
{

    public GameObject target = null;
    private NavMeshAgent nma = null;
    [SerializeField]
    private bool follow = true;

    // Start is called before the first frame update
    void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        follow = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(follow == false)
        {
            nma.SetDestination(transform.position);
        }
        else if(follow  == true)
        {
            nma.SetDestination(target.transform.position);
        }

        



    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "BoxBoyArea")
        {
            follow = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BoxBoyArea")
        {
            follow = false;
        }
    }
}
