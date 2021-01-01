using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomeFire
{
    public class MarkerController : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene("Village");
        }
    
    }
}
