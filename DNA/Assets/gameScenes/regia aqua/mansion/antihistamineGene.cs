using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antihistamineGene : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        if (!playerData.antihistamineGene)
        {
            StartCoroutine(text());
            playerData.antihistamineGene = true;
        }
        
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("Something is sticking out from under one of the boxes.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.destroy();
        speak();
    }

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public Sprite gen2, gen2t, gen2b, gen2j;

    private void Start()
    {
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector;
    }

    public void speak()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];

        msgs.Enqueue(new msg(testFace, () => "What's that?", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "I think it's a new gene.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "It's going to help with my sneezing in here!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "How convenient.", "e", p2));

        //playerData.genes.Add(new Gene()); // PLACEHOLDER FOR NOW until i have the art for it

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
    }
}
