using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Home;

public class HomeQuest : MonoBehaviour
{
    public UIManager uIManager;
    public SoundManager soundManager;
    public RawImage questStartedImg;
    public Texture questStartedTxr;
    public TextMeshProUGUI questName;
    public Fade fade;

    public Compass compass;
    public GameObject panel;
    public GameObject rep;

    public GameObject questDetails;
    public TextMeshProUGUI title;
    public TextMeshProUGUI progress;
    public QuestMarker questMarker;
    public PlayerMovement playerMovement;
    public PlayerRotation playerRotation;

    private void Start()
    {
        PlayerData.Reputation = 0.5f;
        uIManager.UpdateReputation();
        if (PlayerData.DragonsTalePart == 0)
        {
            StartCoroutine(Wait());
        } else
        {
            ShowUI();
            uIManager.UnSetTextBox();
        }
    }

    IEnumerator Wait()
    {
        while(PlayerData.DragonsTalePart == 0)
        {
            yield return null;
        }

        StartCoroutine(Started());
    }
    public IEnumerator Started()
    {
        playerMovement.canMove = true;
        playerRotation.enabled = true;
        questStartedImg.texture = questStartedTxr;
        questName.text = "Dragon Tale";
        questName.alignment = TextAlignmentOptions.Center;
        questName.rectTransform.anchoredPosition = new Vector3(-200, 50, 0);
        StartCoroutine(fade.FadeInAndOut(questStartedImg.gameObject, 1));
        StartCoroutine(fade.FadeInAndOut(questName.gameObject, 1));
        soundManager.PlayQuestStarted();
        yield return new WaitForSeconds(3f);
        PlayerData.DragonsTalePart = 1;
        PlayerData.DragonsTaleStarted = true;
        ShowUI();
    }

    void ShowUI()
    {
        StartCoroutine(compass.AddQuestMarker(questMarker));
        compass.gameObject.SetActive(true);
        panel.SetActive(true);
        rep.SetActive(true);
        ShowProgress();
    }

    void ShowProgress()
    {
        title.text = "Dragon Tale";
        switch (PlayerData.DragonsTalePart)
        {
            case 1: 
                progress.text = "Go to the Village";
                break;
        }
        questDetails.SetActive(true);
    }
}
