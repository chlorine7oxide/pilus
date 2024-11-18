using UnityEngine;

public class testenemy : combatEntity
{
    public GameObject testEnemy;

    public testenemy(int hp, int def) : base(hp, def)
    {
        testEnemy = new GameObject("testEnemy");
        testEnemy.tag = "enemy";
    }

    public void attack(combatEntity target)
    {
        target.takeDamage(10);
    }

    public void bite(combatEntity target)
    {
        target.takeDamage(15);
    }
}
