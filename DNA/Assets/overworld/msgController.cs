using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.ComponentModel;
using Unity.VisualScripting;

public class msgController : MonoBehaviour
{
    public List<textBox> boxes = new();
    public Queue<msg> msgs = new Queue<msg>(); 
    public bool inText = false;
    public bool decision = false;
    public bool readyForbox = false;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();

    public Dictionary<textBox, Vector3> pos = new();

    public decision currentDecision;

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
            if (decision)
            {

            }
            else if (Input.GetKeyDown(KeyCode.Z) )
            {
                if (msgs.Count == 0)
                {
                    nextMsg();
                    return;
                }
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
            string t = m.text();

            if (t.Contains("decision"))
            {
                decision = true;
                if (t.Contains("1"))
                {
                    activateSpeaker(m.speaker);
                    setFace(m.speaker, m.face);
                    StartCoroutine(decision1Listener(createDecision(m.name, "choice 1", "choice 2", (m.speaker.transform.position.x > 0))));
                }
                else if (t.Contains("Sit"))
                {
                    activateSpeaker(m.speaker);
                    setFace(m.speaker, m.face);
                    StartCoroutine(decisionSitListener(createDecision(m.name, "Sit and rest.", "Continue on.", false)));
                }
            }
            else
            {
                activateSpeaker(m.speaker);
                setFace(m.speaker, m.face);
                createBox(m.name, m.text(), (m.speaker.transform.position.x > 0));
            }

            
        }
        else
        {
            inText = false;
            foreach (textBox b in boxes)
            {
                Destroy(b.box);
                Destroy(b.getText());
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

    public IEnumerator decision1Listener(decision d)
    {
        yield return new WaitUntil(() => d.complete);
        if (d.isUp)
        {
            msgs.Enqueue(new msg(null, () => "You chose 1", "decision1", speakers[0]));
        }
        else
        {
            msgs.Enqueue(new msg(null, () => "You chose 2", "decision2", speakers[0]));
        }
        decision = false;
        nextMsg();
    }

    public IEnumerator decisionSitListener(decision d)
    {
        yield return new WaitUntil(() => d.complete);
        if (d.isUp)
        {
            msgs.Dequeue();
            msgs.Enqueue(new msg(null, () => "All these puzzles are tiring aren't they", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "I find it a little strange that an abandoned place like this has them.", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "It does seem a little...", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "Insecure? Of a security system.", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "I'm not gonna argue though.", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "Makes it easier for us.", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "Mmm.", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "How much further do you think we have to go here?", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "How big is this place anyways?", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "I didn't know this place even existed before now - it WAS abandoned when I was here though.", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "And I was only a toddler, so even if I had been here I doubt I would've remembered.", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "I'm just wondering why it was abandoned.", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "It's making me a little anxious, what if there's something dangerous...", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "I doubt that, we haven't seen anything yet.", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "Still.", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "We should be careful.", "TestSpeaker2", speakers[0]));
            msgs.Enqueue(new msg(null, () => "I agree, let's keep going now.", "TestSpeaker2", speakers[1]));
            msgs.Enqueue(new msg(null, () => "I've had enough of sitting here.", "TestSpeaker2", speakers[1]));
            

        }
        decision = false;
        nextMsg();
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
            StartCoroutine(moveOverTime(box.getText(), pos[box] + box.textpos + new Vector3(0, b.getHeight(), 0), 0.2f, b, box.textpos));
            pos[box] = pos[box] + new Vector3(0, b.getHeight(), 0);
        }
        if (boxes.Count == 0)
        {
            readyForbox = true;
        }
        boxes.Add(b);
        StartCoroutine(goafter(b, mc));
    }

    public decision createDecision(string name, string text1, string text2, bool isRight)
    {
        GameObject mc = GameObject.FindGameObjectWithTag("player");
        decision b = new decision(name, text1, isRight, text2);
        pos.Add(b, mc.transform.position + new Vector3(0, -3.5f, 0));
        b.moveTo(new Vector3(10000, -5f, 0));
        readyForbox = false;
        foreach (textBox box in boxes)
        {
            StartCoroutine(moveOverTime(box.box, pos[box] + new Vector3(0, b.getHeight(), 0), 0.2f, b, Vector3.zero));
            StartCoroutine(moveOverTime(box.name, pos[box] + box.namePos + new Vector3(0, b.getHeight(), 0), 0.2f, b, box.namePos));
            StartCoroutine(moveOverTime(box.getText(), pos[box] + box.textpos + new Vector3(0, b.getHeight(), 0), 0.2f, b, box.textpos));
            pos[box] = pos[box] + new Vector3(0, b.getHeight(), 0);
        }
        if (boxes.Count == 0)
        {
            readyForbox = true;
        }
        boxes.Add(b);
        StartCoroutine(goafter(b, mc));
        return b;
    }

    public void activateSpeaker(GameObject speaker)
    {
        foreach(GameObject s in speakers)
        {
            s.transform.localScale = new Vector3(1, 1, 1);
        }
        speaker.transform.localScale = new Vector3(19.0f/16.0f, 19.0f/16.0f, 1);
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
