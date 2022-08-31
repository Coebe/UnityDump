using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public void collected()
    {
        FindObjectOfType<Player>().cherryCount();
        Destroy(gameObject);
    }
}
