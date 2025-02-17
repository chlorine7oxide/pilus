using System.Collections;
using UnityEngine;

public class vaultBook : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("This book is marked as library property!", portrait, null);

        yield return new WaitUntil(() => t.done);

        t.changeText("It's called \"How to make sure your books don't get stolen\".");

        yield return new WaitUntil(() => t.done);

        t.changeText("It seems like the librarian really needs this one.");

        yield return new WaitUntil(() => t.done);

        t.destroy();

        playerData.items.Add("Anti-stealing Book");
    }
}
