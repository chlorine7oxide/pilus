using System.Collections;
using UnityEngine;

public class book : overworldInteractable
{
    public Sprite portrait;
    public int bookNum;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t;
        switch (bookNum) {
            case 0:
                {
                    t = generalText.create("A bookshelf full of red books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a book titled \"0 men left behind\" about war tactics.");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 1:
                {
                    t = generalText.create("A bookshelf full of blue books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a self help book titled \"Number 1 habits\".");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 2:
                {
                    t = generalText.create("A bookshelf full of yellow books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a fantasy book titled \"Council of 2\".");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 3:
                {
                    t = generalText.create("A bookshelf full of grey books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a book titled \"The 3 types of rock\" about geology.");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 4:
                {
                    t = generalText.create("A bookshelf full of black books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a book titled \"4 secrets to partying\" about event planning.");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 5:
                {
                    t = generalText.create("A bookshelf full of pink books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a book titled \"5 ways to force people to like you\", it seems a little problematic...");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 6:
                {
                    t = generalText.create("A bookshelf full of green books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a cook book titled \"6 ways to smoke trout\".");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 7:
                {
                    t = generalText.create("A bookshelf full of white books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a book titled \"Guide to the 7 days of the week\" about efficiency in the workplace.");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 8:
                {
                    t = generalText.create("A bookshelf full of purple books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's a book titled \"8 legs: sea vs land\" that compares spiders and octopii.");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
            case 9:
                {
                    t = generalText.create("A bookshelf full of orange books.", portrait, null);
                    if (t == null)
                    {
                        yield break;
                    }
                    yield return new WaitUntil(() => t.done);
                    t.changeText("They're mostly destroyed, but one seems to be intact.");
                    yield return new WaitUntil(() => t.done);
                    t.changeText("It's an astronomy book titled \"9 things you didn't know about the moon\".");
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                }
        }

        playerData.visitedLibrary = true;



    }
}


/*
red = 0
blue  = 1
yellow = 2
grey = 3
black = 4
pink = 5
green = 6
white = 7
purple = 8
orange = 9
*/