using System.Collections.Generic;
using UnityEngine;

public class testObj : overworldInteractable
{
    public Sprite testface;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public GameObject mc;

    public override void interact()
    {
        msgs.Enqueue(new msg(testface, () => "interactable object", "", speakers[0]));

        speakers[0].transform.position = mc.transform.position + new Vector3(-6f, 0, 0);
        msgController.createDialogue(speakers, msgs, faces);
    }
}
