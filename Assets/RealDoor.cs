using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealDoor : MonoBehaviour
{
    public GameObject realDoor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        realDoor.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        realDoor.SetActive(false);
    }
}
