using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [HideInInspector] public OjectsInCreate ojectsInCreate;

    //InteractableInfo
    public float sphereCastRadius = 0.5f;
    public int interactableLayerIndex;
    private Vector3 raycastPos;
    private GameObject lookObject;
    private PhyObject physicsObject;
    private Camera mainCamera;

    //Pickup
    [SerializeField] private Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;

    //ObbjectFollow
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;
    private float currentSpeed = 0f;
    private float currentDist = 0f;

    //Rotation
    public float rotationSpeed = 100f;
    Quaternion lookRot;




    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pickupParent.position, 0.5f);
    }
    void Update()
    {
        raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hit, maxDistance, 1 << interactableLayerIndex))
        {
            lookObject = hit.collider.transform.root.gameObject;
        }
        else
        {
            lookObject = null;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentlyPickedUpObject == null)
            {
                if (lookObject != null)
                {
                    PickUpObject();
                }
            }

            

        }
        else if (Input.GetButtonUp("Fire1"))
        {
           
                BreakConnection();
           
            
        }



        if (currentlyPickedUpObject != null && currentDist > maxDistance) BreakConnection();
    }

    public void PickUpObject()
    {
        physicsObject = lookObject.GetComponentInChildren<PhyObject>();
        currentlyPickedUpObject = lookObject;
        pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
        pickupRB.constraints = RigidbodyConstraints.FreezeRotation;
        physicsObject.playerInteractions = this;
        StartCoroutine(physicsObject.PickUp());
    }

    private void FixedUpdate()
    {
        if (currentlyPickedUpObject != null)
        {
            currentDist = Vector3.Distance(pickupParent.position, pickupRB.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = pickupParent.position - pickupRB.position;
            pickupRB.velocity = direction.normalized * currentSpeed;
            //Rotation
            lookRot = Quaternion.LookRotation(mainCamera.transform.position - currentlyPickedUpObject.transform.position);
            lookRot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            pickupRB.MoveRotation(lookRot);
        }
    }

    public void BreakConnection()
    {
        

        pickupRB.constraints = RigidbodyConstraints.None;
        currentlyPickedUpObject = null;
        physicsObject.pickedUp = false;
        currentDist = 0;








        //pickupRB.constraints = RigidbodyConstraints.None;
        //currentlyPickedUpObject = null;
        //physicsObject.pickedUp = false;
        //currentDist = 0;
    }
        


 



}
