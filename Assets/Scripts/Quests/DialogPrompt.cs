using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;
using TMPro;

public class DialogPrompt : MonoBehaviour
{
    public QuestManager questManager;
    public TextMeshProUGUI promptText;
    public string questName;
    public Canvas canvas;

    public ParticleSystem ringPrefab;
    private ParticleSystem ring;

    private bool active = true;

    private void Start()
    {
        if (ringPrefab != null)
        {
            ring = Instantiate(ringPrefab, transform);
            //ring.transform.position = transform.position;
            //sparkle.Play();
        }
    }

    private void Update()
    {
        switch (questName)
        {
            case "Grandma's Stew":
                active = !PlayerData.GrandmasStewCompleted;
                break;
            case "Excalibwhere?":
                active = !PlayerData.ExcalibwhereCompleted;
                break;
            case "ALostSoul":
                active = !PlayerData.ALostSoulCompleted;
                break;
            default:
                break;
        }

        if (active && ringPrefab != null && !ring.isPlaying)
        {
            ring.Play();
        }
        else if (!active && ringPrefab != null)
        {
            ring.Stop();
        }
    }

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
        if (Input.GetKeyDown(KeyCode.X) && active)
        {
            active = false;
            canvas.GetComponent<Canvas>().enabled = false;
            //GetComponent<CapsuleCollider>().enabled = false;
            questManager.NewQuest(questName);
            promptText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active) promptText.gameObject.SetActive(false);
    }


}
