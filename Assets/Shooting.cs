using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Player player;

    public float bulletForce = 40f;
    private bool active, canShoot, shootDelay;
    public float shootDelayTime;

    public AudioClip shot;
    public AudioSource aSource;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        active = player.isActive;

        if (active == true && player.holdingFlashlight == false && shootDelay == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
                //shootCooldown();
            }
        }

    }

    void shootCooldown()
    {
        shootDelay = true;
        Invoke("shootCooldownTwo", shootDelayTime );
    }

    void shootCooldownTwo()
    {
        shootDelay = false;
    }
    void Shoot()
    {
        GameObject bullet =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        aSource.clip = shot;
        aSource.PlayOneShot(shot);
    }
}
