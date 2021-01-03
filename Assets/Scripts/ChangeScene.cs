using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomeFire
{
    public class ChangeScene : MonoBehaviour
    {
        public String scene;
        void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(scene);
        }
    
    }
}
