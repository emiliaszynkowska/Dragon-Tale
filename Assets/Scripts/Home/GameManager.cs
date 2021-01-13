﻿using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Linq.Enumerable;

namespace Home
{
    public class GameManager : MonoBehaviour
    {
        public UIManager uiManager;
        public SoundManager soundManager;
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;
        public CatMovement catMovement;
        public DragonMovement dragonMovement;
        public Material skyMaterial;
        public GameObject fog;
        public Camera cam;
        public Fade fade;

        void Start()
        {
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            StartTutorial();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Menu();
        }
        
        public void Menu()
        {
            uiManager.Menu();
        }

        // Tutorial
        void StartTutorial()
        {
            uiManager.uiBar.SetActive(false);
            uiManager.controls.SetActive(false);
            playerMovement.cameraCheck = true;
            playerRotation.enabled = false;
            uiManager.SetTextBoxBig("Good morning! \nIt's time to wake up. \n \nUse the mouse to look around and the WASD keys to move.");
            StartCoroutine(WaitForStart());
        }
    
        IEnumerator WaitForStart()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            soundManager.PlayClick();
            uiManager.UnsetTextBoxBig();
            uiManager.controls.SetActive(true);
            playerRotation.enabled = true;
            playerMovement.canMove = true;
            playerMovement.cameraCheck = false;
            yield return null;
            playerMovement.SetCameraPosition(new Vector3(0, 1.5f, 0));
        }

        // Cutscene
        public void DragonScene()
        {
            soundManager.PlayClick();
            uiManager.marker.SetActive(false);
            fog.SetActive(false);
            RenderSettings.skybox = skyMaterial;
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            catMovement.gameObject.SetActive(false);
            dragonMovement.gameObject.SetActive(true);
            playerMovement.SetCameraPosition(new Vector3(0, 0.5f, 0));
            playerMovement.SetCameraRotation(Quaternion.Euler(0, 0, 0));
            soundManager.StopMusic();
            soundManager.PlayFight();
            StartCoroutine(DragonAnimation());
        }

        IEnumerator DragonAnimation()
        {
            // Dragon Lands
            dragonMovement.Animate("FlyIdle");
            yield return new WaitForSeconds(1);
            dragonMovement.Animate("Land");
            yield return new WaitForSeconds(4);
            // Dragon Talks
            uiManager.SetTextBox("You. Don't move any further!");
            dragonMovement.Animate("Scream");
            soundManager.PlayRoar();
            yield return new WaitForSeconds(3.5f);
            dragonMovement.Animate("Idle");
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox("I am Yvryr, mighty dragon of this land. Generations of dragons before me have ruled this place.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox("Human, bow down. It is only right to show your respect to a noble dragon such as me.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox("Such audacity! How dare you mock me? Those who do not respect me must be destroyed.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            uiManager.UnSetTextBox();
            soundManager.PlayClick();
            dragonMovement.Animate("Scream");
            soundManager.PlayRoar();
            StartCoroutine(dragonMovement.Fire());
            yield return new WaitForSeconds(2);
            // Dragon Leaves
            dragonMovement.Animate("TakeOff");
            foreach (var i in Range(0, 1000))
            {
                yield return null;
                playerMovement.SetCameraPosition(Vector3.MoveTowards(cam.transform.localPosition, new Vector3(0, 10, 0), Time.deltaTime * 2));
            }
            yield return new WaitForSeconds(2);
            uiManager.SetTextBox("Take this as a warning. If we meet again I will surely destroy you!");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            uiManager.UnSetTextBox();
            soundManager.PlayClick();
            StartCoroutine(dragonMovement.FlyUp());
            yield return new WaitForSeconds(3);
            // Player Resets
            foreach (var i in Enumerable.Range(0, 1000))
            {
                yield return null;
                playerMovement.SetCameraPosition(Vector3.MoveTowards(cam.transform.localPosition, new Vector3(0, 0.5f, 0), Time.deltaTime * 2));
            }
            fade.StartCoroutine("BlackIn");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("HomeFire");
        }
    
    }
}
