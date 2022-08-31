using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDark : MonoBehaviour
{
    public GameObject fakeDoor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        fakeDoor.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        fakeDoor.SetActive(false);
    }
}
