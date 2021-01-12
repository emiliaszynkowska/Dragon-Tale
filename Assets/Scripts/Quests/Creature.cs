using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float health;
    public float distance = 5f;
    public GameObject player;
    public float attackDelay = 0.5f;
    public float attackWait = 2;
    public float range = 2;

    private float hitTimeout = 1;
    private float lastHitTime;


    private bool dead;
    private AnimationsController anim;
    private bool hunting = false;
    private GameObject prey;
    private bool attacking;

    private void Start()
    {
        anim = GetComponent<AnimationsController>();
    }

    private void Update()
    {
        if (hunting && Vector3.Distance(transform.position, prey.transform.position) > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, prey.transform.position, 0.2f);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        } else if (hunting && !attacking)
        {
            StartCoroutine(Attack());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log("Detected " + other.tag);
        if (other.CompareTag("Attack") && Time.time > lastHitTime + hitTimeout && !dead)
        {
            Debug.Log("Hit");
            lastHitTime = Time.time;
            StartCoroutine(TakeDamage());
            
        }
    }



    public IEnumerator TakeDamage()
    {
        if (health > 0)
        {
            anim.Hit();
            health -= 10;
            Debug.Log(health);
        }
        else
        {
            health = 0;
            StartCoroutine(Die());
        }
        //Animate("Hit");
        //soundManager.PlayRoar();
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator Die()
    {
        hunting = false;
        dead = true;
        GetComponent<BoxCollider>().enabled = false;
        anim.SetDead();
        Debug.Log("Die");
        switch (tag)
        {
            case "Spider":
                PlayerData.SpidersKilled += 1;
                break;
        }
        yield return new WaitForSeconds(3);
        Vector3 target = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
        yield return Move(target, 3);
        Destroy(gameObject);
    }


    public IEnumerator Move(Vector3 target, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = target;
    }

    public void Hunt(GameObject target)
    {
        hunting = true;
        prey = target;
    }

    public void StopHunting()
    {
        hunting = false;
    }

    IEnumerator Attack()
    {
        bool hit = false;
        attacking = true;
        hunting = false;
        yield return new WaitForSeconds(attackDelay);
        anim.Attack();
        if (Vector3.Distance(transform.position, prey.transform.position) <= distance + range) hit = true;
        switch (prey.tag)
        {
            case "Player" when hit:
                Debug.Log("Player Hit");
                break;
            case "Player":
                Debug.Log("Evaded");
                break;
            case "Soul" when hit:
                Debug.Log("Soul Hit");
                break;
        }
        hunting = true;
        yield return new WaitForSeconds(attackWait);
        attacking = false;
    }
}
