using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("The only thing on this shelf is a large rock.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
