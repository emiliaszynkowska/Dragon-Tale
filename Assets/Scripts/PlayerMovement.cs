using System;
using System.Collections;
using Lair;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float movementSpeed;
    private float speed;
    private float gravity = 10;
    private Vector3 movement;
    private Vector3 velocity;
    public bool lava;
    public bool canMove;
    public bool cameraCheck;
    // Combat
    public float health;
    private float hitTimeout = 2;
    private float lastHitTime = -10;
    private float attackTimeout = 1;
    private float lastAttackTime = -10;
    public Collider attackCollider;
    // Objects
    public SoundManager soundManager;
    public Animator attackAnimator;
    public PlayerUI playerUI;
    private Camera cam;
    private CharacterController controller;
    
    public void Start()
    {
        cam = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        controller.enableOverlapRecovery = false;
        if (SceneManager.GetActiveScene().name == "Village")
        {
            if (PlayerData.VillageExit == 0)
            {
                transform.position = new Vector3(240.6f, 0.087f, 859.4f); //Spawn from Home
            } else if (PlayerData.VillageExit == 1)
            {
                transform.position = new Vector3(-32.8f, 1.9f, 839.4f); //Spawn from Lair
            }
        }
        if (PlayerData.Health > 0)
            health = PlayerData.Health;
        else
            health = 50;
    }

    void Update()
    {
        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        movement = transform.right * x + transform.forward * z;
        movement.y = 0;
        if (canMove)
            controller.Move(movement * speed * Time.deltaTime);

        // Sprint
        if (Input.GetKey("left shift"))
            speed = movementSpeed * 2;
        else
        {
            speed = movementSpeed;
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = 0;
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        // Lava Damage
        if (lava)
        {
            if (health > 0)
            {
                health -= 0.05f;
                playerUI.UpdateHealth(health);
            }
            else
            {
                health = 0;
                playerUI.UpdateHealth(health);
                playerUI.StartCoroutine("Die");
            }
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (attackCollider != null && Time.time > lastAttackTime + attackTimeout)
            {
                StartCoroutine(Attack());
            }
        }
        
        // Set Camera
        if (cameraCheck)
        {
            if (cam.transform.rotation.y > 0)
            {
                var rot = cam.transform.localRotation;
                cam.transform.Rotate(0, -0.45f, 0, Space.Self);
            }

            if (cam.transform.position.y < 2.4f)
            {
                var pos = cam.transform.localPosition;
                cam.transform.localPosition = new Vector3(pos.x, pos.y + 0.005f, pos.z);
            }
        }
    }

    public void SetCameraPosition(Vector3 p)
    {
        cam.transform.localPosition = p;
    }

    public void SetCameraRotation(Quaternion r)
    {
        cam.transform.localRotation = r;
    }
    
    public void SetLocalCameraPosition(Vector3 p)
    {
        cam.transform.localPosition = p;
    }

    public void SetLocalCameraRotation(Quaternion r)
    {
        cam.transform.localRotation = r;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetMovementSpeed(float f)
    {
        movementSpeed = f * 20;
    }

    public void Fight()
    {
        playerUI.gameObject.SetActive(true);
        playerUI.UpdateHealth(health);
    }

    public void EndFight()
    {
        playerUI.gameObject.SetActive(false);
    }

    public IEnumerator Attack()
    {
        lastAttackTime = Time.time;
        attackAnimator.SetTrigger("Attack");
        soundManager.PlayAttack();
        yield return new WaitForSeconds(1);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Beetle") || other.gameObject.CompareTag("Spider")) && Time.time > lastHitTime + hitTimeout)
        {
            if (other.gameObject.GetComponent<BossMovement>() != null && other.gameObject.GetComponent<BossMovement>().attack)
            {
                lastHitTime = Time.time;
                if (health > 0)
                {
                    health -= 10;
                    playerUI.UpdateHealth(health);
                }
                else
                {
                    health = 0;
                    playerUI.UpdateHealth(health);
                    playerUI.StartCoroutine("Die");
                }
            }
            else
            {
                lastHitTime = Time.time;
                if (health > 0)
                {
                    health -= 5;
                    playerUI.UpdateHealth(health);
                }
                else
                {
                    health = 0;
                    playerUI.UpdateHealth(health);
                    playerUI.StartCoroutine("Die");
                }
            }
        }
        else if (other.gameObject.CompareTag("Platform"))
        {
            lava = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            if (soundManager.audioSource.clip.name.Equals("Boss"))
                lava = true;
        }
    }
    
}

