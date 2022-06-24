using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int enemyHealth;
    public GameObject battery;
    public GameObject health;
    private Animation anim;
    private bool canAttack = true;
    private float timeBetweenAttack, startTimeBetweenAttack, timeBetweenSound, startTimeBetweenSound;
    private bool oneShot, canSound = false;

    public AudioSource zombSource;
    public AudioClip zombClip;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 3;
        anim = gameObject.GetComponent<Animation>();
        timeBetweenAttack = 0f;
        startTimeBetweenAttack = 1f;
        startTimeBetweenSound = 10f;
        timeBetweenSound = 0f;

    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Battery" || collision.gameObject.tag == "Health")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Character")
        {
            if (canAttack)
            {
                collision.gameObject.GetComponent<Player>().playerHealth -= 1;
                collision.gameObject.GetComponent<Player>().hurtSound();
                GameObject camera = GameObject.Find("Main Camera");
                canAttack = false;
                timeBetweenAttack = startTimeBetweenAttack;


            }
        }
    }
    void Update()
    {
        anim.Play("EnemyWalk");

        if (enemyHealth <= 0)
        {
            float randBattery = Random.Range(0, 100);
            float randHealth = Random.Range(0, 100);
            if (randBattery <= 10)
            {
                Instantiate(battery, transform.position, Quaternion.identity);
            } else
            {
                if (randHealth <= 25)
                {
                    Instantiate(health, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);

        }

        if (canAttack == false)
        {
            if (timeBetweenAttack <= 0f)
            {
                canAttack = true;
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }
        }

        if (canSound == false)
        {
            if (timeBetweenSound <= 0f)
            {
                canSound = true;
                playSound();
                timeBetweenSound = startTimeBetweenSound;

            }
            else
            {
                timeBetweenSound -= Time.deltaTime;
            }
        }

    }

    private void playSound()
    {
        if (canSound == true)
        {
            zombSource.clip = zombClip;
            zombSource.PlayOneShot(zombClip);
            canSound = false;
        }

    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.up) * 3f, 3f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 3, Color.red);

        if (hit.collider.tag == "Character")
        {
            Debug.Log("hit player");
        }
    }
}
