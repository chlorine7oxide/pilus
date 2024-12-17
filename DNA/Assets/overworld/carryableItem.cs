using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class carryableItem : MonoBehaviour
{
    public bool isHeld;
    public bool inventory;

    public bool interactable = false;

    public static void create(Sprite sprite, string item, Vector3 pos, bool isHeld, bool inventory)
    {
        GameObject g = new GameObject(item);
        g.AddComponent<carryableItem>();
        g.AddComponent<BoxCollider2D>();
        g.AddComponent<Rigidbody2D>().gravityScale = 0;
        g.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        g.GetComponent<BoxCollider2D>().isTrigger = true;
        g.AddComponent<SpriteRenderer>().sprite = sprite;
        g.GetComponent<SpriteRenderer>().sortingOrder = (isHeld) ? 8 : 6;
        g.transform.position = pos;
        g.GetComponent<carryableItem>().isHeld = isHeld;
        g.GetComponent<carryableItem>().inventory = inventory;
        if (isHeld)
        {
            walkingAnimation.heldItem = true;
        }
        g.tag = "holdableObject";
        if (item == "Giant Rock")
        {
            g.AddComponent<giantRock>();
            g.AddComponent<BoxCollider2D>().excludeLayers = 1 << 3;
        }

    }

    private void OnDestroy()
    {
        if (inventory)
        {
            playerData.items.Add(this.gameObject.name);
        }
    }

    private void Update()
    {
        if (isHeld)
        {
            this.transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(0, 1, 0);
            this.gameObject.GetComponent<Rigidbody2D>().excludeLayers |= 1 << 7;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().excludeLayers &= ~(1 << 7);

        }
        if ((Input.GetKeyDown(KeyCode.Z) && interactable && !overworldInteractable.talkable))
        {
            StartCoroutine(pickUp());
        }
        if (isHeld && Input.GetKeyDown(KeyCode.Z) && !overworldInteractable.talkable)
        {
            StartCoroutine(drop());
        }
    }

    public IEnumerator pickUp()
    {
        yield return new WaitForSeconds(0.05f);
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 8;
        walkingAnimation.heldItem = true;
        isHeld = true;
    }

    public IEnumerator drop()
    {
        yield return new WaitForSeconds(0.05f);
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
        this.gameObject.transform.Translate(new Vector3(0, -1.5f, 0));
        walkingAnimation.heldItem = false;
        isHeld = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            interactable = false;
        }
    }
}
