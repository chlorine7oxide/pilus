using Mono.Cecil;
using UnityEngine;

public class MC : combatEntity
{

    public MC(int hp, int def) : base(hp, def)
    {
        entity = new GameObject("mc");
        entity.tag = "player";
    }

    public void punch(combatEntity target)
    {
        target.takeDamage(10);
    }

    public void slam(combatEntity target)
    {
        target.takeDamage(15);
    }

    public void insult(combatEntity target)
    {
        target.def -= 5;
    }
    
    public void focus(combatEntity target)
    {
        target.heal(10);
    }

    public void check(combatEntity target)
    {
        Debug.Log("check placeholder");
    }

    public void punch2(combatEntity target)
    {
        target.takeDamage(15);
    }

    public void slam2(combatEntity target)
    {
        target.takeDamage(20);
    }
}
