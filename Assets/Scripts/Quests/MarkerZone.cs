using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkerZone : MonoBehaviour
{
    public QuestMarker marker;
    public TextMeshProUGUI txt;
    public string zone;
    //public Compass compass;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && marker.image != null)
        {
            txt.text = "Search Area: " + zone;
            txt.gameObject.SetActive(true);
            marker.image.color = new Color(0f, 0f, 0f, 0f);
        } else
        {
            txt.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && marker.image != null)
        {
            txt.gameObject.SetActive(false);
            marker.image.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
