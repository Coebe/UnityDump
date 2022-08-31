using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    float movementFactor;
    const float tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        // continually growing over time
        float cycle = Time.time / period;

        // get the sin wave flip-flop
        float sinwaveVal = Mathf.Sin(cycle * tau);
        // calculate the val to [0, 1]
        movementFactor = Mathf.Abs(sinwaveVal);

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}
