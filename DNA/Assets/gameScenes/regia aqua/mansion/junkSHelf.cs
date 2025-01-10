using System.Collections;
using UnityEngine;

public class junkSHelf : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A shelf filled with old, useless junk.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("I don't even know what some of this stuff is.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
