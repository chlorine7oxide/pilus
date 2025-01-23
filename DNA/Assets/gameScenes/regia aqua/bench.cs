using System.Collections.Generic;
using UnityEngine;

public class bench : overworldInteractable
{
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

    public override void interact()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];

        msgs.Enqueue(new msg(testFace, () => "We've been going for a while now.", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "Don't you think it's time for a break?", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "decisionSit", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(null, () => "I guess we do have to keep going then...", "TestSpeaker2", p2));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
    }
}
