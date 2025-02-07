using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class frontDesk : overworldInteractable
{
    public GameObject bookUI, cameraObj;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public Sprite portrait, libPortrait;

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
        if (playerData.librarianMet)
        {
            if (!bookUI.GetComponent<bookSelector>().active && playerData.libraryBooks.Contains(""))
            {
                bookUI.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);
                StartCoroutine(bookUI.GetComponent<bookSelector>().select());
            }
            else if (!playerData.libraryBooks.Contains("") && playerData.secretLibraryKey)
            {
                StartCoroutine(libTalk());
            }
        }
        else
        {
            GameObject p1 = speakers[0];
            GameObject p2 = speakers[1];
            GameObject p3 = speakers[2];

            msgs.Enqueue(new msg(testFace, () => "Hello, and welcome to my humble little library.", "e", p3));
            msgs.Enqueue(new msg(testFace, () => "Let me know if there's anything you would like.", "e", p3));
            msgs.Enqueue(new msg(testFace, () => "I am a little thirsty right now?", "e", p1));
            msgs.Enqueue(new msg(testFace, () => "Don't ask for that, this is a library not a cafe!", "e", p2)); // elbows MC
            msgs.Enqueue(new msg(testFace, () => "(I would be happy to get you some water.)", "e", p3));
            msgs.Enqueue(new msg(testFace, () => "We are quite alright.", "e", p2));
            msgs.Enqueue(new msg(testFace, () => "Well...", "e", p3));
            msgs.Enqueue(new msg(testFace, () => "Either way, feel free to take a look around.", "e", p3));
            msgs.Enqueue(new msg(testFace, () => "...", "e", p1)); // looks around
            msgs.Enqueue(new msg(testFace, () => "There seem to be quite a few books checked out at the moment.", "e", p1)); 
            msgs.Enqueue(new msg(testFace, () => "I'm afraid not.", "e", p3)); 
            msgs.Enqueue(new msg(testFace, () => "This place hasn't seen much kindness in recent times.", "e", p3)); 
            msgs.Enqueue(new msg(testFace, () => "Many books have gone missing.", "e", p3)); 
            msgs.Enqueue(new msg(testFace, () => "Is there anything we can do to help?", "e", p2)); 
            msgs.Enqueue(new msg(testFace, () => "Not really, just keep an eye out for anything marked library property.", "e", p3)); 
            msgs.Enqueue(new msg(testFace, () => "Definetely!", "e", p1)); 
            msgs.Enqueue(new msg(testFace, () => "We will find all your missing books for you!", "e", p1)); 
            msgs.Enqueue(new msg(testFace, () => "Oh, you don't have to, don't feel forced to help me.", "e", p3)); 
            msgs.Enqueue(new msg(testFace, () => "It's no trouble at all.", "e", p2)); 
            msgs.Enqueue(new msg(testFace, () => "Thank you very much then! Let me know if you find anything.", "e", p3)); 


            speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
            speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
            speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);

            msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

            playerData.librarianMet = true;
        }
    }

    public IEnumerator libTalk()
    {
        generalText t = generalText.create("Any luck in finding the secret library?", libPortrait, null);

        if (t is null)
        {
            yield break;
        }

        yield return new WaitUntil(() => t.done);

        t.destroy();

        if (playerData.secretLibraryUnlocked)
        {
            // TODO once secret library is added
        }
        else
        {
            t = generalText.create("Not yet.", portrait, null);

            if (t is null)
            {
                yield break;
            }

            yield return new WaitUntil(() => t.done);

            t.destroy();
        }
    }
}
