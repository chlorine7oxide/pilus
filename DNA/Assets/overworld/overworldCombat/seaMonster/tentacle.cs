using System.Collections;
using UnityEngine;

public class tentacle : MonoBehaviour
{
    public static Sprite projectile;
    public static Sprite baseSprite;

    public bool alive = true;
    public bool active = false;
    public bool ready = true;

    public static float submergeTime = 0.5f;
    public static float emergeTime = 0.5f;
    public static float slashTime = 2;
    public static float splashTime = 3;
    public static float slamTime = 5;

    public static boss1Controller controller;

    public bool interactable = false;
    public bool combatable = false;
    public bool slamming = false;

    public static tentacle create()
    {
        GameObject g = new("tentacle");
        g.AddComponent<tentacle>();
        g.AddComponent<BoxCollider2D>();
        g.AddComponent<Rigidbody2D>().gravityScale = 0;
        g.GetComponent<BoxCollider2D>().isTrigger = true;
        DontDestroyOnLoad(g);

        return g.GetComponent<tentacle>();
    }
    public void activate(Vector3 pos)
    {
        this.transform.position = pos;
        this.gameObject.AddComponent<SpriteRenderer>().sprite = baseSprite;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        active = true;
        // animation placeholder
    }

    public void submerge()
    {
        // animation placeholder
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        StartCoroutine(submergeAnimation());
    }

    public IEnumerator submergeAnimation()
    {
        ready = false;
        yield return new WaitForSeconds(submergeTime);
        ready = true;
    }

    public void emerge(Vector3 pos)
    {
        slamming = false;
        this.transform.position = pos;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = baseSprite;
        StartCoroutine(emergeAnimation());
    }

    public IEnumerator emergeAnimation()
    {
        ready = false;
        yield return new WaitForSeconds(emergeTime);
        ready = true;
    }

    public void slash(Vector3 target)
    {
        StartCoroutine(slashAttack(target));
    }

    public IEnumerator slashAttack(Vector3 target)
    {
        ready = false;
        //indication placeholder
        yield return new WaitForSeconds(1);

        Vector3 og = this.gameObject.transform.position;
        this.transform.position = target;
        // animation placeholder
        yield return new WaitForSeconds(slashTime - 1);

        this.transform.position = og;
        ready = true;
    }

    public void splash(Vector3 target)
    {
        StartCoroutine(splashAttack(target));
    }

    public IEnumerator splashAttack(Vector3 target)
    {
        //indication placeholder
        ready = false;
        enemyUtils.projectile(25, target - this.gameObject.transform.position, projectile, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, -this.gameObject.transform.position + (target + Vector3.Cross(target - this.gameObject.transform.position, new Vector3(0, 0, 1).normalized)), projectile, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, -this.gameObject.transform.position + (target + Vector3.Cross(target - this.gameObject.transform.position, new Vector3(0, 0, -1).normalized)), projectile, 5, this.gameObject.transform.position);
        yield return new WaitForSeconds(1);
        enemyUtils.projectile(25, target - this.gameObject.transform.position, projectile, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, -this.gameObject.transform.position + (target + Vector3.Cross(target - this.gameObject.transform.position, new Vector3(0, 0, 1).normalized)), projectile, 5, this.gameObject.transform.position);
        enemyUtils.projectile(25, -this.gameObject.transform.position + (target + Vector3.Cross(target - this.gameObject.transform.position, new Vector3(0, 0, -1).normalized)), projectile, 5, this.gameObject.transform.position);
        yield return new WaitForSeconds(slashTime - 1);
        ready = true;

    }

    public void slam(Vector3 target)
    {
        StartCoroutine(slamAttack(target));
    }

    public IEnumerator slamAttack(Vector3 target)
    {
        
        ready = false;
        //indication placeholder
        yield return new WaitForSeconds(1);
        slamming = true;
        Vector3 og = this.gameObject.transform.position;
        this.transform.position = target;
        // animation placeholder
        combatable = true;
        yield return new WaitForSeconds(slamTime - 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && alive && active)
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            interactable = false;
        }
    }

    private void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.Z) && active && alive && slamming)
            {
                interactable = false;
                StartCoroutine(controller.enterCombat(this));
            }
        }
        if (controller.isCombat && this.gameObject.transform.position.x < 100)
        {
            this.transform.Translate(new Vector3(1000, 0, 0));
        }
    }
}
