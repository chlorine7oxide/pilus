using UnityEngine;

public class boulderEnemy : combatEntity
{
    public boulderEnemy(int hp, int def) : base(hp, def)
    {
        entity = new GameObject();
        entity.tag = "enemy";
    }

    public void shield(combatEntity target)
    {
        target.def += 5;
    }

    public void nothing()
    {
        return;
    }
}
