using System;
using System.Collections;
using Home;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI Bar
    public GameObject uiBar;
    public GameObject level;
    public Image reputation;
    public GameObject playername;
    // Controls
    public GameObject controls;
    public GameObject talk;
    public GameObject quests;
    public GameObject attack;
    public GameObject menu;
    // Menu
    public GameObject menuPanel;
    public Slider volume;
    public Slider lookSensitivity;
    public Slider movementSpeed;
    private bool isMenu;
    private bool isMute;
    // Text Boxes
    public GameObject textBoxes;
    public GameObject textBoxBig;
    public GameObject textBox;
    public GameObject iconBox;
    // World
    public GameObject toVillage;
    public GameObject marker;
    // Screen
    public GameObject deathScreen;
    public Image endingScreen;
    public Sprite badEnding;
    public Sprite goodEnding;
    // Game
    public SoundManager soundManager;
    public PlayerMovement playerMovement;
    public PlayerRotation playerRotation;
    public Fade fade;

    void Start()
    {
        // Menu Settings
        if (PlayerData.Mute)
            Mute();
        if (PlayerData.Volume > 0)
            soundManager.audioSource.volume = PlayerData.Volume;
        if (PlayerData.LookSensitivity > 0)
            playerRotation.lookSensitivity = PlayerData.LookSensitivity;
        if (PlayerData.MovementSpeed > 0 && (SceneManager.GetActiveScene().name.Equals("Village") || SceneManager.GetActiveScene().name.Equals("Lair")))
            playerMovement.movementSpeed = PlayerData.MovementSpeed * 3;
        else 
        {
            if (PlayerData.MovementSpeed > 0)
                playerMovement.movementSpeed = PlayerData.MovementSpeed / 3;
        }
        UpdateReputation();
    }

    public void Pause()
    {
        soundManager.PlayClick();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        soundManager.PlayClick();
        Time.timeScale = 1;
    }

    public void UpdateReputation()
    {
        reputation.fillAmount = (PlayerData.Reputation + 1) / 2;
        if (reputation.fillAmount < 0.5)
        {
            reputation.color = new Color(0.8f, 0.2f, 0.2f);
        } else
        {
            reputation.color = new Color(0.2f, 0.8f, 0.2f);
        }
    }

    public void Quit()
    {
        soundManager.PlayClick();
        StartCoroutine(QuitGame());
    }
    public void SetTextBoxBig(string t)
    {
        textBoxBig.gameObject.SetActive(true);
        textBoxBig.GetComponentInChildren<TextMeshProUGUI>().text = t;
    }

    public void SetTextBox(string t)
    {
        textBox.gameObject.SetActive(true);
        iconBox.gameObject.SetActive(true);
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = t;
    }

    public void UnsetTextBoxBig()
    {
        textBoxBig.gameObject.SetActive(false);
    }

    public void UnSetTextBox()
    {
        textBox.gameObject.SetActive(false);
        iconBox.gameObject.SetActive(false);
    }

    public void Menu()
    {
        isMenu = !isMenu;
        if (isMenu)
        {
            Pause();
            menuPanel.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(false);
            Resume();
        }
    }

    public void Apply()
    {
        StartCoroutine(ApplyChanges());
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

    public void LookSensitivity()
    {
        playerRotation.lookSensitivity = lookSensitivity.value * 10;
    }

    public void MovementSpeed()
    {
        if (SceneManager.GetActiveScene().name.Equals("Village") || SceneManager.GetActiveScene().name.Equals("Lair"))
            playerMovement.movementSpeed = movementSpeed.value * 60;
        else
            playerMovement.movementSpeed = movementSpeed.value * 20;
    }
    
    public void EndingScreen(int screen)
    {
        endingScreen.gameObject.SetActive(true);
        if (screen == 0)
            endingScreen.sprite = goodEnding;
        else
            endingScreen.sprite = badEnding;
    }

    IEnumerator ApplyChanges()
    {
        soundManager.PlayClick();
        Resume();
        yield return new WaitForSeconds(0.05f);
        Pause();
    }

    IEnumerator QuitGame()
    {
        soundManager.PlayClick();
        fade.StartCoroutine("BlackIn");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }


    public IEnumerator DeathScreen()
    {
        soundManager.StopMusic();
        yield return fade.BlackIn();
        deathScreen.SetActive(true);
        Pause();
        yield return fade.BlackOut();
        //soundManager.PlayQuestStarted();
    }
}
