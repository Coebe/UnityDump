using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig : MonoBehaviour
{
    Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        // let the UI always looking at camera 
        transform.LookAt(cam.transform.position);


    }
}
