using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ticketBooth : overworldInteractable
{

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public GameObject itemMenu, cameraObj;
    public GameObject[] itemButtons;

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public Sprite portrait, sel, assistant;

    public bool active = false;

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
        if (!active)
        {
            if (playerData.ticketBooth)
            {
                StartCoroutine(selectID());
            }
            else
            {
                GameObject p1 = speakers[0];
                GameObject p2 = speakers[1];
                GameObject p3 = speakers[2];

                msgs.Enqueue(new msg(testFace, () => "Two tickets to cross the gorge please.", "e", p2));
                msgs.Enqueue(new msg(testFace, () => "You have IDs?", "e", p3));
                msgs.Enqueue(new msg(testFace, () => "Well you see...", "e", p1));
                msgs.Enqueue(new msg(testFace, () => "I don't care what you have to say, give me your IDs.", "e", p3));

                speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
                speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
                speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -3.1f, 0);
                msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

                StartCoroutine(selectID());
            }
        }

        
    }

    public IEnumerator selectID()
    {
        active = true;

        yield return new WaitUntil(() => !msgController.inText);

        itemMenu.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);

        string[] items = playerData.items.ToArray();
        Vector3[] pos = new Vector3[5];
        for (int i = 0; i < 5; i++)
        {
            pos[i] = itemButtons[i].transform.position + new Vector3(1.5f, -0.2f, 0);
        }

        if (items.Length == 0)
        {
            items = new string[] { "No items" };
        }

        dynamicSelectorText d = dynamicSelectorText.create(pos, items, sel, new Vector3(-4, 0.15f, 0));

        yield return new WaitUntil(() => d.done || Input.GetKeyDown(KeyCode.X));

        if (!d.done)
        {
            d.destroy();
            itemMenu.transform.position = new Vector3(-100, -100, 0);
            yield break;
        }

        itemMenu.transform.position = new Vector3(-100, -100, 0);
        string item = items[d.result];
        d.destroy();

        if (item.ToLower().Contains("card"))
        {
            // library card placeholder
        }
        else
        {
            generalText t = generalText.create("I'm afraid I cannot sell you tickets without valid identification.", assistant, null);

            if (t is null)
            {
                yield break;
            }

            yield return new WaitUntil(() => t.done);

            t.changeText("Come back when you have one.");

            yield return new WaitUntil(() => t.done);

            t.destroy();
        }

        active = false;
    }
}
