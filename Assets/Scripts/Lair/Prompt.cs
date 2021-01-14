using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Home;

public class Prompt : MonoBehaviour
{
    public PlayerMovement mv;
    public PlayerRotation rt;
    public GameObject textBox;
    public Image icon;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI messageBox;

    public Sprite thisSprite;
    public string speaker;
    public string message;
    public Sprite dragonSprite;

    public GameObject soul;

    private void OnTriggerEnter(Collider other)
    {
        if ((speaker == "Mayor" && PlayerData.Reputation <= 0) || (speaker == "Soul" && !PlayerData.SoulSaved))
        {
            Destroy(gameObject);
        }


        if (other.CompareTag("Player"))
        {
            StartCoroutine(Talk());
        }
    }

    IEnumerator Talk()
    {
        mv.canMove = false;
        rt.enabled = false;
        icon.sprite = thisSprite;
        speakerName.text = speaker;
        messageBox.text = message;
        textBox.SetActive(true);
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.V));
        textBox.SetActive(false);
        icon.sprite = dragonSprite;
        speakerName.text = "Yvryr";
        mv.canMove = true;
        rt.enabled = true;
        Destroy(gameObject);
    }
}
