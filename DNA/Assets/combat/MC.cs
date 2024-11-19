using Mono.Cecil;
using UnityEngine;

public class MC : combatEntity
{

    public MC(int hp, int def) : base(hp, def)
    {
        entity = new GameObject("mc");
        entity.tag = "player";
    }

    public void attack(combatEntity target)
    {
        target.takeDamage(10);
    }

    public void kick(combatEntity target)
    {
        target.takeDamage(15);
    }

    public void insult(combatEntity target)
    {
        target.def -= 5;
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
