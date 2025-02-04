using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class gondolaConvo : overworldInteractable
{
    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

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
        if (!playerData.gondolaChecked)
        {
            GameObject p1 = speakers[0];
            GameObject p2 = speakers[1];

            msgs.Enqueue(new msg(testFace, () => "How do you work this thing?", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "Usually you would just insert your ticket.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "But it seems we don't have any.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "Well if you didn't notice, the ticket store is closed.", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "So that means we're stuck here.", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "We aren't stuck, we just need to find another gondola station.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "Luckily, earlier I saw a sign for one pointing towards a road to the north of here.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "Well, either way, it's almost like they don't even want our money!", "e", p1));

            speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
            speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
            msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

            playerData.gondolaChecked = true;
        }
        else
        {
            GameObject p1 = speakers[0];
            GameObject p2 = speakers[1];

            msgs.Enqueue(new msg(testFace, () => "I think we should head north to find the other gondola station.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "I agree, let's go.", "e", p1));

            speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
            speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
            msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
        }
    }
}
