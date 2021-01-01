using UnityEngine;

namespace Home
{
    public class MarkerController : MonoBehaviour
    {
        public GameManager gameManager;

        void OnTriggerEnter(Collider other)
        {
            gameManager.DragonScene();
        }
    
    }
}
