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
        public bool following;
        public bool walking;
        public float distance = 1;
        public float buffer = 1;
        public float speed = 0.01f;

        public void Fight()
        {
            StartCoroutine(Follow());
            StartCoroutine(Attack());
        }

        IEnumerator Follow()
        {
            following = true;
            walking = false;
            bool idle = true;
            float actualDistance;
            while (following)
            {
                actualDistance = Vector3.Distance(transform.position, dragon.transform.position);
                if ((!idle && actualDistance > distance) || (idle && actualDistance > distance + buffer))
                {
                    walking = true;
                    idle = false;
                    transform.LookAt(dragon.transform);
                    transform.position = Vector3.MoveTowards(transform.position, dragon.transform.position, speed);
                    transform.position = new Vector3(transform.position.x, 11.3f, transform.position.z);
                } else
                {
                    walking = false;
                    idle = true;
                }
                yield return null;
            }
        }
        
        IEnumerator Attack()
        {
            while (true)
            {
                soundManager.PlayAttack();
                anim.Play("Male Attack 1", 0);
                yield return new WaitForSeconds(1);
                anim.Play("Male_Sword_Walk", 0);
                yield return new WaitForSeconds(5);
            }
        }
        
    }

}