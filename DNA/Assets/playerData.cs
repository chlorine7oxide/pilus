using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class playerData
{
    public static LinkedList<string> MCabilities = new();
    public static LinkedList<string> friendAbilities = new();
    public static LinkedList<string> items = new();
    public static LinkedList<Gene> genes = new();
    public static int hp;
    public static int def;
}
