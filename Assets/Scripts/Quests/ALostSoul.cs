using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALostSoul : MonoBehaviour
{
    public QuestManager questManager;
    public GameObject soul;
    public Transform soulsHead;
    public Transform player;
    public Transform cam;
    public Fade fade;

    public List<Creature> spiders;
    public float distance = 10f;
    public float buffer = 5f;
    public float speed = 0.01f;

    public QuestMarker questMarker;
    public QuestMarker sophieMarker;
    public QuestMarker soulMarker;

    private Animator anim;
    private bool watchingSoul;
    private bool following;
    private bool walking;
    
    private void Start()
    {
        //StartCoroutine(questManager.AddQuestMarker(questMarker));
        foreach (Creature spider in spiders)
        {
            spider.gameObject.SetActive(false);
        }
        soul.transform.position = new Vector3(129.37f, 0.4f, 837.33f);
        soul.transform.rotation = Quaternion.Euler(90f, -90f, -90f);
        anim = soul.GetComponent<Animator>();
        anim.enabled = false;
        PlayerData.ALostSoulCompleted = false;
    }

    
    public void Play()
    {
        PlayerData.ALostSoulStarted = true;
        if (!PlayerData.ALostSoulCompleted)
        {
            if (PlayerData.ALostSoulPart > 0) questManager.CurrentQuest = "A Lost Soul";
            switch (PlayerData.ALostSoulPart)
            {
                case 0: StartCoroutine(Part0()); break;
                case 1: StartCoroutine(Part1()); break;
                //case 2: StartCoroutine(Part2()); break;
                //case 3: StartCoroutine(Part3()); break;
                case 4: StartCoroutine(Part4()); break;
                case 5: StartCoroutine(Part5()); break;
            }
        }
    }

    public string GetProgress()
    {
        switch (PlayerData.ALostSoulPart)
        {
            case 1: return "Look for Soul";
            case 3 when PlayerData.SpidersKilled < 6: return "Kill Spiders: " + PlayerData.SpidersKilled + "/6";
            case 4: return "Return to Soul";
            case 5: return "Reunite Soul & Sophie";
            default: return "";
        }
    }

    IEnumerator Completed()
    {
        Debug.Log("Completed");
        PlayerData.ALostSoulCompleted = true;
        switch (PlayerData.ALostSoulPart)
        {
            case 0:
                //yield return questManager.RemoveQuestMarker(questMarker);
                yield return questManager.RemoveQuestMarker(questMarker);
                yield return questManager.Refused("A Lost Soul");
                Debug.Log("Failed");
                break;
            case 2:
                //yield return questManager.RemoveQuestMarker(questMarker);
                yield return questManager.RemoveQuestMarker(soulMarker);
                yield return questManager.Completed(null, "Soul Died. You might be next.");
                break;
            case 5:
                anim.Play("Male Idle", 0);
                yield return questManager.RemoveQuestMarker(sophieMarker);
                yield return questManager.Completed(null, "You saved Soul. Maybe he'll help defeat Yvryr.");
                break;
            default: Debug.Log("Completion Part not matched: " + PlayerData.ALostSoulPart);
                break;
        }
        yield return null;

    }

    IEnumerator Part0()
    {
        yield return questManager.Speak("Sophie", "Help! My friend Soul was supposed to visit days ago but hasn't arrived yet.");
        yield return questManager.Speak("Sophie", "I'm really scared something might have happened to him. Please can you try and find him?");
        questManager.ShowRadial("He's more then likely dead", "I'll do my best, stay strong.");
        while (!questManager.Answered)
        {
            yield return null;
        }
        questManager.HideRadial();
        if (questManager.LastSelection == 0)
        {
            yield return questManager.Speak("Sophie", "How could you say that!");
            StartCoroutine(Completed());
        }
        else
        {
            questManager.CurrentQuest = "A Lost Soul";
            StartCoroutine(questManager.Started());
            soul.transform.position = new Vector3(132.98f, 0f, 833.68f);
            soul.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            anim.enabled = true;
            yield return questManager.UpdateQuestMarker(soulMarker, questMarker);
            Debug.Log("Removed Marker?");
            PlayerData.ALostSoulPart = 1;
        }
    }

    IEnumerator Part1()
    {
        if (PlayerData.TalkingTo == "Sophie")
        {
            yield return questManager.Speak("Sophie", "Please hurry. He could be in danger.");
        } else if (PlayerData.TalkingTo == "Soul") {
            yield return questManager.Speak("Soul", "Hey you! Please help me!");
            yield return questManager.Speak("Soul", "I'm on my way to the Village but I feel I've been circling this forest for days.");
            yield return questManager.Speak("Soul", "I've been hearing strange noises the last few days, do you know the way?");
            questManager.ShowRadial("It's literally just over there...", "Sophie sent me. Follow me, I'll take you there.");
            while (!questManager.Answered)
            {
                yield return null;
            }
            questManager.HideRadial();
            if (questManager.LastSelection == 0)
            {
                yield return questManager.Speak("Soul", "Oh really? I hope it's safe...");
                PlayerData.ALostSoulPart = 2;
                yield return Part2();
                
            }
            else
            {
                yield return questManager.Speak("Soul", "Really? Thank you so much! Lead the way!");
                foreach (Creature spider in spiders)
                {
                    spider.gameObject.SetActive(true);
                }
                cam.position = new Vector3(157.3f, 5f, 801.5f);
                cam.rotation = Quaternion.Euler(0, 212, 0);
                PlayerData.FreeCam = true;
                yield return questManager.Speak("Soul", "Wait, what are those! Protect me!");
                PlayerData.FreeCam = false;
                PlayerData.ALostSoulPart = 3;
                yield return Part3();
            }
        }
    }

    IEnumerator LookAtSoul()
    {
        questManager.FreezePlayer(true);
        watchingSoul = true;
        while (watchingSoul)
        {
            cam.LookAt(soulsHead);
            yield return null;
        }
    }

    IEnumerator Part2()
    {
        StartCoroutine(LookAtSoul());
        anim.Play("Male_Walk", 0);
        yield return new WaitForSeconds(5);
        foreach (Creature spider in spiders)
        {
            spider.gameObject.SetActive(true);
        }
        anim.Play("Male Idle", 0);
        yield return questManager.Speak("Soul", "IT WAS NOT SAFE!");
        questManager.FreezePlayer(true);
        foreach (Creature spider in spiders)
        {
            spider.Hunt(soul.gameObject);
        }
        yield return new WaitForSeconds(7);
        anim.Play("Male Die", 0);
        yield return new WaitForSeconds(3);
        foreach (Creature spider in spiders)
        {
            spider.StopHunting();
        }
        StartCoroutine(Completed());
        watchingSoul = false;
        questManager.FreezePlayer(false);
        yield return new WaitForSeconds(5);
        foreach (Creature spider in spiders)
        {
            spider.Hunt(player.gameObject);
        }
    }

    IEnumerator Part3()
    {
        yield return questManager.RemoveQuestMarker(soulMarker);
        cam.localPosition = new Vector3(0, 1.5f, 0);
        cam.localRotation = Quaternion.Euler(0, 0, 0);
        foreach (Creature spider in spiders)
        {
            spider.Hunt(player.gameObject);
        }
        while (PlayerData.SpidersKilled < 6)
        {
            yield return null;
        }
        yield return questManager.AddQuestMarker(soulMarker);
        PlayerData.ALostSoulPart = 4;
    }

    IEnumerator Part4()
    {
        if (PlayerData.TalkingTo == "Sophie")
        {
            yield return questManager.Speak("Sophie", "Please hurry. He could be in danger.");
        }
        else if (PlayerData.TalkingTo == "Soul")
        {
            yield return questManager.Speak("Soul", "Wow, that was amazing. Let's go to the village.");
            StartCoroutine(FollowPlayer(soul));
            PlayerData.ALostSoulPart = 5;
            yield return questManager.UpdateQuestMarker(sophieMarker, soulMarker);
        }
    }

    IEnumerator Part5()
    {
        if (PlayerData.TalkingTo == "Sophie")
        {
            yield return fade.BlackIn();
            player.transform.position = new Vector3(107.8f, 1.9f, 373.36f);
            player.transform.rotation = Quaternion.Euler(0, 234.93f, 0);
            soul.transform.position = new Vector3(103.08f, 0, 363.98f);
            soul.transform.rotation = Quaternion.Euler(0, 31.434f, 0);
            yield return fade.BlackOut();
            yield return questManager.Speak("Sophie", "Soul! It's so good to see you safe. I was very worried!.");
            yield return questManager.Speak("Soul", "I never would have made it without your help.");
            yield return questManager.Speak("Soul", "Thank you, traveller. Call on me if you ever find yourself in trouble");
            following = false;
            StartCoroutine(Completed());
        }
    }

    IEnumerator FollowPlayer(GameObject soul)
    {
        following = true;
        walking = false;
        bool idle = true;
        float actualDistance;
        while (following)
        {
            actualDistance = Vector3.Distance(soul.transform.position, player.transform.position);
            if ((!idle &&  actualDistance > distance) || (idle  && actualDistance > distance + buffer))
            {
                //scale = 0f;
                //if (actualDistance < 15) scale = 1f - (actualDistance / 15);
                anim.Play("Male_Walk", 0);
                walking = true;
                idle = false;
                soul.transform.LookAt(player);
                soul.transform.position = Vector3.MoveTowards(soul.transform.position, player.transform.position, speed);
                soul.transform.position = new Vector3(soul.transform.position.x, 0f, soul.transform.position.z);
            } else
            {
                walking = false;
                idle = true;
                anim.Play("Male Idle", 0);
            }
            yield return null;
        }
    }
}
