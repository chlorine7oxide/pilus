using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class businessShelf : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A shelf full of various filing boxes and folders.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("Hidden behind a box is an expensive looking fountain pen.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
        if (!playerData.items.Contains("Fountain pen"))
        {
            pen();
        }
    }

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    private void Start()
    {
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector;
    }

    public void pen()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];

        msgs.Enqueue(new msg(testFace, () => "I'll take that if you don't mind.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "You can't just take that!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "That belongs to someone you know.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Haven't you been taking everything that isn't nailed down?", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "I've left a few things...", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "That's besides the point!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "I'll report you for looting.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Fine. I'll leave it.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Hee hee, I'll be taking that.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Seriously...", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Too easy.", "e", p1));

        playerData.items.Add("Fountain pen");

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
    }
}
