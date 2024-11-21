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

    public GameObject mc;

    private void Start()
    {
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;
    }

    public override void interact()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];

        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world!", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "Hello, world! AGAIN", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "Hello, world! AGAIN", "TestSpeaker2", p2));
        msgs.Enqueue(new msg(testFace, () => "BBBBBBBBBOBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB", "TestSpeaker1", p1));
        msgs.Enqueue(new msg(testFace, () => "ASOUIDHOASIDOAHSDOAHSDOIAHSDOIHSADOIHADOIHSADOIHASOIDHAOISHDOISAHDOIAHSDOIHSADOISAOIDHASOIDHSAOIHDAOISHDOISAHDOIASHDOIHSADOIHSADOIHSAOIDHAS", "TestSpeaker2", p2));

        speakers[0].transform.position = mc.transform.position + new Vector3(6f, 0, 0);
        speakers[1].transform.position = mc.transform.position + new Vector3(-6f, 0, 0);
        msgController.createDialogue(speakers, msgs);
    }
}
   
