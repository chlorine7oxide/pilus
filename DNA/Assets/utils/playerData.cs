using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting.FullSerializer;

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
            switch (gene.name)
            {
                case "Arm":
                    cur.Add("Punch");
                    break;
                case "Courage":
                    cur.Add("Insult");
                    break;
                case "Mechanical Arm":
                    cur.Add("Slam");
                    break;
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
    public static bool armWarning = false;
    public static bool crowBeaten = false;
    public static bool ratBeaten = false;
    public static bool rodConvo = false;
}
