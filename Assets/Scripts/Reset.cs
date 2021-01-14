using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    public Button btn;
    public UIManager uiManager;
    private void Start(){
        btn.onClick.AddListener(() => Menu());
    }

    private void Menu()
    {
        Debug.Log("Clicked");

        uiManager.Resume();
        //World
        PlayerData.VillageExit = 0;
        PlayerData.TalkingTo = "Yvryr";
        PlayerData.FreeCam = false;
        PlayerData.VillageComplete = false;
        //Stats
        PlayerData.Reputation = 0;
        PlayerData.Health = 0;
        PlayerData.Resistance = 0;
        PlayerData.Attack = 0;
        //Quests
        PlayerData.DragonsTalePart = 0;
        PlayerData.DragonsTaleStarted = false;
        PlayerData.DragonsTaleCompleted = false;
        PlayerData.GrandmasStewPart = 0;
        PlayerData.GrandmasStewCompleted = false;
        PlayerData.GrandmasStewStarted = false;
        PlayerData.MushroomsCollected = 0;
        PlayerData.CarrotsCollected = 0;
        PlayerData.ApplesCollected = 0;
        PlayerData.ExcalibwherePart = 0;
        PlayerData.ExcalibwhereCompleted = false;
        PlayerData.ExcalibwhereStarted = false;
        PlayerData.SwordCollected = false;
        PlayerData.ALostSoulPart = 0;
        PlayerData.ALostSoulCompleted = false;
        PlayerData.ALostSoulStarted = false;
        PlayerData.SpidersKilled = 0;
        PlayerData.SoulSaved = false;
        PlayerData.BeetleJuicePart = 0;
        PlayerData.BeetleJuiceStarted = false;
        PlayerData.BeetleJuiceCompleted = false;
        PlayerData.BeetlesKilled = 0;
        //Items
        PlayerData.HealthPotion25 = false;
        PlayerData.HealthPotion50 = false;
        PlayerData.HealthPotion75 = false;
        PlayerData.SprintPotion = false;
        SceneManager.LoadScene("Start");
    }
}
