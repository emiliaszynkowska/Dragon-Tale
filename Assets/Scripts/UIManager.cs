using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // UI Bar
    public GameObject uiBar;
    public GameObject level;
    public GameObject reputation;
    public GameObject playername;
    // Controls
    public GameObject controls;
    public GameObject talk;
    public GameObject quests;
    public GameObject attack;
    public GameObject menu;
    // Text Boxes
    public GameObject textBoxes;
    public GameObject textBoxBig;
    public GameObject textBox;
    public GameObject iconBox;
    // World
    public GameObject toVillage;
    public GameObject marker;
    // Other
    public SoundManager soundManager;

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

}
