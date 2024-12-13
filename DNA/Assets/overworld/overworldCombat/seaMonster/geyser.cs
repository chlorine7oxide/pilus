using System.Collections;
using UnityEngine;

public class geyser : MonoBehaviour
{
    public static Sprite proj;

    public static void create(Vector3 position)
    {
        GameObject geyser = new GameObject();
        geyser.transform.position = position;
        geyser.AddComponent<SpriteRenderer>().sprite = tentacle.baseSprite;
        geyser.GetComponent<SpriteRenderer>().sortingOrder = 3;
        geyser.AddComponent<BoxCollider2D>();
        geyser.GetComponent<BoxCollider2D>().isTrigger = true;
        geyser.AddComponent<Rigidbody2D>().gravityScale = 0;
        geyser.AddComponent<geyser>();
    }

    private void Start()
    {
        StartCoroutine(geyserAttack());
    }

    public IEnumerator geyserAttack()
    {
        //animation placeholder
        yield return new WaitForSeconds(1);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 0) * 3, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 45) * 3, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 90) * 3, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 135) * 3, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 180) * 3, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 225) * 3, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 270) * 3, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(10, boss1Controller.fromPolar(1, 315) * 3, proj, 5, this.gameObject.transform.position);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
