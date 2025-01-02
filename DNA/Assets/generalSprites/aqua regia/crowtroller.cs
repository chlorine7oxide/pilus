using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowtroller : MonoBehaviour
{
    public GameObject crow;

    public Sprite portrait, face, portrait2, selector;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public GameObject crowPrefab;

    public Sprite rock;

    public int stage = 0;

    public GameObject alertPrefab;

    private void Start()
    {
        crowEnemy.crowPrefab = crowPrefab;
        print(playerData.crowBeaten);
        if (playerData.crowBeaten)
        {
            carryableItem.create(rock, "Giant Rock", new Vector3(-54, 42, 0), false, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (stage == 0)
            {
                Destroy(this.GetComponent<Rigidbody2D>());
                Animator anim = crow.GetComponent<Animator>();
                anim.SetInteger("state", 1);
                StartCoroutine(switchAnim());
            }
            else if (stage == 2)
            {
                Destroy(this.GetComponent<Rigidbody2D>());
                StartCoroutine(enterCombat());
            }
            
            
        }
    }
    
    public IEnumerator switchAnim()
    {

        if (stage == 1)
        {
            yield break;
        }
        else
        {
            stage = 1;
        }
        yield return new WaitForSeconds(1f);
        Animator anim = crow.GetComponent<Animator>();
        anim.SetInteger("state", 2);
        for (int i = 0;i< 120; i++)
        {
            crow.transform.Translate(new Vector3(-0.08f, 0.03f, 0));
            yield return new WaitForFixedUpdate();
        }

        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];

        

        msgs.Enqueue(new msg(face, () => "What was that?", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(face, () => "I think it's a crowlder.", "TestSpeaker1", p2));
        msgs.Enqueue(new msg(face, () => "I've never heard of that before.", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(face, () => "Me either, I just made it up.", "TestSpeaker1", p2));
        msgs.Enqueue(new msg(face, () => "I think we should track it down though, we could use that boulder to solve the puzzle.", "TestSpeaker1", p2));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController.createDialogue(speakers, msgs, faces);

        crow.transform.position = new Vector3(-54, 42, 0);
        this.gameObject.transform.position = new Vector3(-46, 42, 0);
        this.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        anim.SetInteger("state", 3);
        stage = 2;
    }

    public IEnumerator enterCombat()
    {
        if (stage == 3)
        {
            yield break;
        }
        else
        {
            stage = 3;
        }

        crow.GetComponent<SpriteRenderer>().flipX = true;
        crow.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Animator anim = crow.GetComponent<Animator>();

        Instantiate(alertPrefab, crow.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        anim.SetInteger("state", 1);
        yield return new WaitForSeconds(1f);

        anim.SetInteger("state", 2);

        for (int i = 0; i < 120; i++)
        {
            crow.transform.Translate(new Vector3(0.22f, 0, 0));
            yield return new WaitForFixedUpdate();
        }

        
    }
}
