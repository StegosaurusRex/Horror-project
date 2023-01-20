using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    
    private void Start()
    {
        myDoor = this.transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        myDoor.SetBool("Open", true);

        
    }
    private void OnTriggerExit(Collider other)
    {
        myDoor.SetBool("Close", true);
    }
}
