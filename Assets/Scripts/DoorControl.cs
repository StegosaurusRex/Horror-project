using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    public AudioClip closeDoor;
    public AudioClip openDoor;
    AudioSource closeSound;
    AudioSource openSound;

    private void Start()
    {
        closeSound = gameObject.GetComponent<AudioSource>();
        openSound = gameObject.GetComponent<AudioSource>();
        myDoor = this.transform.parent.GetComponent<Animator>();
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        closeSound.PlayOneShot(closeDoor, 1F);
        
        // Code to execute after the delay
    }
    IEnumerator ExecuteAfterTimeCollision(float time)
    {
        yield return new WaitForSeconds(time);
        
        Destroy(gameObject);
        // Code to execute after the delay
    }
    private void OnTriggerEnter(Collider other)
    {
        myDoor.SetBool("Open", true);
        openSound.PlayOneShot(openDoor, 1F);

    }
    private void OnTriggerExit(Collider other)
    {
        myDoor.SetBool("Close", true);
        StartCoroutine(ExecuteAfterTime(1));
        StartCoroutine(ExecuteAfterTimeCollision(1.6f));
        

    }
}
