using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinScare : MonoBehaviour
{
    public GameObject mannequnBefore1;
    public GameObject mannequnBefore2;
    public GameObject mannequnAfter1;
    public GameObject mannequnAfter2;
    public AudioClip scareSound;
    AudioSource forScare;
    // Start is called before the first frame update
    void Start()
    {
        forScare = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ExecuteAfterTimeCollision(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
        // Code to execute after the delay
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(mannequnBefore1);
        Destroy(mannequnBefore2);
        mannequnAfter1.SetActive(true);
        mannequnAfter2.SetActive(true);
        forScare.PlayOneShot(scareSound, 1F);
        StartCoroutine(ExecuteAfterTimeCollision(1f));
    }
}
