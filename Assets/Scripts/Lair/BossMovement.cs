using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lair
{
    public class BossMovement : MonoBehaviour
    {
        // Variables
        public bool fight;
        public bool attack;
        public float health;
        private float hitTimeout = 2;
        private float lastHitTime = -10;
        // Objects
        public LairManager lairManager;
        public SoundManager soundManager;
        public Animator animator;
        public BossUI bossUI;
        public GameObject fire;
        public GameObject player;

        void Start()
        {
            Animate("Idle");
        }

        void FixedUpdate()
        {
            var dragonpos = transform.localPosition;
            var playerpos = player.transform.localPosition;
            // Look at player
            playerpos.y = dragonpos.y;
            transform.LookAt(playerpos);
            // Move towards player
            if (fight && !attack && Mathf.Abs(dragonpos.magnitude - playerpos.magnitude) > 5)
            {
                transform.Translate(new Vector3(playerpos.x - dragonpos.x, 0, playerpos.z - dragonpos.z) * 0.0005f,
                    Space.World);
            }
        }

        public void Animate(string a)
        {
            animator.SetTrigger(a);
        }

        public void DisableHealth()
        {
            bossUI.gameObject.SetActive(false);
        }

        public void ClawAttack()
        {
            Animate("ClawAttack");
            PlayerData.Health -= 5*PlayerData.Resistance;
            soundManager.PlayClawAttack();
        }

        public void FlameAttack()
        {
            Animate("Scream");
            //Damage is done during the animation
            soundManager.PlayRoar();
            StartCoroutine(Fire());
        }

        public IEnumerator TakeDamage()
        { 
            Animate("Hit");
            soundManager.PlayRoar();
            yield return new WaitForSeconds(1.5f);
        }

        public IEnumerator Die()
        {
            fight = false;
            Animate("Die");
            yield return new WaitForSeconds(2.1f);
            bossUI.gameObject.SetActive(false);
            lairManager.StartCoroutine("EndBattle");
        }

        public IEnumerator Fire()
        {
            fire.SetActive(true);
            fire.GetComponent<ParticleSystem>().Play();
            PlayerData.Health -= 10*PlayerData.Resistance;
            yield return new WaitForSecondsRealtime(3);
            fire.SetActive(false);
        }

        public IEnumerator Fight()
        {
            bossUI.gameObject.SetActive(true);
            bossUI.maxHealth = health;
            bossUI.UpdateHealth(health);
            while (true)
            {
                yield return new WaitForSeconds(10);
                // Attack
                if (fight)
                {
                    var choice = Random.Range(0, 2);
                    switch (choice)
                    {
                        case (0):
                            attack = true;
                            ClawAttack();
                            yield return new WaitForSeconds(1);
                            attack = false;
                            break;
                        case (1):
                            attack = true;
                            FlameAttack();
                            yield return new WaitForSeconds(3);
                            attack = false;
                            break;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Attack") && Time.time > lastHitTime + hitTimeout)
            {
                lastHitTime = Time.time;
                StartCoroutine(TakeDamage());
                if (health > 0)
                {
                    health -= 10*PlayerData.Attack;
                    bossUI.UpdateHealth(health);
                }
                if (health <= 0)
                {
                    //health = 0;
                    bossUI.UpdateHealth(health);
                    StartCoroutine(Die());
                }
            }
        }
    }
}
