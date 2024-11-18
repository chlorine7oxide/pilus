using UnityEngine;

public abstract class combatEntity
{
    public int hp, maxhp, def;
    public bool active = true;

    public combatEntity(int hp, int def)
    {
        this.hp = hp;
        this.maxhp = hp;
        this.def = def;
    }

    public void takeDamage(int damage)
    {
        hp -= Mathf.Max((damage - def), 0);
        if (hp <= 0)
        {
            hp = 0;
            active = false;
        }
    }
}
