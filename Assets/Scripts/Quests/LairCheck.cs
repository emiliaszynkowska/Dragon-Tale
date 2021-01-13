using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LairCheck : MonoBehaviour
{
    public QuestManager questManager;
    public ChangeScene changeScene;
    public GameObject textBox;
    public TextMeshProUGUI text;
    public GameObject player;
    public Fade fade;

    private void OnTriggerEnter(Collider other)
    {
        questManager.FreezePlayer(true);
        StartCoroutine(Check());

    }

    IEnumerator Check()
    {
        text.text = "You are about to enter Yvryr'r Lair.\nOnce you enter there is no turning back.\nAre you ready?";
        textBox.gameObject.SetActive(true);
        questManager.ShowRadial("Yes", "No");
        while (!questManager.Answered)
        {
            yield return null;
        }
        questManager.HideRadial();
        if (questManager.LastSelection == 0)
        {
            StartCoroutine(changeScene.Switch());
        }
        else
        {
            yield return fade.BlackIn();
            player.transform.position = new Vector3(-32.8f, 1.9f, 839.4f);
            player.transform.rotation = Quaternion.Euler(0, 150, 0);
            textBox.gameObject.SetActive(false);
            yield return fade.BlackOut();
            questManager.FreezePlayer(false);
        }
    }
}
