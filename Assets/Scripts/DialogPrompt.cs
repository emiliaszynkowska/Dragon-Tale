using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogPrompt : MonoBehaviour
{
    public QuestManager questManager;
    public TextMeshProUGUI promptText;
    public int ID;

    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            promptText.text = "Press X to talk";
            promptText.gameObject.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.X) && active)
        {
            questManager.NewQuest(ID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active) promptText.gameObject.SetActive(false);
    }
}
