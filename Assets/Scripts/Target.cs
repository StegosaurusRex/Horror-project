using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Target : MonoBehaviour
{
    public static bool isDead=false;
    GameObject dogDead;
    // Start is called before the first frame update

    public float health = 50f;

    private void Start()
    {
        
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("DEAD");
    }

}