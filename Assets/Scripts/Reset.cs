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
        SceneManager.LoadScene("Start");
    }
}
