using UnityEngine;

public class ratTailEnemy : combatEntity
{
    public static GameObject ratTailPrefab;

    public ratTailEnemy(int hp, int def) : base(hp, def)
    {
        entity = GameObject.Instantiate(ratTailPrefab);
        entity.tag = "RatEnemy";
    }

    public void hit(combatEntity target, combatEntity target2)
    {
        target.takeDamage(25);
        target2.takeDamage(25);
    }

    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);
        if (hp <= 0)
        {
            entity.tag = "Untagged";
        }
    }
}
