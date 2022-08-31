using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public void collected()
    {
        FindObjectOfType<Player>().gemCount();
        Destroy(gameObject);
    }
}
