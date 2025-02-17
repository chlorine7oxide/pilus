using System.Collections;
using UnityEngine;

public class vaultKey : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A key!", portrait, null);

        yield return new WaitUntil(() => t.done);

        t.changeText("I wonder where we can use it?");

        yield return new WaitUntil(() => t.done);

        t.destroy();

        playerData.items.Add("Townhall Key");
        Destroy(gameObject);
    }
}
