using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class fish : MonoBehaviour
{
    public static Sprite box;

    public static fish create(Sprite fishSprite, int type)
    {
        GameObject fish = new();
        fish.AddComponent<SpriteRenderer>().sprite = fishSprite;
        fish.AddComponent<fish>().type = type;
        switch (type) {
            case 0:
                fish.GetComponent<fish>().hp = 75;
                fish.GetComponent<fish>().maxHp = 100;
                fish.GetComponent<fish>().speed = 2;
                break;
            case 1:
                fish.GetComponent<fish>().hp = 125;
                fish.GetComponent<fish>().maxHp = 150;
                fish.GetComponent<fish>().speed = 4;
                break;
            case 2:
                fish.GetComponent<fish>().hp = 175; 
                fish.GetComponent<fish>().maxHp = 250;
                fish.GetComponent<fish>().speed = 6;
                break;
        }
        fish.AddComponent<BoxCollider2D>().isTrigger = true;
        fish.AddComponent<Rigidbody2D>().gravityScale = 0;
        fish.AddComponent<BoxCollider2D>().excludeLayers |= 1 << 8;
        fish.transform.position = Vector3.zero;

        GameObject hpbar = new(), hp = new();
        hpbar.transform.parent = fish.transform;
        hpbar.AddComponent<SpriteRenderer>().color = Color.red;
        hpbar.transform.position = new Vector3(0, 0.5f, 0);
        hpbar.transform.localScale = new Vector3(3, 0.3f, 1);
        hp.transform.parent = fish.transform;
        hp.AddComponent<SpriteRenderer>().color = Color.green;
        hp.transform.localScale = new Vector3(3, 0.3f, 1);
        hp.transform.Translate(0, 0.5f, 0);
        hp.GetComponent<SpriteRenderer>().sortingOrder = 1;
        hpbar.GetComponent<SpriteRenderer>().sprite = box;
        hp.GetComponent<SpriteRenderer>().sprite = box;

        return fish.GetComponent<fish>();

    }

    public float hp = 1000;
    public float maxHp = 1000;
    public int type;
    public bool reeling = false;
    public float speed;
    public float target = -1000, start = 0;

    private void FixedUpdate()
    {
        if (target == -1000)
        {
            start = this.gameObject.transform.position.y;
            target = Random.Range(-2.9f, 2.9f);
            StartCoroutine(speedINC());
        }
        else if (start > target)
        {
            if (this.gameObject.transform.position.y <= target)
            {
                target = -1000;
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -speed));
            }
        }
        else if (start <= target)
        {
            if (this.gameObject.transform.position.y >= target)
            {
                target = -1000;
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
            }
        }

        if (reeling)
        {
            hp -= 0.3f;
        }
        else{
            hp = Mathf.Min(maxHp, hp + 0.15f);
        }
        Debug.Log(hp);

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.transform.GetChild(1).transform.localScale = new Vector3(3 * hp / maxHp, 0.3f, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bar"))
        {
            reeling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bar"))
        {
            reeling = false;
        }
    }

    public IEnumerator speedINC()
    {
        speed = speed * 4;
        yield return new WaitUntil(() => !((this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY > 0) ^ (target - start > 0)));
        speed = speed / 4;
    }
}
