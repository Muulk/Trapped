using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D playerBody;

    public Camera cam;
    public bool isActive;
    public bool canPickup;
    int delayOn;
    public bool holdingFlashlight;
    public GameObject flashlight;
    public Light2D flashlightBeam;
    public float minLightBrightness, maxLightBrightness, drainRate, maxInnerRadius, maxOuterRadius;
    public int playerHealth;
    public GameObject mainCamera;
    private Animation anim;
    public Text healthText;

    public SpriteRenderer spriteRend;
    public Sprite flashlightHold, gunHold;

    public AudioSource playerASource;
    public AudioClip hurt;


    Vector2 movement;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        canPickup = false;
        holdingFlashlight = false;
        delayOn = 1;
        anim = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
   

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKeyUp(KeyCode.E) && canPickup == true && delayOn == 1)
            {
                spriteRend.sprite = flashlightHold;

                flashlight.GetComponent<Rigidbody2D>().isKinematic = true;
                flashlight.transform.position = gameObject.transform.GetChild(0).position;
                flashlight.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                flashlight.transform.parent = gameObject.transform;
                flashlight.GetComponent<SpriteRenderer>().enabled = false;
                holdingFlashlight = true;
                Invoke("delayPickup", 0.25f);
            }
            if (Input.GetKeyUp(KeyCode.E) && holdingFlashlight == true && delayOn == -1)
            {
                holdingFlashlight = false;

                flashlight.transform.SetParent(null);
                flashlight.GetComponent<SpriteRenderer>().enabled = true;

                spriteRend.sprite = gunHold;

                Invoke("delayPickup", 0.25f);
            }

            flashlightBeam.intensity = Mathf.Clamp(flashlightBeam.intensity, minLightBrightness, maxLightBrightness);
            if (flashlightBeam.intensity > minLightBrightness)
            {
                flashlightBeam.intensity -= Time.deltaTime * (drainRate / 1000);
                flashlightBeam.pointLightInnerRadius -= Time.deltaTime * (drainRate / 500);
                flashlightBeam.pointLightOuterRadius -= Time.deltaTime * (drainRate / 500);

            }

        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("EndGame");
            Debug.Log("Player Dead");
        }

        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        healthText.text = "Health: " + playerHealth.ToString();
        
    }

    public void hurtSound()
    {
        playerASource.clip = hurt;
        playerASource.PlayOneShot(hurt);

    }

    void delayPickup()
    {
        delayOn = -delayOn;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Flashlight")
        {
            if (holdingFlashlight == false)
            {
                canPickup = true;

            }
        }
        if (other.tag == "Battery" && holdingFlashlight == true)
        {
            flashlightBeam.intensity = maxLightBrightness;
            flashlightBeam.pointLightInnerRadius = maxInnerRadius;
            flashlightBeam.pointLightOuterRadius = maxOuterRadius;
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Health")
        {
            if (playerHealth < 3)
            {
                playerHealth += 1;
                Destroy(other.gameObject);

            } else
            {
                Destroy(other.gameObject);
            }

        }
    }



    private void FixedUpdate()
    {
        if (isActive)
        {
            playerBody.MovePosition(playerBody.position + (movement * moveSpeed * Time.fixedDeltaTime));

            Vector2 lookDir = mousePos - playerBody.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            playerBody.rotation = angle;
        }

    }
}
