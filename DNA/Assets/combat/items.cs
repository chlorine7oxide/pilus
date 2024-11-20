using UnityEngine;

public static class items
{
    public static void potion(combatEntity target)
    {
        target.heal(25);
    }

    public static void bigPotion(combatEntity target)
    {
        Debug.Log(target.hp);
        target.heal(25);
        Debug.Log(target.hp);
    }

    public static void molotov(combatEntity target)
    {
        target.takeDamage(30);
    }
}
