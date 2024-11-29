using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class playerData
{
    public static List<string> MCabilities = new();
    public static List<string> friendAbilities = new();
    public static List<string> items = new();
    public static int hp;
    public static int def;

    public static List<Gene> genes = new();
    public static List<Gene> eyes = new();
    public static List<Gene> arms = new();

    public static Gene[] equiped = new Gene[8]; // 0, 1 eye 2, 3 arm 4, 5, 6, 7 general

    ///////////////// flags
    public static bool armWarning = false;
}
