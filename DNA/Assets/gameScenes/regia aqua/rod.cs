using System.Collections.Generic;
using UnityEngine;

public class rod : overworldInteractable
{
    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public Sprite rodKeyLess;
    public static Sprite rod2;
    public static GameObject thisRod;

    private void Start()
    {
        rod2 = rodKeyLess;
        thisRod = this.gameObject;

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

        if (!playerData.rodConvo)
        {
            msgs.Enqueue(new msg(testFace, () => "This reminds me of when I was a kid.", "TestSpeaker2", p2));
            msgs.Enqueue(new msg(testFace, () => "My father used to take me out on his boat fishing.", "TestSpeaker2", p2));
            msgs.Enqueue(new msg(testFace, () => "I actually always used to hate it, we never caught anything either.", "TestSpeaker2", p2));
            msgs.Enqueue(new msg(testFace, () => "But now I miss it, those were simpler times.", "TestSpeaker2", p2));
            msgs.Enqueue(new msg(testFace, () => "What's stopping you from fishing now?", "TestSpeaker2", p1));
            msgs.Enqueue(new msg(testFace, () => "Nothing, really.", "TestSpeaker2", p2));
            msgs.Enqueue(new msg(testFace, () => "I guess I just don't live near the water.", "TestSpeaker2", p2));
            playerData.rodConvo = true;
        }
        msgs.Enqueue(new msg(testFace, () => "decisionFishing", "TestSpeaker2", p1));



        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
    }
}
