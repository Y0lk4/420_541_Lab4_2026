using System;
using System.Data;
using UnityEditor.Callbacks;
using UnityEditorInternal;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletMaxImpulse = 10.0f;
    public float maxChargeTime = 3.0f;
    private float chargeTime = 0.0f;
    private bool isCharging = false;

    // public gunState state;

    // public enum gunState
    // {
    //     Charging,
    //     ChargeRelease,
    //     Resting
    // }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            chargeTime = 0.0f;
        }
        if (Input.GetButton("Fire1"))
        {
            isCharging = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ShootBullet();
            isCharging = false;
        }

        // StateHandler();
    }

    // public void StateHandler()
    // {
    //     if (Input.GetButton("Fire1"))
    //     {
    //         state = gunState.Charging;
    //         chargeTime += Time.deltaTime;
    //         chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime);
    //         isCharging = true;
    //     }
    //     else if (Input.GetButtonUp("Fire1"))
    //     {
    //         state = gunState.ChargeRelease;
    //         ShootBullet();
    //     }
    //     else if (state != gunState.ChargeRelease )
    //     {
    //         state = gunState.Resting;
    //     }
    // }


    void ShootBullet()
    {
            // notes to help me understand
            // create a new game object
            // that new object is a bullet and is actually copying the prefab of the bullet
            // instantiate takes the gameobject, and then its position in vector3 and its quaternion rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            //get the rigid body of the bullet we just made
            Rigidbody rb = bullet.GetComponent<Rigidbody>();


            // get the ratio of how much u charged
            float chargeRatio = chargeTime / maxChargeTime;

            // makes the force that i applied into the bullet the force scaled to the amount charged
            float bulletImpulse = bulletMaxImpulse * chargeRatio;

            //make the force/impulse we just made to be applied or added into the bullet
            //we take the forward of the bulletSpawnPoint and add the force to that direction thus x.forward * bulletImpulse.
            rb.AddForce(bulletSpawnPoint.forward * bulletImpulse, ForceMode.Impulse);

            // state = gunState.Resting;
        

    }
}
