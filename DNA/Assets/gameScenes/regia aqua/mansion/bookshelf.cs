using System.Collections;
using UnityEngine;

public class bookshelf : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A small bookshelf marked \"returns\".", portrait, null);
        if (t == null)
        {
            yield break;
        }
        if (!playerData.items.Contains("Drilling Book"))
        {
            yield return new WaitUntil(() => t.done);
            t.changeText("There's only one book sitting on it.");
            yield return new WaitUntil(() => t.done);
            t.changeText("It's a book documenting the process of making this town, it's mostly filled with drilling information.");
            yield return new WaitUntil(() => t.done);
            t.changeText("Maybe someone might find this useful, I should check it out.");
            yield return new WaitUntil(() => t.done);
            playerData.items.Add("Drilling Book");
        }
        t.destroy();
    }
}
