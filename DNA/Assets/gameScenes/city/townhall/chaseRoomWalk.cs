using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseRoomWalk : MonoBehaviour
{
    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    void Start()
    {
        StartCoroutine(walk());
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector;
    }

    public GameObject guard;

    public Sprite[] up, down, side;

    public float speed;

    public Sprite guardmask;

    public bool ready;

    public IEnumerator walk()
    {
        int animProgress = 0;
        guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];

        for (int i = 0; i < (5 / speed); i++)
        {
            guard.transform.Translate(speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }
        guard.GetComponent<SpriteRenderer>().flipX = false;
        for (int i = 0; i < (5 / speed); i++)
        {
            guard.transform.Translate(0, -speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = down[animProgress];
            }
        }
        for (int i = 0; i < (10 / speed); i++)
        {
            guard.transform.Translate(-speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }
        for (int i = 0; i < (8 / speed); i++)
        {
            guard.transform.Translate(0, -speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = down[animProgress];
            }
        }

        guard.GetComponent<SpriteRenderer>().sprite = up[0];
        ready = true;
    }

    public IEnumerator startChase()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];
        GameObject p3 = speakers[2];

        msgs.Enqueue(new msg(testFace, () => "Wait right here for just a moment.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "I will be right back.", "e", p3));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
        speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

        playerData.guard2stage = 3;

        yield return new WaitUntil(() => !msgController.inText);

        int animProgress = 0;
        for (int i = 0; i < (8 / speed); i++)
        {
            guard.transform.Translate(0, speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = up[animProgress];
            }
        }
        guard.GetComponent<SpriteRenderer>().flipX = true;
        for (int i = 0; i < (10 / speed); i++)
        {
            guard.transform.Translate(speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }
        guard.GetComponent<SpriteRenderer>().flipX = false;
        for (int i = 0; i < (5 / speed); i++)
        {
            guard.transform.Translate(0, speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = up[animProgress];
            }
        }
        for (int i = 0; i < (5 / speed); i++)
        {
            guard.transform.Translate(-speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }
        for(int i = 0; i < (3 / speed); i++)
        {
            guard.transform.Translate(0, speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = up[animProgress];
            }
        }
        guard.GetComponent<SpriteRenderer>().enabled = false;

        yield break;
    }

    public IEnumerator returnHere()
    {
        guard.GetComponent<SpriteRenderer>().enabled = true;

        int animProgress = 0;
        for (int i = 0; i < (3 / speed); i++)
        {
            guard.transform.Translate(0, -speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = down[animProgress];
            }
        }
        guard.GetComponent<SpriteRenderer>().sprite = side[0];
        guard.GetComponent<SpriteRenderer>().flipX = true;

        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];
        GameObject p3 = speakers[2];

        msgs.Enqueue(new msg(testFace, () => "You two have been reported to the proper authorities.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Breaking into private property through a locked door is a serious crime.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "And in the town hall nonetheless.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "We didn't know!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "We were just trying to get back home!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "There are plenty of legal methods to get anywhere in the world, you have no excuse.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Now I just have to keep an eye on you until someone arrives to properly deal with you.", "e", p3));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
        speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

        Destroy(trigger2);
        playerData.guard2stage = -1;

        guard.GetComponent<SpriteRenderer>().flipX = false;

        for (int i = 0; i < (1 / speed); i++)
        {
            guard.transform.Translate(-speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                guard.GetComponent<SpriteRenderer>().sprite = down[animProgress];
            }
        }

        guard.GetComponent<SpriteRenderer>().flipX = true;
        guard.GetComponent<SpriteRenderer>().sprite = side[0];

        yield break;
    }


    public GameObject trigger2;
}
