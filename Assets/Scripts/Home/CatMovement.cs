using UnityEngine;

namespace Home
{
    public class CatMovement : MonoBehaviour
    {
        private Animator animator;
        public GameObject player;
        public SoundManager soundManager;
        private float meowTimeout = 3;
        private float lastMeowTime = -10;
    
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            var catpos = transform.localPosition;
            var playerpos = player.transform.localPosition;
            playerpos.y = 0;
            transform.LookAt(playerpos);
            if (playerpos.magnitude - catpos.magnitude > 2 || catpos.magnitude - playerpos.magnitude > 2)
            {
                AnimateWalk();
                transform.Translate(new Vector3(playerpos.x - catpos.x, 0, playerpos.z - catpos.z) * 0.005f, Space.World);
            }
            else
            {
                AnimateIdleB();
            }
        }

        public void AnimateIdleA()
        {
            animator.SetTrigger("IdleA");
        }
    
        public void AnimateIdleB()
        {
            animator.SetTrigger("IdleB");
        }
    
        public void AnimateWalk()
        {
            animator.SetTrigger("Walk");
        }
    
        public void AnimateRun()
        {
            animator.SetTrigger("Run");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Equals("Player") && (Time.time > lastMeowTime + meowTimeout))
            {
                lastMeowTime = Time.time;
                soundManager.PlayMeow();
            }
        }
    }
}
