using System.Collections;
using UnityEngine;

public class tentacle : MonoBehaviour
{
    public static Sprite projectile;
    public static Sprite baseSprite;

    public static GameObject slamPrefab;

    public bool alive = true;
    public bool active = false;
    public bool ready = true;

    public static float submergeTime = 1.5f;
    public static float emergeTime = 0.875f;
    public static float slashTime = 2;
    public static float splashTime = 2;
    public static float slamTime = 6;

    public static int numTent = 0;
    public int tentNum;

    public static boss1Controller controller;

    public bool interactable = false;
    public bool combatable = false;
    public bool slamming = false;

    public static Sprite[] emergeAnim;
    public static Sprite[] submergeAnim;
    public static Sprite[] slamAnim;
    public static Sprite[] splashAnim;

    public static tentacle create()
    {
        GameObject g = new("tentacle");
        g.tag = "tantacle";
        g.AddComponent<tentacle>();
        g.AddComponent<BoxCollider2D>();
        g.AddComponent<Rigidbody2D>().gravityScale = 0;
        g.GetComponent<BoxCollider2D>().isTrigger = true;
        DontDestroyOnLoad(g);
        g.GetComponent<tentacle>().tentNum = numTent;
        numTent++;

        return g.GetComponent<tentacle>();
    }
    public void activate(Vector3 pos)
    {
        this.transform.position = pos;
        this.gameObject.AddComponent<SpriteRenderer>().sprite = baseSprite;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tentNum;
        this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "enemy";
        active = true;
        // animation placeholder
    }

    public void submerge()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        StartCoroutine(submergeAnimation());
    }

    public IEnumerator submergeAnimation()
    {
        ready = false;
        for (int i = 0; i < 12; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = submergeAnim[i];
            yield return new WaitForSeconds(submergeTime / 12);
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        ready = true;
    }

    public void emerge(Vector3 pos)
    {
        slamming = false;
        this.transform.position = pos;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = baseSprite;
        StartCoroutine(emergeAnimation());
        if (GameObject.FindGameObjectWithTag("player").transform.position.x > this.gameObject.transform.position.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public IEnumerator emergeAnimation()
    {
        ready = false;
        for (int i = 0; i < 7; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = emergeAnim[i];
            yield return new WaitForSeconds(emergeTime / 7);
        }
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
        for (int i = 0; i < 4;i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = splashAnim[i];
            yield return new WaitForSeconds(0.125f);
        }
        geyser.create(this.gameObject.transform.position + new Vector3(-2, 0, 0));
        for (int i = 4; i < splashAnim.Length; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = splashAnim[i];
            yield return new WaitForSeconds(0.125f);
        }
        yield return new WaitForSeconds(splashTime - 0.125f * splashAnim.Length);
        ready = true;

    }

    public void slam(Vector3 target)
    {
        StartCoroutine(slamAttack(target));
    }

    public IEnumerator slamAttack(Vector3 target)
    {
        Vector3 offset = new Vector3(-4, 0, 0);


        if (this.gameObject.transform.position.x < GameObject.FindGameObjectWithTag("player").transform.position.x)
        {
            offset = new Vector3(4, 0, 0);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        ready = false;

        GameObject hitbox = Instantiate(slamPrefab, this.gameObject.transform.position - new Vector3(0, 1, 0), Quaternion.identity, this.gameObject.transform);
        hitbox.transform.localScale = new Vector3((this.gameObject.GetComponent<SpriteRenderer>().flipX) ? -10 : 10, 1, 1);
        hitbox.GetComponent<PolygonCollider2D>().enabled = false;
        hitbox.transform.Translate(offset);
        hitbox.GetComponent<hitboxSlam>().t = this;

        //indication placeholder
        yield return new WaitForSeconds(1);
        slamming = true;
        //Vector3 og = this.gameObject.transform.position;
        //this.transform.position = target;
        // animation placeholder

        hitbox.GetComponent<SpriteRenderer>().enabled = false;

        this.gameObject.transform.Translate(offset);

        for (int i = 0; i < 5; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = slamAnim[i];
            yield return new WaitForSeconds(0.125f);
        }

        hitbox.GetComponent<PolygonCollider2D>().enabled = true;
        hitbox.transform.Translate(-offset);

        combatable = true;
        /*
        yield return new WaitForSeconds(slamTime - 3);
        combatable = false;

        for (int i = 5; i < 16; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = slamAnim[i];
            yield return new WaitForSeconds(0.125f);
        }

        ready = true;
        slamming = false;
        */
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
                //interactable = false;
                //StartCoroutine(controller.enterCombat(this));
            }
        }
        if (controller.isCombat && this.gameObject.transform.position.x < 100)
        {
            this.transform.Translate(new Vector3(1000, 0, 0));
        }
    }
}
