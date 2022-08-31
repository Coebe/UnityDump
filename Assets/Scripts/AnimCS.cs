using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCS : MonoBehaviour {
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //Vector3 rote = new Vector3(0, 0, 0);
        transform.Rotate(new Vector3(0, 36, 0) * Time.deltaTime);
    }
}
