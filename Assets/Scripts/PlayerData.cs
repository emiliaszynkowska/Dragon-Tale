using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    // Settings
    public static float Volume
    { get; set; }

    public static bool Mute
    { get; set; }

    public static float MovementSpeed
    { get; set; }

    public static float LookSensitivity
    { get; set; }

    // World
    public static int VillageExit //0 = from home, //1 = from lair
    { get; set; }

    public static string TalkingTo
    { get; set; }

    public static bool FreeCam
    { get; set; }

    // Quests
    public static int DragonsTalePart
    { get; set;}
    public static bool DragonsTaleStarted
    { get; set; }
    public static bool DragonsTaleCompleted
    { get; set; }

    public static bool AMayorsRequestCompleted
    { get; set; }
    
    public static bool AMayorsRequestStarted
    { get; set; }
    
    public static int AMayorsRequestPart
    { get; set; }
    
    public static int GrandmasStewPart
    { get; set; }

    public static bool GrandmasStewCompleted
    { get; set; }

    public static bool GrandmasStewStarted 
    { get; set; }

    public static int MushroomsCollected
    { get; set; }

    public static int CarrotsCollected
    { get; set; }

    public static int ApplesCollected
    { get; set; }

    public static int ExcalibwherePart
    { get; set; }

    public static bool ExcalibwhereCompleted
    { get; set; }

    public static bool ExcalibwhereStarted
    { get; set; }

    public static bool SwordCollected
    { get; set; }

    public static int ALostSoulPart
    { get; set; }

    public static bool ALostSoulCompleted
    { get; set; }

    public static bool ALostSoulStarted
    { get; set; }

    public static int SpidersKilled
    { get; set; }

    public static int BeetleJuicePart
    { get; set; }

    public static bool BeetleJuiceCompleted
    { get; set; }

    public static bool BeetleJuiceStarted
    { get; set; }

    public static int BeetlesKilled
    { get; set; }
    public static bool healthPotion25
    { get; set; }
    
    public static bool healthPotion50
    { get; set; }
    
    public static bool healthPotion75
    { get; set; }
    
    public static bool strengthPotion
    { get; set; }
    
    public static bool damagePotion
    { get; set; }
    
    public static bool speedPotion
    { get; set; }

    

}
