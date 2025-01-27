using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class geyser : MonoBehaviour
{
    public static Sprite proj;
    public static Sprite[] geyserAnim;

    public static void create(Vector3 position)
    {
        GameObject geyser = new GameObject();
        geyser.transform.position = position;
        geyser.AddComponent<SpriteRenderer>().sprite = tentacle.baseSprite;
        geyser.GetComponent<SpriteRenderer>().sortingOrder = 19;
        geyser.GetComponent<SpriteRenderer>().sprite = null;
        geyser.AddComponent<BoxCollider2D>();
        geyser.GetComponent<BoxCollider2D>().isTrigger = true;
        geyser.AddComponent<Rigidbody2D>().gravityScale = 0;
        geyser.AddComponent<geyser>();
        geyser.transform.Translate(new Vector3(0, -1.3f, 0));
    }

    private void Start()
    {
        StartCoroutine(geyserAttack());
    }

    public IEnumerator geyserAttack()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = geyserAnim[0];

        for (int i = 1; i < 5; i++)
        {
            yield return new WaitForSeconds(0.125f);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = geyserAnim[i];
        }

        
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 0) * 8, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 45) * 8, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 90) * 8, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 135) * 8, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 180) * 8, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 225) * 8, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 270) * 8, proj, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, boss1Controller.fromPolar(1, 315) * 8, proj, 5, this.gameObject.transform.position);

        yield return new WaitForSeconds(0.125f);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = geyserAnim[5];
        yield return new WaitForSeconds(0.125f);

        Destroy(this.gameObject);
    }
}
