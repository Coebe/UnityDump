using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rotateValue = 1f;
    public float thrustSpeed = 1f;

    Rigidbody rg;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rg.AddRelativeForce(Vector3.up * Time.deltaTime * thrustSpeed);
        }
    }

    void ProcessRotate()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector3.forward;
            ApplyRotate(direction);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector3.back;
            ApplyRotate(direction);
        }

    }

    void ApplyRotate(Vector3 direction)
    {
        rg.freezeRotation = true;   // freeze rotation when key pressed 'A' or 'left arrow'
        transform.Rotate(direction * rotateValue * Time.deltaTime);
        rg.freezeRotation = false;  // unfressze rotation when key release 
    }
}
