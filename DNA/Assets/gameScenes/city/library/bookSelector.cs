using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class bookSelector : MonoBehaviour
{
    public GameObject[] books;
    public Sprite selector, sel;
    public GameObject itemMenu, cameraObj;
    public bool active = false;
    public GameObject[] itemButtons;
    public GameObject[] bookObjs, indicatorObjs;

    public GameObject librarian;

    public Sprite grey, greyB, blue, blueB;

    public float speed;

    public GameObject bookUI;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector2;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public List<Sprite> up, side, back;

    public GameObject barricade;


    protected void Start()
    {
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector2;
    }

    public IEnumerator select()
    {
        yield return new WaitForEndOfFrame();

        List<GameObject> l = new();
        for (int i = 0;i < 5; i++)
        {
            if (playerData.libraryBooks[i].Equals(""))
            {
                l.Add(books[i]);
                bookObjs[i].SetActive(false);
                indicatorObjs[i].SetActive(true);
            }
            else
            {
                bookObjs[i].SetActive(true);
                indicatorObjs[i].SetActive(false);
                if (i == 0 || i == 1 || i == 3)
                {
                    switch (playerData.libraryBooks[i])
                    {
                        case "Drilling Book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = grey;
                            break;
                        case "Marine life book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = blue;
                            break;
                    }
                }
                else
                {
                    switch (playerData.libraryBooks[i])
                    {
                        case "Drilling Book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = greyB;
                            break;
                        case "Marine life book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = blueB;
                            break;

                    }

                }
            }
        }

        if (l.Count == 0)
        {
            yield break;
        }

        active = true;
        staticSelector s = staticSelector.create(l.ToArray(), l.Count, selector);
        yield return new WaitUntil(() => s.done || Input.GetKeyDown(KeyCode.X));

        if (!s.done)
        {
            active = false;
            s.destroy();
            this.gameObject.transform.position = new Vector3(-100, -100, 0);
            yield break;
        }

        int sRes = s.result;

        s.destroy();

        this.gameObject.transform.position = new Vector3(-100, -100, 0);
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
            active = false;
            itemMenu.transform.position = new Vector3(-100, -100, 0);
            StartCoroutine(select());
            this.gameObject.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);
            yield break;
        }

        if (items[d.result].ToLower().Contains("book"))
        {
            playerData.items.Remove(items[d.result]);
            for (int i = 0; i < 5; i++)
            {
                if (books[i].Equals(l[sRes]))
                {
                    playerData.libraryBooks[i] = items[d.result];
                }
            }

            GameObject p1 = speakers[0];
            GameObject p2 = speakers[1];
            GameObject p3 = speakers[2];

            msgs.Enqueue(new msg(testFace, () => "Ahh, I see you've found one of my books!", "e", p3));

            switch (items[d.result]) {
                case "Drilling Book":
                    {
                        msgs.Enqueue(new msg(testFace, () => "I've never actually this one - too much technical jargon.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "But if you're into technical documentation it's a good read.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "Regia Aqua was a facinating place, though.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "A tragedy, really.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "What actually happened there?", "e", p1));
                        msgs.Enqueue(new msg(testFace, () => "Ahh, well nobody knows exactly.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "But it's likely that the predators in the area tore the place apart after the locals caught too many of the smaller fish.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "I hear people are trying to restablish the area, I hope they don't make the same mistake again.", "e", p3));

                        break;
                    }
                case "Marine life book":
                    {
                        msgs.Enqueue(new msg(testFace, () => "An old marine encyclopedia, people used to use these all the time.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "Now our infrastructure is less reliant on the ocean these are made less.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "This is definetely a rare find.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "I'd like to check this one out if that's alright.", "e", p2));
                        msgs.Enqueue(new msg(testFace, () => "Of course, just remember to return by the end of the month.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "...", "e", p1));
                        msgs.Enqueue(new msg(testFace, () => "What's that for?", "e", p1));
                        msgs.Enqueue(new msg(testFace, () => "My father is a fisherman.", "e", p2));
                        msgs.Enqueue(new msg(testFace, () => "I'd like to show it to him when we return.", "e", p2));
                        msgs.Enqueue(new msg(testFace, () => "If he's a fisherman wouldn't have one if he needed it then?", "e", p1));
                        msgs.Enqueue(new msg(testFace, () => "Yes, but it's always interesting to look at older books in your field.", "e", p2));
                        msgs.Enqueue(new msg(testFace, () => "I see.", "e", p1));

                        break;
                    }
                case "Friendship Book":
                    {
                        msgs.Enqueue(new msg(testFace, () => "What a strange book to see once again.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "Nobody ever checked it out - so I was quite suprised to see it missing.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "Nonetheless it returns, in case someone ever wanted it.", "e", p3));
                        msgs.Enqueue(new msg(testFace, () => "It did help me through a tough time years ago.", "e", p3));

                        break;
                    }
            }

            speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
            speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
            speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);

            msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();



            if (!playerData.libraryBooks.Contains(""))
            {
                d.destroy();
                itemMenu.transform.position = new Vector3(-100, -100, 0);
                this.gameObject.transform.position = new Vector3(-100, -100, 0);

                StartCoroutine(allFound());
                StopCoroutine(select());
            }

        }

        
        itemMenu.transform.position = new Vector3(-100, -100, 0);
        this.gameObject.transform.position = new Vector3(-100, -100, 0);
        d.destroy();

        yield return new WaitUntil(() => !msgController.inText);


        active = false;
    }    

    public IEnumerator allFound()
    {
        yield return new WaitUntil(() => !msgController.inText);

        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];
        GameObject p3 = speakers[2];

        msgs.Enqueue(new msg(testFace, () => "...!", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "You've done so much for me, now I am a little closer to bringing this place back to it's former glory.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Here, let me get a little something for you, you've earned it.", "e", p3));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
        speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);

        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

        yield return new WaitUntil(() => !msgController.inText);

        int animProgress = 0;
        librarian.GetComponent<SpriteRenderer>().sprite = up[animProgress];
        barricade.transform.Translate(-2, 0, 0);
        

        for (int i = 0;i < (1 / speed); i++)
        {
            librarian.transform.Translate(0, speed, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                librarian.GetComponent<SpriteRenderer>().sprite = up[animProgress];
            }
        }
        for (int i = 0; i < (6 / speed); i++)
        {
            librarian.transform.Translate(-speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                librarian.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }

        librarian.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1);

        librarian.GetComponent<SpriteRenderer>().enabled = true;
        librarian.GetComponent<SpriteRenderer>().flipX = true;

        for (int i = 0; i < (6 / speed); i++)
        {
            librarian.transform.Translate(speed, 0, 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                librarian.GetComponent<SpriteRenderer>().sprite = side[animProgress];
            }
        }

        librarian.GetComponent<SpriteRenderer>().flipX = false;

        for (int i = 0; i < (1 / speed); i++)
        {
            librarian.transform.Translate(0, -speed , 0);
            yield return new WaitForFixedUpdate();
            if (i % 10 == 9)
            {
                animProgress++;
                animProgress %= 4;
                librarian.GetComponent<SpriteRenderer>().sprite = back[animProgress];
            }
        }

        barricade.transform.Translate(2, 0, 0);

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
        speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);

        msgs.Enqueue(new msg(testFace, () => "Take this.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "A key?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Not just any key, this will let you into a secret library in another town.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "How do we find it?", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Don't worry.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "You will know it once you find it.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Any hints?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Not even I know where it is, but I know it's real.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Good luck out there.", "e", p3));

        playerData.items.Add("Librarian's key");
        playerData.secretLibraryKey = true;

        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();


        active = false;
    }
}
