using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScare : MonoBehaviour
{
    public AudioSource forScare;
    // Start is called before the first frame update
    void Start()
    {
        
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
    private void OnTriggerEnter(Collider other)
    {
        
        forScare.Play();
        StartCoroutine(ExecuteAfterTimeCollision(1f));
    }
}

