using System.Collections;
using UnityEngine;

public class cantReach : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
      
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("There's something on top of this shelf.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("I can't reach it though.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
