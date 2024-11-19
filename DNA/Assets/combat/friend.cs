using UnityEngine;

public class friend : combatEntity
{
    public GameObject friendObj;

    public friend(int hp, int def) : base(hp, def)
    {
        friendObj = new GameObject("friend");
        friendObj.tag = "player";
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
        target.hp += 10;
        if (target.hp > target.maxhp)
        {
            target.hp = target.maxhp;
        }
    }
}
