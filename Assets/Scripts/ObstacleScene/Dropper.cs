using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Time-time.html
// TODO: random time drop over player 

public class Dropper : MonoBehaviour
{
    public float deltaTime = 3f;

    new MeshRenderer renderer;
    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > deltaTime)
        {
            renderer.enabled = true;
            rigidbody.useGravity = true;
            Debug.Log("There hava past 3 seconds.");
        }
    }
}
