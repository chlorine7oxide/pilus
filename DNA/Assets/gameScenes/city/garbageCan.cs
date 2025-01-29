using System.Collections;
using UnityEngine;

public class garbageCan : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A Garbage can.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        if (!playerData.garbageBook)
        {
            t.changeText("Inside is an old book.");
            yield return new WaitUntil(() => t.done);
            t.changeText("It seems to be a guide to making freinds.");
            yield return new WaitUntil(() => t.done);
            t.changeText("Hopefully it's owner used it well.");
            yield return new WaitUntil(() => t.done);
            playerData.items.Add("Friendship Book");
            playerData.garbageBook = true;
        }
        t.destroy();
    }
}
