using UnityEngine;

public class skeletonEnemy : combatEntity
{
    public static GameObject skeletonPrefab;

    public skeletonEnemy(int hp, int def) : base(hp, def)
    {
        entity = GameObject.Instantiate(skeletonPrefab);
        entity.tag = "enemy";
    }
}
