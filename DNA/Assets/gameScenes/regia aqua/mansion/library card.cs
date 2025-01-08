using System.Collections;
using UnityEngine;

public class librarycard : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("An abandoned library card.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("The name was worn off long ago.");
        yield return new WaitUntil(() => t.done);
        t.changeText("And the photo is of a middle aged man.");
        yield return new WaitUntil(() => t.done);
        t.changeText("But it's mine now - I'll give it a nice home.");
        yield return new WaitUntil(() => t.done);
        playerData.items.Add("Library card");
        t.destroy();
        Destroy(this.gameObject);
    }
}
