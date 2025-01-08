using System.Collections;
using UnityEngine;

public class registry : overworldInteractable
{
    public Sprite portrait, portrait2;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A library book registry.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        if (!playerData.libraryRegistryRead)
        {
            
            t.changeText("There's at least of decade of history in here.");
            yield return new WaitUntil(() => t.done);
            playerData.libraryRegistryRead = true;
        }
        if (playerData.items.Contains("Drilling Book") && playerData.items.Contains("Library card"))
        {
            t.destroy();
            t = generalText.create("What are you doing?", portrait2, null);
            if (t == null)
            {
                yield break;
            }
            yield return new WaitUntil(() => t.done);
            t.destroy();
            t = generalText.create("I'm just checking out a book.", portrait, null);
            if (t == null)
            {
                yield break;
            }
            yield return new WaitUntil(() => t.done);
            t.destroy();
            t = generalText.create("What?", portrait2, null);
            if (t == null)
            {
                yield break;
            }
            yield return new WaitUntil(() => t.done);
            t.destroy();
            t = generalText.create("I don't want to take it illegally.", portrait, null);
            if (t == null)
            {
                yield break;
            }
            yield return new WaitUntil(() => t.done);
            t.changeText("It's a matter of principle.");
            yield return new WaitUntil(() => t.done);
            t.changeText("I need to check it out properly to take it.");
            yield return new WaitUntil(() => t.done);
            t.destroy();
            t = generalText.create("That's not even your library card...", portrait2, null);
            if (t == null)
            {
                yield break;
            }
            yield return new WaitUntil(() => t.done);
        } else if (playerData.items.Contains("Drilling Book") && !playerData.items.Contains("Library card"))
        {
            t.changeText("If only I could take that book officially");
            yield return new WaitUntil(() => t.done);
        }
        else if (!playerData.items.Contains("Drilling Book") && playerData.items.Contains("Library card"))
        {
            t.changeText("What good is a library card if I can't get any books with it.");
            yield return new WaitUntil(() => t.done);
        }
        else
        {
            t.changeText("I can't use it without a library card and a book.");
            yield return new WaitUntil(() => t.done);
        }
        
        t.destroy();
    }
}
