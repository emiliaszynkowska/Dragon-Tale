using System.Collections;
using System.Linq;
using UnityEngine;

namespace Home
{
    public class DragonMovement : MonoBehaviour
    {
        public Animator animator;
        public GameObject fire;

        public void Animate(string a)
        {
            animator.SetTrigger(a);
        }

        public IEnumerator Fire()
        {
            fire.SetActive(true);
            fire.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSecondsRealtime(3);
            fire.SetActive(false);
        }

        public IEnumerator FlyUp()
        {
            foreach (var i in Enumerable.Range(0, 300))
            {
                yield return null;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(45.5f, 15, 32), Time.unscaledDeltaTime * 2);
            }
        }

    }
}
