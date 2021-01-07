using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public Transform player;
    public string quest;

    private bool active = false;
   
    private void OnTriggerEnter(Collider other)
    {
        if (quest == "Grandma's Stew" && PlayerData.GrandmasStewPart > 0 && PlayerData.MushroomsCollected < 15)
        {
            active = true;
        } else
        {
            active = false;
        }

        if (active)
        {
            pickupText.text = "Press Q to collect";
            pickupText.gameObject.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.Q) && active)
        {
            pickupText.gameObject.SetActive(false);
            StartCoroutine(MoveToPlayer(0.2f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active) pickupText.gameObject.SetActive(false);
    }

    IEnumerator MoveToPlayer(float timeToMove)
    {
        Vector3 startPosition = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(startPosition, player.position, t);
            yield return null;
        }
        gameObject.SetActive(false);
        IncrementData();
    }

    void IncrementData()
    {
        switch (tag)
        {
            case "Mushroom": PlayerData.MushroomsCollected += 1;
                break;
        }
    }
}
