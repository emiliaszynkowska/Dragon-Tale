using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int VillageExit //0 = from home, //1 = from lair
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
}
