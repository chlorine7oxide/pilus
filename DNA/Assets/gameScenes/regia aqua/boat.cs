using System.Collections.Generic;
using UnityEngine;

public class boat : overworldInteractable
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
        if (!playerData.boatPrepared)
        {
            GameObject p1 = speakers[0];
            GameObject p2 = speakers[1];

            msgs.Enqueue(new msg(testFace, () => "I think I see something across the water.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "I really hope that's what we've been looking for.", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "We've been exploring this old BORING town for hours now.", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "This place is filled with history.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "I think it's quite facinating actually.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "Alright history man.", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "What do you think we're looking for here?", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "I think it could be some sort of historic artifact or relic.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "What if there isn't even anything to find?", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "I highly doubt that, we've been sent here since clearly we are capable people.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "There must be a purpose.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "Sometimes things just are how they are for no reason.", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "Either way, we should cross the water and see what that is.", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "I'll set this boat up for now, just come back to me when you're ready to leave.", "e", p2));

            playerData.boatPrepared = true;
            playerData.companion = false;



            speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
            speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
            msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
        }
        else
        {

            GameObject p1 = speakers[0];
            GameObject p2 = speakers[1];

            msgs.Enqueue(new msg(testFace, () => "Are you ready to leave?", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "decisionBoat", "e", p1));

            speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
            speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
            msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
        }
    }
}
