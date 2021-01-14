using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogPrompt : MonoBehaviour
{
    public QuestManager questManager;
    public TextMeshProUGUI promptText;
    public string questName;
    public string person;
    public Canvas canvas;

    public Image dialogBox;
    public GameObject left;

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
        if (!(dialogBox.gameObject.activeInHierarchy || left.activeInHierarchy)){
            switch (questName)
            {
                case "Dragon Tale" when person == "Mayor":
                    active = !PlayerData.DragonsTaleCompleted;
                    break;
                case "Grandma's Stew":
                    active = (PlayerData.GrandmasStewPart >= 0 && !PlayerData.GrandmasStewCompleted) 
                          || (PlayerData.GrandmasStewCompleted && PlayerData.GrandmasStewPart > 0 && !PlayerData.HealthPotion25 && !PlayerData.HealthPotion50 && !PlayerData.HealthPotion75);
                    break;
                case "Excalibwhere?":
                    active = PlayerData.ExcalibwherePart >= 0 && !PlayerData.ExcalibwhereCompleted;
                    break;
                case "A Lost Soul" when person == "Sophie":
                    active = (PlayerData.ALostSoulPart == 0 || PlayerData.ALostSoulPart == 1 || PlayerData.ALostSoulPart == 4 || PlayerData.ALostSoulPart == 5) && !PlayerData.ALostSoulCompleted;
                    break;
                case "A Lost Soul" when person == "Soul":
                    active = (PlayerData.ALostSoulPart == 1 || PlayerData.ALostSoulPart == 4) && !PlayerData.ALostSoulCompleted;
                    break;
                case "Beetle Juice" when person == "Luna":
                    active = (PlayerData.BeetleJuicePart == 0) && !PlayerData.BeetleJuiceCompleted ;
                    break;
                case "Beetle Juice" when person == "Jesse":
                    active = (PlayerData.BeetleJuicePart == 1) && !PlayerData.BeetleJuiceCompleted;
                    break;
                default:
                    break;
            }
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
            promptText.text = "Talk to " + person + "\nPress X";
            promptText.gameObject.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.X) && active)
        {
            PlayerData.TalkingTo = person;
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
