using UnityEngine;

public class testenemy : combatEntity
{
    public testenemy(int hp, int def) : base(hp, def)
    {
        entity = new GameObject("testEnemy");
        entity.tag = "enemy";
    }

    public void attack(combatEntity target)
    {
        target.takeDamage(10);
    }

    public void bite(combatEntity target)
    {
        target.takeDamage(25);
    }
}
