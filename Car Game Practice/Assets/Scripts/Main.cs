using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    //[SerializeField]
    //Collider collider;

    Rigidbody rb;

    [SerializeField]
    Transform RRwheel;
    [SerializeField]
    Transform RLwheel;
    [SerializeField]
    Transform FRwheel;
    [SerializeField]
    Transform FLwheel;

    [SerializeField]
    float carMass = 1;
    float force = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = carMass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GroundDetection(RRwheel) && GroundDetection(RLwheel) && GroundDetection(FRwheel) && GroundDetection(FLwheel))
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 forceVector = transform.right * force;

                rb.AddForceAtPosition(forceVector, RRwheel.position);
                rb.AddForceAtPosition(forceVector, RLwheel.position);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Vector3 forceVector = -transform.right * force;

                rb.AddForceAtPosition(forceVector, RRwheel.position);
                rb.AddForceAtPosition(forceVector, RLwheel.position);
            }


        }

    }

    bool GroundDetection(Transform wheel)
    {
        if(Physics.Raycast(wheel.position, Vector3.down, wheel.localScale.y + 0.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
