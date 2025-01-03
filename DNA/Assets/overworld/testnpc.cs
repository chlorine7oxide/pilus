using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;

public class testnpc : overworldInteractable
{
    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public Sprite mcsprite, lilbro, testFace, selector;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public GameObject mc;

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

        /*
        for (int i = 0; i < 100; i++)
        {
            msgs.Enqueue(new msg(testFace, () => i.ToString(), "TestSpeaker1", p1));
        }
        */
        /*
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker2", p2));
        */
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "decision1", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker1", p1));

        string s = "";
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("holdableObject"))
        {
            if (g.GetComponent<carryableItem>().isHeld)
            {
                s = g.name;
            }
        }

        msgs.Enqueue(new msg(testFace, () => "Woah there, thats a nice " + s + " youve got there", "TestSpeaker2", p2));

        speakers[0].transform.position = mc.transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = mc.transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
    }
}
   
