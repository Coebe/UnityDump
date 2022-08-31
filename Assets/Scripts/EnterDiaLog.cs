using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDiaLog : MonoBehaviour
{
    public GameObject enterDoor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enterDoor.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enterDoor.SetActive(false);
        }
    }
}
