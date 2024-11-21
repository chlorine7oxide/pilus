using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;

public class testnpc : overworldInteractable
{
    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public Sprite testSpeaker, testFace;

    public List<GameObject> speakers = new();
    public Queue<msg> msgs = new();

    public bool ready = false;

    private void Start()
    {
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        StartCoroutine(populate());
    }

    public override void interact()
    {
        Debug.Log("connected");
        StartCoroutine(go());
    }

    public IEnumerator populate()
    {
        GameObject p1 = new();
        GameObject p2 = new();

        p1.transform.position = new Vector3(8f, 100f, 0);
        p2.transform.position = new Vector3(-8f, 100f, 0);
        p1.AddComponent<SpriteRenderer>();
        p2.AddComponent<SpriteRenderer>();
        p1.GetComponent<SpriteRenderer>().sprite = testSpeaker;
        p2.GetComponent<SpriteRenderer>().sprite = testSpeaker;
        speakers.Add(p1);
        speakers.Add(p2);

        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "Hello, world! AGAIN", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world! AGAIN", "TestSpeaker2", p2));

        ready = true;

        yield break;
    }

    public IEnumerator go()
    {
        yield return new WaitUntil(() => ready);
        speakers[0].transform.position = new Vector3(8f, 0, 0);
        speakers[1].transform.position = new Vector3(-8f, 0, 0);
        msgController.createDialogue(speakers, msgs);
    }
}
   
