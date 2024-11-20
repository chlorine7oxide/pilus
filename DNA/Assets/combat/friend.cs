using UnityEngine;

public class friend : combatEntity
{

    public friend(int hp, int def) : base(hp, def)
    {
        entity = new GameObject("friend");
        entity.tag = "player";
    }

    public void attack(combatEntity target)
    {
        target.takeDamage(10);
    }

    public void swing(combatEntity target)
    {
        target.takeDamage(15);
    }

    public void meditate(combatEntity target)
    {
        target.def += 5;
    }

    public void heal(combatEntity target)
    {
        target.heal(10);
    }
}
