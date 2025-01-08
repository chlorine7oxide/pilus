using System.Collections;
using UnityEngine;

public class document1 : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("An official looking document.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("It reads \"Declaration of martial law\"");
        yield return new WaitUntil(() => t.done); 
        t.changeText("\"In these dangerous times we need to take durastic measures to make sure the people stay safe.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText("It continues for a while more with legal jargon.");
        yield return new WaitUntil(() => t.done);
        playerData.documentsRead++;
        t.destroy();
    }
}
