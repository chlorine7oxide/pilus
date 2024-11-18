using Mono.Cecil;
using UnityEngine;

public class MC : combatEntity
{
    public GameObject mc;

    public MC(int hp, int def) : base(hp, def)
    {
        mc = new GameObject("mc");
        mc.tag = "player";
    }

    void attack(combatEntity target)
    {
        target.takeDamage(10);
    }

    void kick(combatEntity target)
    {
        target.takeDamage(15);
    }

    void insult(combatEntity target)
    {
        target.def -= 5;
    }
    
    void heal(combatEntity target)
    {
        target.hp += 10;
        if (target.hp > target.maxhp)
        {
            target.hp = target.maxhp;
        }
    }
}
