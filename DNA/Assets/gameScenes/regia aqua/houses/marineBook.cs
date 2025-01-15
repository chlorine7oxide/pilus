using System.Collections;
using UnityEngine;

public class marineBook : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A bookshelf full of water damaged books.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);


        if (!playerData.items.Contains("Marine life book"))
        {
            t.changeText("The only one remotely salvagable is missing it's cover.");
            yield return new WaitUntil(() => t.done);
            t.changeText("It reads \"A diverse ecosystem of wildlife exists under our feet,\"");
            yield return new WaitUntil(() => t.done);
            t.changeText("\"with biodiversity the like we haven’t seen before.\"");
            yield return new WaitUntil(() => t.done);
            t.changeText("\"Even though there exist many predators to prey on the smaller herbivorous fish,\"");
            yield return new WaitUntil(() => t.done);
            t.changeText("\"there are suprisingly many freshwater trout living in the shallows.\"");
            yield return new WaitUntil(() => t.done);
            t.changeText("\"Local fish school movements suggest the existence of an apex predator in the deeper waters,\"");
            yield return new WaitUntil(() => t.done);
            t.changeText("\"the existance of which is yet to be confirmed.\"");
            yield return new WaitUntil(() => t.done);
            t.changeText("The local library would definetely take this.");
            yield return new WaitUntil(() => t.done);

            playerData.items.Add("Marine life book");
        }
        

        t.destroy();
    }
}
