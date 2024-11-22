using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class msgController : MonoBehaviour
{
    public List<textBox> boxes = new();
    public Queue<msg> msgs = new Queue<msg>(); 
    public bool inText = false;
    public bool decision = true;
    public bool readyForbox = false;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();

    public Dictionary<textBox, Vector3> pos = new();

    public static GameObject createDialogue(List<GameObject> speakers, Queue<msg> msgs, List<GameObject> faces)
    {
        GameObject g = new();
        g.AddComponent<msgController>();
        g.GetComponent<msgController>().speakers = speakers;
        g.GetComponent<msgController>().msgs = msgs;
        g.GetComponent<msgController>().faces = faces;
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                foreach (textBox t in boxes)
                {
                    t.moveTo(pos[t]);
                }
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
            foreach(GameObject s in speakers)
            {
                s.transform.position = new Vector3(0, 100, 0);
            }
            boxes = null;
            Destroy(this.gameObject);
        }
    }

    public void createBox(string name, string text, bool isRight)
    {
        GameObject mc = GameObject.FindGameObjectWithTag("player");
        textBox b = new textBox(name, text, isRight);
        pos.Add(b, mc.transform.position + new Vector3(0, -3.5f, 0));
        b.moveTo(new Vector3(10000, -5f, 0));
        readyForbox = false;
        foreach (textBox box in boxes) {
            StartCoroutine(moveOverTime(box.box, pos[box] + new Vector3(0, b.getHeight(), 0), 0.2f, b, Vector3.zero));
            StartCoroutine(moveOverTime(box.name, pos[box] + box.namePos + new Vector3(0, b.getHeight(), 0), 0.2f, b, box.namePos));
            StartCoroutine(moveOverTime(box.text, pos[box] + box.textpos + new Vector3(0, b.getHeight(), 0), 0.2f, b, box.textpos));
            pos[box] = pos[box] + new Vector3(0, b.getHeight(), 0);
        }
        if (boxes.Count == 0)
        {
            readyForbox = true;
        }
        boxes.Add(b);
        StartCoroutine(goafter(b, mc));
        
        
    }

    public void activateSpeaker(GameObject speaker)
    {
        foreach(GameObject s in speakers)
        {
            s.transform.localScale = new Vector3(1, 1, 1);
        }
        speaker.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void setFace(GameObject speaker, Sprite face)
    {
        for (int i = 0; i < speakers.Count; i++)
        {
            if (speaker == speakers[i])
            {
                faces[i].GetComponent<SpriteRenderer>().sprite = face;
            }
        }
    }

    public IEnumerator goafter(textBox b, GameObject mc)
    {
        yield return new WaitUntil(() => (readyForbox || Input.GetKeyDown(KeyCode.Z)));
        b.moveTo(mc.transform.position + new Vector3(0, -3.5f, 0));
    }

    public IEnumerator moveOverTime(GameObject obj, Vector3 finalPos, float time, textBox box, Vector3 offset)
    {
        yield return new WaitForSeconds(0.025f);
        for (float t = 0; t < time; t += 0.01f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, finalPos, t);
            yield return new WaitForSeconds(0.01f);
        }
        obj.transform.position = finalPos;
        readyForbox = true;
    }
}
