using UnityEngine;

public static class items
{
    public static void potion(combatEntity target)
    {
        target.hp += 10;
    }

    public static void bigPotion(combatEntity target)
    {
        target.hp += 25;
    }

    public static void molotov(combatEntity target)
    {
        target.takeDamage(30);
    }
}
