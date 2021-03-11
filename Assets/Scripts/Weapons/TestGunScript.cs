using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public int damage = 10;
    public float range = 100f;
    public float force = 30f;
    public Camera fpsCam;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            CharacterStats target = hit.transform.GetComponent<CharacterStats>();
            if(target != null)
            {
                target.TakeDamage(damage);  
            }
            
            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }
    }
}
