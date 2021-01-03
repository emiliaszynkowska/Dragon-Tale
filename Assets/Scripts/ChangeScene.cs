using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HomeFire
{
    public class ChangeScene : MonoBehaviour
    {

        public RawImage img;
        public Fade fade;
        public string scene;
        void OnTriggerEnter(Collider other)
        {
            StartCoroutine(Switch());
        }

        IEnumerator Switch()
        {
            fade.Out(img);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(scene);
        }
    }
}
