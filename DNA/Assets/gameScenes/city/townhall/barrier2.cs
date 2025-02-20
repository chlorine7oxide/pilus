using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier2 : MonoBehaviour
{
    //public static int guard2stage = 0; // 0 is waiting for start, 1 is talked to guard waiting for player to follow them, 2 is done and barrier destory

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    protected void Start()
    {
        if (playerData.guard2stage == 1)
        {
            Destroy(guard2);
            Destroy(barricade);
        }
        else
        {
            Destroy(guard2);
            Destroy(barricade);
            Destroy(b2);
            Destroy(b3);
        }

        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector;


    }

    public float speed;

    public Sprite[] up, down, side;
    public GameObject guard2, barricade, b2, b3;

    public IEnumerator dialogue()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];
        GameObject p3 = speakers[2];

        msgs.Enqueue(new msg(testFace, () => "You two look lost.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "What are you here for?", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "We are here to see the mayor.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "We have a few questions to ask.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Ahh, I see.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Do you have an appointment?", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "No, we don't.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Don't worry, that is alright, just come with me, I'll show you where to go.", "e", p3));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
        speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

        yield return new WaitUntil(() => !msgController.inText);

        playerData.guard2stage = 1;
        int animProgress = 0;
        guard2.GetComponent<SpriteRenderer>().sprite = down[animProgress];
        Destroy(barricade);

        for (int i = 0; i < (1 / speed); i++)
        {
            guard2.transform.Translate(0, -speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                guard2.GetComponent<SpriteRenderer>().sprite = down[animProgress];
            }
        }
        for (int i = 0; i < (2.5 / speed); i++)
        {
            guard2.transform.Translate(-speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard2.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }
        Destroy(guard2);
    }
}
