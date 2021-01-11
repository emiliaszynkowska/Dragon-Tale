using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public Transform player;
    public string quest;
    public int part;

    public ParticleSystem sparklePrefab;
    private ParticleSystem sparkle;



    private bool active = false;

    private void Start()
    {
        if (sparklePrefab != null)
        {
            sparkle = Instantiate(sparklePrefab, transform);
            sparkle.transform.position = transform.position;
            //sparkle.Play();
        }
    }

    private void Update()
    {
        active = (quest == "Grandma's Stew" && part == 1 && PlayerData.GrandmasStewPart == 1 && PlayerData.MushroomsCollected < 15)
              || (quest == "Grandma's Stew" && part == 2 && PlayerData.GrandmasStewPart == 2 && PlayerData.CarrotsCollected < 5)
              || (quest == "Grandma's Stew" && part == 3 && PlayerData.GrandmasStewPart == 3 && PlayerData.ApplesCollected < 3)
              || (quest == "Excalibwhere?" && part == 1 && PlayerData.ExcalibwherePart == 1 && PlayerData.SwordCollected == false);

        if (active && sparklePrefab != null && !sparkle.isPlaying)
        {
            sparkle.Play();
        } else if (!active && sparklePrefab != null)
        {
            sparkle.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (active)
        {
            pickupText.text = "Press X to collect";
            pickupText.gameObject.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.X) && active)
        {
            active = false;
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
            case "Carrot": PlayerData.CarrotsCollected += 1;
                break;
            case "Apple": PlayerData.ApplesCollected += 1;
                break;
            case "Sword": PlayerData.SwordCollected = true;
                break;
        }
    }
}
