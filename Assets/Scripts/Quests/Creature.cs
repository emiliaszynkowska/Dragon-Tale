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
    public float speed = 0.2f;
    public float attackDamage;

    private float hitTimeout = 1;
    private float lastHitTime;
    private bool walking = false;


    private bool dead;
    private AnimationsController anim;
    private Animator beetAnim;
    private bool hunting = false;
    private GameObject prey;
    private bool attacking;

    private void Start()
    {
        switch (tag)
        {
            case "Spider":
                anim = GetComponent<AnimationsController>();
                break;
            case "Beetle":
                beetAnim = GetComponent<Animator>();
                break;
        }
    }

    private void Update()
    {
        if (hunting && Vector3.Distance(transform.position, prey.transform.position) > distance)
        {
            if (CompareTag("Beetle"))
            {
                if (!walking) Walk();
                transform.LookAt(player.transform);
            }
            transform.position = Vector3.MoveTowards(transform.position, prey.transform.position, speed);
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
            switch (tag)
            {
                case "Spider":
                    anim.Hit();
                    break;
                case "Beetle":
                    beetAnim.Play("Take Damage", 0);
                    break;
            }
            health -= 10*PlayerData.Attack;
            Debug.Log(health);
        }
        if (health <= 0) 
        {
            //health = 0;
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
        Debug.Log("Die");
        switch (tag)
        {
            case "Spider":
                anim.SetDead();
                PlayerData.SpidersKilled += 1;
                break;
            case "Beetle":
                beetAnim.Play("Die");
                //beetAnim.PlayInFixedTime("Die", 0, 1f);
                
                PlayerData.BeetlesKilled += 1;
                break;
        }
        yield return new WaitForSeconds(3);
        Vector3 target = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
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
        float random = Random.value;
        switch (tag)
        {
            case "Spider":
                anim.Attack();
                break;
            case "Beetle" when random < 0.66f:
                beetAnim.Play("Stab Attack", 0);
                break;
            case "Beetle":
                beetAnim.Play("Smash Attack");
                break;
        }
        if (Vector3.Distance(transform.position, prey.transform.position) <= distance + range) hit = true;
        switch (prey.tag)
        {
            case "Player" when hit:
                PlayerData.Health -= attackDamage*PlayerData.Resistance;
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

    IEnumerator Walk()
    {
        walking = true;
        beetAnim.Play("Walk Forward In Place");
        yield return new WaitForSeconds(1);
        walking = false;
    }
}
