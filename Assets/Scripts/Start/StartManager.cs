using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Start
{
    public class StartManager : MonoBehaviour
    {
        public GameObject dragon;
        public SoundManager soundManager;
        public Fade fade;
        public GameObject settingsPanel;
        public Slider volume;
        private bool isMute;
        private bool isSettings;

        void Start()
        {
            dragon.GetComponent<Animator>().SetTrigger("FlyIdle");
        }

        public void NewGame()
        {
            soundManager.PlayClick();
            StartCoroutine(NewGameCoroutine());
        }

        public void Settings()
        {
            isSettings = !isSettings;
            soundManager.PlayClick();
            if (isSettings)
                settingsPanel.SetActive(true);
            else
                settingsPanel.SetActive(false);
        }

        public void Volume()
        {
            soundManager.audioSource.volume = volume.value;
        }

        public void Mute()
        {
            soundManager.PlayClick();
            isMute = !isMute;
            if (isMute)
                soundManager.audioSource.mute = true;
            else
                soundManager.audioSource.mute = false;
        }

        public IEnumerator NewGameCoroutine()
        {
            fade.StartCoroutine("BlackIn");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Scenes/Home");
        }

    }
}
