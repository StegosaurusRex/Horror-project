using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookV : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    [SerializeField] private Animator deathAnim;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (GameManager.GameIsPaused == false)
        {
            Cursor.visible = false;
            float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * MouseX);

        }
        if (DogExample.playerIsDead == true)
        {
            deathAnim.SetBool("IsDeadPlayer", true);
        }
    }
}


