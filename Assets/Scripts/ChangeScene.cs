using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Fade fade;
    public string scene;
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        fade.StartCoroutine("FadeOut");
        yield return new WaitForSeconds(1);
        if (scene == "Village")
        {
            if (SceneManager.GetActiveScene().name == "HomeFire" || SceneManager.GetActiveScene().name == "Home")
            {
                PlayerData.VillageExit = 0;
            }
            else if (SceneManager.GetActiveScene().name == "Lair")
            {
                PlayerData.VillageExit = 1;
            }
        }
        SceneManager.LoadScene(scene);
    }
}