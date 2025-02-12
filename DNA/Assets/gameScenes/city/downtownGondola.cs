using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downtownGondola : overworldInteractable
{
    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public Sprite portrait;

    protected void Start()
    {
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector;
    }

    public override void interact()
    {
        if (playerData.items.Contains("Gondola Ticket"))
        {
            //// fast travel placeholder
        }
        else
        {
            if (!playerData.ticketForge)
            {
                GameObject p1 = speakers[0];
                GameObject p2 = speakers[1];

                msgs.Enqueue(new msg(testFace, () => "Still no ticket.", "e", p1));
                msgs.Enqueue(new msg(testFace, () => "One would think it isn't this difficult to obtain.", "e", p2));
                msgs.Enqueue(new msg(testFace, () => "Let's forge them!", "e", p1));
                msgs.Enqueue(new msg(testFace, () => "It can't that hard.", "e", p1));
                msgs.Enqueue(new msg(testFace, () => "Unfortunately we couldn't even if we wanted to.", "e", p2));
                msgs.Enqueue(new msg(testFace, () => "They're more secure than just a piece of paper.", "e", p2));
                msgs.Enqueue(new msg(testFace, () => "Alright fine.", "e", p1));
                msgs.Enqueue(new msg(testFace, () => "Let's just get them normally then.", "e", p1));

                speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
                speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
                msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
                playerData.ticketForge = true;
            }
            else
            {
                StartCoroutine(text());
            }
        }
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("Still no ticket...", portrait, null);

        if (t is null)
        {
            yield break;
        }

        yield return new WaitUntil(() => t.done);

        t.destroy();
    }
}
