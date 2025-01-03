using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class inventoryController : MonoBehaviour
{
    public GameObject inv;
    public GameObject[] baseButtons;

    public GameObject gene;
    public GameObject[] geneButtons;
    public GameObject[] eyebuttons;
    public GameObject[] armButtons;
    public GameObject[] generalButtons;

    public GameObject item;
    public GameObject[] itemButtons;
    public GameObject[] itemOptionButtons;
    public GameObject portrait;

    public static GameObject money;

    public Sprite sel;

    public Sprite itemHeld;

    public bool active = false;

    public GameObject textItem1, textItem2, textItem3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !active && GameObject.FindGameObjectWithTag("dialogue") == null)
        {
            open();
            active = true;
            StartCoroutine(mainSel());
        }
        else if (Input.GetKeyDown(KeyCode.C) && active)
        {
            close();
            active = false;
            StopAllCoroutines();
        }
    }

    public void open()
    {
        inv.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + 10 * Vector3.forward;
        money.transform.position = new Vector3(-480 + 960, -350 + 540, 0);
        money.GetComponent<TextMeshProUGUI>().text = playerData.money.ToString() + " money";
    }

    public void close()
    {
        inv.transform.position = new Vector3(1000, 0, 0);
        gene.transform.position = new Vector3(1000, 0, 0);
        item.transform.position = new Vector3(1000, 0, 0);
        money.transform.position = new Vector3(10000, 0, 0);
    }

    public void enterGene()
    {
        gene.transform.position = inv.transform.position;
        inv.transform.position = new Vector3(1000, 0, 0);
        money.transform.position = new Vector3(10000, 0, 0);
    }

    public void toBase()
    {
        inv.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + 10 * Vector3.forward;
        gene.transform.position = new Vector3(1000, 0, 0);
        item.transform.position = new Vector3(1000, 0, 0);
        money.transform.position = new Vector3(-480 + 960, -350 + 540, 0);
        StopAllCoroutines();
        StartCoroutine(mainSel());
    }

    
    public void enterItem()
    {
        item.transform.position = inv.transform.position;
        inv.transform.position = new Vector3(1000, 0, 0);
        money.transform.position = new Vector3(10000, 0, 0);
    }

    /*
    public void enterSetting()
    {

    }
    */

    public IEnumerator geneMenuSel()
    {
        Debug.Log("Gene");
        staticSelector s = staticSelector.create(geneButtons, 2, sel, new Vector3(-1, 0, 0));

        yield return new WaitUntil(() => (s.done || Input.GetKeyDown(KeyCode.X)));

        if (!s.done)
        {
            toBase();
            yield break;
        }

        switch (s.result)
        {
            case 0:
                generalText i = generalText.create("I don't think it's a good idea to take this one out.", inventoryTester.port, null);
                yield return new WaitUntil(() => i.done);
                i.destroy();
                StartCoroutine(geneMenuSel());
                yield break;
            case 1:
                StartCoroutine(eyeSel());
                break;
            case 2:
                StartCoroutine(armSel(2));
                break;
            case 3:
                StartCoroutine(armSel(3));
                break;
            case 4:
                StartCoroutine(generalSel(4));
                break;
            case 5:
                StartCoroutine(generalSel(5));
                break;
            case 6:
                StartCoroutine(generalSel(6));
                break;
            case 7:
                StartCoroutine(generalSel(7));
                break;
        }

        s.destroy();
    }

    /*
    public IEnumerator eyeWarning()
    {

    }
    */

    public IEnumerator eyeSel()
    {
        geneDescription.create("eye", eyebuttons[1]);

        Gene[] e = playerData.eyes.ToArray();
        List<Sprite> jars = new();
        List<Sprite> tops = new();
        List<Sprite> bottoms = new();

        for (int i = 0; i < playerData.eyes.Count; i++)
        {
            jars.Add(playerData.eyes[i].jar);
            tops.Add(playerData.eyes[i].top);
            bottoms.Add(playerData.eyes[i].bottom);
        }

        dynamicSelector d = dynamicSelector.create(eyebuttons, jars.ToArray(), sel, tops.ToArray(), bottoms.ToArray(), new Vector3(0, -2, 0));

        yield return new WaitUntil(() => d.done || Input.GetKeyDown(KeyCode.X));

        if (!d.done)
        {
            d.destroy();
            toBase();
            yield break;
        }

        playerData.equiped[1] = e[d.result];
        geneButtons[1].GetComponent<SpriteRenderer>().sprite = e[d.result].icon;

        d.destroy();

        StartCoroutine(geneMenuSel());

    }

    public IEnumerator armSel(int pos)
    {
        if (!playerData.armWarning)
        {
            generalText g = generalText.createAt("Having both arms the same will definetely make them more powerful.", inventoryTester.port, null, Vector3.zero);
            playerData.armWarning = true;
            yield return new WaitUntil(() => g.done);
            g.destroy();
        }

        geneDescription.create("arm", armButtons[1]);

        Gene[] e = playerData.arms.ToArray();
        List<Sprite> jars = new();
        List<Sprite> tops = new();
        List<Sprite> bottoms = new();

        for (int i = 0; i < playerData.arms.Count; i++)
        {
            jars.Add(playerData.arms[i].jar);
            tops.Add(playerData.arms[i].top);
            bottoms.Add(playerData.arms[i].bottom);
        }

        dynamicSelector d = dynamicSelector.create(armButtons, jars.ToArray(), sel, tops.ToArray(), bottoms.ToArray(), new Vector3(0, -2, 0));

        yield return new WaitUntil(() => d.done || Input.GetKeyDown(KeyCode.X));

        if (!d.done)
        {
            d.destroy();
            toBase();
            yield break;
        }

        playerData.equiped[pos] = e[d.result];
        geneButtons[pos].GetComponent<SpriteRenderer>().sprite = e[d.result].icon;
        d.destroy();

        StartCoroutine(geneMenuSel());
    }

    public IEnumerator generalSel(int pos)
    {
        geneDescription.create("gene", generalButtons[1]);

        Gene[] e = playerData.genes.ToArray();
        List<Gene> available = new();
        List<Sprite> jars = new();
        List<Sprite> tops = new();
        List<Sprite> bottoms = new();

        for (int i = 0; i < playerData.genes.Count; i++)
        {
            if (!playerData.equiped.Contains(playerData.genes[i]))
            {
                available.Add(playerData.genes[i]);
            }
            jars.Add(playerData.genes[i].jar);
            tops.Add(playerData.genes[i].top);
            bottoms.Add(playerData.genes[i].bottom);
        }

        dynamicSelector d = dynamicSelector.create(generalButtons, jars.ToArray(), sel, tops.ToArray(), bottoms.ToArray(), new Vector3(0, -2, 0));

        yield return new WaitUntil(() => d.done || Input.GetKeyDown(KeyCode.X));

        if (!d.done)
        {
            d.destroy();
            toBase();
            yield break;
        }

        if (available.Contains(e[d.result]))
        {
            playerData.equiped[pos] = e[d.result]; 
            geneButtons[pos].GetComponent<SpriteRenderer>().sprite = e[d.result].icon;
        }
        else
        {
            generalText g = generalText.create("I already have this gene equiped.", inventoryTester.port, null);
            yield return new WaitUntil(() => g.done);
            g.destroy();
        }

        
        d.destroy();

        StartCoroutine(geneMenuSel());
    }

    
    public IEnumerator itemSel()
    {
        string[] items = playerData.items.ToArray();

        Vector3[] pos = new Vector3[5];
        for(int i = 0;i < 5; i++)
        {
            pos[i] = itemButtons[i].transform.position + new Vector3(1.5f, -0.2f, 0);
        }

        if (items.Length == 0)
        {
            items = new string[] { "No items" };
        }

        dynamicSelectorText d = dynamicSelectorText.create(pos, items, sel, new Vector3(-4, 0.15f, 0));

        yield return new WaitUntil(() => d.done || Input.GetKeyDown(KeyCode.X));

        if (!d.done)
        {
            d.destroy();
            toBase();
            yield break;
        }

        if (items[0] == "No items")
        {
            d.destroy();
            StartCoroutine(itemSel());
            yield break;
        }

        string item = d.options[d.pos];

        d.destroy();

        staticSelector s = staticSelector.create(itemOptionButtons, 1, sel);
        textItem1.transform.position = new Vector3(-300, -200, 0) + new Vector3(960, 540, 0);
        textItem2.transform.position = new Vector3(-300, -300, 0) + new Vector3(960, 540, 0);
        textItem3.transform.position = new Vector3(-300, -400, 0) + new Vector3(960, 540, 0);

        yield return new WaitUntil(() => s.done || Input.GetKeyDown(KeyCode.X));

        if (!s.done)
        {
            s.destroy();
            StartCoroutine(itemSel());
            yield break;
        }

        switch (s.result)
        {
            case 0: // use
                switch (item)
                {
                    case "Giant Rock":
                        itemDialogue i = itemDialogue.create("I'm carrying it, somehow...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i.done);
                        i.destroy();
                        playerData.items.Remove(item);
                        carryableItem.create(inventoryTester.giantRock, "Giant Rock", GameObject.FindGameObjectWithTag("player").transform.position, false, true);
                        break;
                    case "item2":
                        itemDialogue i2 = itemDialogue.create("item 2", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i2.done);
                        i2.destroy();
                        i2 = itemDialogue.create("used", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i2.done);
                        i2.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item3":
                        itemDialogue i3 = itemDialogue.create("cant use item 3", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i3.done);
                        i3.destroy();
                        break;
                    case "item4":
                        itemDialogue i4 = itemDialogue.create("item 4", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i4.done);
                        i4.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item5":
                        itemDialogue i5 = itemDialogue.create("item 5", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i5.done);
                        i5.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item6":
                        itemDialogue i6 = itemDialogue.create("item 6", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "Desk Key":
                        itemDialogue i7 = itemDialogue.create("There's no lock, how do I use a key here?", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i7.done);
                        i7.destroy();
                        break;
                    case "Old Computer":
                        itemDialogue i8 = itemDialogue.create("It's broken.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i8.done);
                        i8.destroy();
                        break;
                    case "FishKey":
                        itemDialogue i9 = itemDialogue.create("What do I do with this?", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i9.done);
                        i9.destroy();
                        break;
                } // item use funtionality and text
                break;
            case 1:// drop
                
                switch (item) { 
                    case "Giant Rock":
                        itemDialogue i = itemDialogue.create("Finally, I don't have to carry that around...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i.done);
                        carryableItem.create(inventoryTester.giantRock, "Giant Rock", GameObject.FindGameObjectWithTag("player").transform.position, false, false);
                        i.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item2":
                        itemDialogue i2 = itemDialogue.create("item 2 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i2.done);
                        i2.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item3":
                        itemDialogue i3 = itemDialogue.create("item 3 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i3.done);
                        i3.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item4":
                        itemDialogue i4 = itemDialogue.create("item 4 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i4.done);
                        i4.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item5":
                        itemDialogue i5 = itemDialogue.create("item 5 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i5.done);
                        i5.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "item6":
                        itemDialogue i6 = itemDialogue.create("item 6 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        i6 = itemDialogue.create("tragic...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "Desk Key":
                        itemDialogue i7 = itemDialogue.create("This feels to important to throw out.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i7.done);
                        i7.destroy();
                        break;
                    case "Old Computer":
                        itemDialogue i8 = itemDialogue.create("I guess I didn't really need it...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i8.done);
                        i8.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "FishKey":
                        itemDialogue i9 = itemDialogue.create("I should keep this.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i9.done);
                        i9.destroy();
                        break;
                } // item drop text and functionality
                break;
            case 2: // inspect
                switch (item)
                {
                    case "Giant Rock":
                        itemDialogue i = itemDialogue.create("It's a huge boulder, that I, for some reason, decided to pick up.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i.done);
                        i.destroy();
                        break;
                    case "item2":
                        itemDialogue i2 = itemDialogue.create("its an item", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i2.done);
                        i2.destroy();
                        break;
                    case "item3":
                        itemDialogue i3 = itemDialogue.create("its an item", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i3.done);
                        i3.destroy();
                        break;
                    case "item4":
                        itemDialogue i4 = itemDialogue.create("its an item", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i4.done);
                        i4.destroy();
                        break;
                    case "item5":
                        itemDialogue i5 = itemDialogue.create("its an item", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i5.done);
                        i5.destroy();
                        break;
                    case "item6":
                        itemDialogue i6 = itemDialogue.create("ahh", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        i6 = itemDialogue.create("indeed", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        i6 = itemDialogue.create("its an item", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        break;
                    case "Desk Key":
                        itemDialogue i7 = itemDialogue.create("It's a small key, I'm not sure what it's used for.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i7.done);
                        i7.destroy();
                        break;
                    case "Old Computer":
                        itemDialogue i8 = itemDialogue.create("A computer from at least a decade ago, it doesn't work", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i8.done);
                        i8.destroy();
                        break;
                    case "FishKey":
                        itemDialogue i9 = itemDialogue.create("This came from the bottom of the lake, I doubt I can find where to use it.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i9.done);
                        i9.destroy();
                        break;
                } // item inspect text and functionality
                break;
        }

        textItem1.transform.position = new Vector3(-3000, -200, 0);
        textItem2.transform.position = new Vector3(-3000, -300, 0);
        textItem3.transform.position = new Vector3(-3000, -400, 0);

        StartCoroutine(itemSel());
        yield break;
    }

    /*
    public IEnumerator settings()
    {

    }
    */

    public IEnumerator mainSel()
    {
        staticSelector s = staticSelector.create(baseButtons, 1, sel, new Vector3(-3.5f, 0, 0));
        
        yield return new WaitUntil(() => s.done);

        switch (s.result)
        {
            case 0:
                enterGene();
                StartCoroutine(geneMenuSel());
                break;

                
            case 1:
                enterItem();
                StartCoroutine(itemSel());
                break;
                /*
            case 2:
                enterSetting();
                StartCoroutine(settings());
                break;
                */
        }

        s.destroy();

    }

}
