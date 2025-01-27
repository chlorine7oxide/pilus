using UnityEngine;

public class tentacleEnemy : combatEntity
{
    public static GameObject tentaclePrefab;

    public tentacleEnemy(int hp, int def) : base(hp, def)
    {
        entity = GameObject.Instantiate(tentaclePrefab);
        entity.tag = "enemy";
    }

    public void slash(combatEntity[] targets)
    {
        foreach(combatEntity c in targets)
        {
            c.takeDamage(15);
        }
    }

    public void slam(combatEntity target)
    {
        target.takeDamage(25);
    }
}
