using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class msgController : MonoBehaviour
{
    public List<textBox> boxes = new();
    public Queue<msg> msgs = new Queue<msg>(); 
    public bool inText = false;
    public bool waitingForDecision = false;

    public List<GameObject> speakers = new();

    public static GameObject createDialogue(List<GameObject> speakers, Queue<msg> msgs)
    {
        GameObject g = new();
        g.AddComponent<msgController>();
        g.GetComponent<msgController>().speakers = speakers;
        g.GetComponent<msgController>().msgs = msgs;
        g.tag = "dialogue";

        return g;
    }
    
    void Start()
    {
        nextMsg();
    }

    void Update()
    {
        if (inText)
        {
            if (!waitingForDecision && Input.GetKeyDown(KeyCode.Z))
            {
                nextMsg();
            }
        }
        if (msgs.Count > 0)
        {
            inText = true;
        }
    }

    public void nextMsg()
    {
        if (msgs.Count > 0)
        {
            inText = true;
            msg m = msgs.Dequeue();
            activateSpeaker(m.speaker);
            setFace(m.speaker, m.face);
            createBox(m.name, m.text(), (m.speaker.transform.position.x > 0));
        }
        else
        {
            inText = false;
            foreach (textBox b in boxes)
            {
                Destroy(b.box);
                Destroy(b.text);
                Destroy(b.name);
            }
            boxes = null;
            Destroy(this.gameObject);
        }
    }

    public void createBox(string name, string text, bool isRight)
    {
        textBox b = new textBox(name, text, isRight);
        foreach (textBox box in boxes) {
            box.move(new Vector3(0, b.getHeight(), 0));
        }
        boxes.Add(b);
    }

    public void activateSpeaker(GameObject speaker)
    {

    }

    public void setFace(GameObject speaker, Sprite face)
    {

    }
}
