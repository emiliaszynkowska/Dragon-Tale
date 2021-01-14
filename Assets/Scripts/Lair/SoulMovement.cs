using System;
using System.Collections;
using UnityEngine;

namespace Lair
{
    public class SoulMovement : MonoBehaviour
    {
        public SoundManager soundManager;
        public GameObject dragon;
        public Animator anim;
        public BossMovement bossMovement;
        public bool following;
        public float distance = 1;
        public float buffer = 1;
        public float speed = 0.01f;
        public bool fighting;
        private bool attacking;
        private bool dead = false;

        private void Start()
        {
            StartCoroutine(LateStart());
        }

        IEnumerator LateStart()
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(PlayerData.SoulSaved);
        }

        public void Fight()
        {
            fighting = true;
            StartCoroutine(Follow());
            StartCoroutine(Attack());
        }

        IEnumerator Follow()
        {
            following = true;
            bool idle = true;
            float actualDistance;
            while (following && !dead)
            {
                actualDistance = Vector3.Distance(transform.position, dragon.transform.position);
                if ((!idle && actualDistance > distance) || (idle && actualDistance > distance + buffer) && !attacking)
                {

                    anim.Play("Male_Sword_Walk", 0);
                    idle = false;
                    transform.LookAt(dragon.transform);
                    transform.position = Vector3.MoveTowards(transform.position, dragon.transform.position, speed);
                    transform.position = new Vector3(transform.position.x, 11.3f, transform.position.z);
                } else if (!attacking)
                {
                    idle = true;
                    anim.Play("Male Idle", 0);
                }
                yield return null;
            }
        }
        
        IEnumerator Attack()
        {
            yield return new WaitForSeconds(5);
            while (!dead)
            {
                attacking = true;
                soundManager.PlayAttack();
                anim.Play("Male Attack 1", 0);
                bossMovement.SoulHit();
                yield return new WaitForSeconds(1);
                attacking = false;
                yield return new WaitForSeconds(5);
                if (UnityEngine.Random.value < 0.1f)
                {
                    dead = true;
                    anim.Play("Male Die", 0);
                }
            }
        }
        
    }

}