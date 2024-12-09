using UnityEngine;

public static class enemyUtils
{
    public static GameObject projectile(int damage, Vector3 dir, Sprite sprite, float lifespan, Vector3 origin)
    {
        GameObject g = new();
        g.layer = 8;
        g.transform.position = origin;
        g.AddComponent<SpriteRenderer>().sprite = sprite;
        g.GetComponent<SpriteRenderer>().sortingOrder = 10;
        g.AddComponent<Rigidbody2D>().linearVelocity = dir;
        g.AddComponent<BoxCollider2D>().isTrigger = true;
        g.AddComponent<projectile>().damage = damage;
        g.GetComponent<projectile>().dir = dir;
        g.GetComponent<projectile>().life = lifespan;
        g.GetComponent<Rigidbody2D>().excludeLayers = 1 << 8;
        
        return g;
    }
}
