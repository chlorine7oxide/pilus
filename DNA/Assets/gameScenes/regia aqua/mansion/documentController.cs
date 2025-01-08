using System.Collections.Generic;
using UnityEngine;

public class documentController : MonoBehaviour
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

    public void interact()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];

        msgs.Enqueue(new msg(testFace, () => "I had heard about this place when I was a child.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "\"The ghost town on the lake\" or the \"Haunted village\"", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "There was all sorts of stories being told.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "What actually happened then?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Nobody really knows, not many people actually managed to evacuate.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "And even then, those people tend to stick to themselves.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "What do you think happened then?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "You seem to know more then me about this.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "A natural disaster probably?", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Honestly, I'm not too sure.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Where did you hear about this place from?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "I had never heard of it?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "I grew up in the city we woke up in.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Oh?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Is that why you knew your way around?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "My family left when I was still very young though.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "I think you just have a bad sence of direction.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "I can get around perfectly fine, thank you.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Sure.", "e", p2));



        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
    }

    void Update()
    {
        if (playerData.documentsRead == 3)
        {
            playerData.documentsRead++;
            interact();
        }
    }
}
