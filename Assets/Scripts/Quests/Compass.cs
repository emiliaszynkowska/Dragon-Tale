using System.Collections;
using Quests;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject iconPrefab;
    List<QuestMarker> questMarkers = new List<QuestMarker>();

    public RawImage compassImage;
    public Transform player;

    public float maxDistance = 400f;

    float compassUnit;

    //Quests
    //public GrandmasStew grandmasStew;

    //Waypoints
    //public QuestMarker grandmasStewQuestMarker;
    //public QuestMarker grandmaMarker;

    //Icons
    //public Sprite questIcon;
    //public Sprite grandmaIcon;
    
    

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach(QuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dst = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marker.Position);
            float scale = 0f;

            if (dst < maxDistance) scale = 1f - (dst / maxDistance);
            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public IEnumerator AddQuestMarker (QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;
        marker.image.rectTransform.localScale = Vector3.zero;

        questMarkers.Add(marker);
        yield return null;
    }

    public IEnumerator RemoveQuestMarker (QuestMarker marker)
    {
        marker.image.color = new Color(0f, 0f, 0f, 0f);
        marker.image = null;
        questMarkers.Remove(marker);
        yield return null;
    }

    Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.Position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    } 
}
