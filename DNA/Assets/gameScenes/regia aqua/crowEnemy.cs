using UnityEngine;

public class crowEnemy : combatEntity
{
    public static GameObject crowPrefab;

    public crowEnemy(int hp, int def) : base(hp, def)
    {
        entity = GameObject.Instantiate(crowPrefab);
        entity.tag = "enemy";
    }

    public void dive(combatEntity target)
    {
        target.takeDamage(35);
    }

    public void doubleDive(combatEntity target, combatEntity target2)
    {
        target.takeDamage(25);
        target2.takeDamage(25);
    }
}
