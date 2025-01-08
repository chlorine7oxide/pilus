using System.Collections;
using UnityEngine;

public class chair : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A stray chair.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("It looks so lost without its table.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
