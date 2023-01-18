using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;

    [SerializeField] private float nextTimeToFire = 0f;
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;


    void Start()
    {
        currentAmmo = maxAmmo;
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
            
    }


    void Update()
    {
        if (isReloading)
            return;
        if (Input.GetKeyDown(KeyCode.R)&&currentAmmo <maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }
        //If you want a different input, change it here
        if (Input.GetButtonDown("Fire1"))
        {
            //Calls animation on the gun that has the relevant animation events that will fire
            if (currentAmmo == 0)
                return;
            gunAnimator.SetTrigger("Fire");
            
            
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire&&currentAmmo>0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        if (currentAmmo == 0)
            return;
        muzzleflash.Play();
        currentAmmo--;
            RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            //Bullet impact for dog
            if (hit.transform.name!="zombieDog"&& hit.transform.name != "FirstPersonPlayer")
            { 
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); 
            }
            
        }



    }
    
   IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");
        gunAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        gunAnimator.SetBool("Reloading", false);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);

        
    }

}
