using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting.FullSerializer;
using TMPro;
using UnityEditor.PackageManager;

public static class playerData
{
    public static List<string> MCabilities = new();

    public static void setAbilities()
    {
        List<string> cur = new()
        {
            "Check", "Focus"
        };
        foreach (Gene gene in equiped)
        {
            if (gene == null)
            {
                continue;
            }
            int armCount = 0, mechArmCount = 0;
            switch (gene.name)
            {
                case "Arm":
                    cur.Add("Punch");
                    armCount++;
                    break;
                case "Courage":
                    cur.Add("Insult");
                    break;
                case "Mechanical Arm":
                    cur.Add("Slam");
                    mechArmCount++;
                    break;
            }
            if (armCount == 2)
            {
                cur.Remove("Punch");
                cur.Remove("Punch");
                cur.Add("Double Punch");
            }
            if (mechArmCount == 2)
            {
                cur.Remove("Slam");
                cur.Remove("Slam");
                cur.Add("Slam+");
            }
        }
        MCabilities = cur;
    }

    public static List<string> friendAbilities = new();

    public static void setFriendAbilities()
    {
        List<string> cur = new();
        cur.Add("attack");
        cur.Add("swing");
        cur.Add("meditate");
        cur.Add("heal");
        friendAbilities = cur;
    }

    public static List<string> items = new();

    public static int hp;
    public static int def;

    public static void setStats()
    {
        int hp = 100;
        int def = 10;

        foreach (Gene gene in genes)
        {
            switch (gene.name)
            {
                case "Courage":
                    hp += 10;
                    break;
                case "Stronger bones":
                    def += 5;
                    break;
            }
        }

        playerData.hp = hp;
        playerData.def = def;
    }

    public static List<Gene> genes = new();
    public static List<Gene> eyes = new();
    public static List<Gene> arms = new();

    public static Gene[] equiped = new Gene[8]; // 0, 1 eye 2, 3 arm 4, 5, 6, 7 general

    public static int money = 0;

    ///////////////// flags
    public static bool companion = true;
    public static bool armWarning = false;
    public static bool crowBeaten = false;
    public static bool ratBeaten = false;
    public static bool rodConvo = false;
    public static int documentsRead = 0;
    public static bool libraryRegistryRead = false;
    public static bool visitedLibrary = false;
    public static bool strangeLibrary = false;
    public static bool skeletonFought = false;
    public static bool skeletonDialogue = false;
    public static bool antihistamineGene = false;
    public static bool[] lockFlags = { false, false };
    public static bool boatPrepared = false;
    public static string[] libraryBooks = { "", "", "", "", "" };
    public static bool garbageBook = false;
    public static bool gondolaChecked = false;
    public static bool librarianMet = false;
    public static bool inspectsConvo = false;
    public static bool secretLibraryUnlocked = false;
    public static bool secretLibraryKey = false;
    public static bool ticketForge = false;
    public static bool ticketBooth = false;
    public static bool visitedDowntown = false;
    public static int birthdayGuess = 0;
}
