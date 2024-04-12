using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    // The projectile prefab to be spawned.
    public GameObject projectile;

    // A reference to the firepoint location
    public Transform firePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses the fire button
        if (Input.GetButtonDown("Fire1"))
        {
            // Call the Shoot method
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the firepoint position
        GameObject firedProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);

        // Destroy the projectile after 3 seconds
        Destroy(firedProjectile, 3f);
    }
}
