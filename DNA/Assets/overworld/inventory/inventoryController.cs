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
                        itemDialogue i9 = itemDialogue.create("There's no lock, how do I use a key here?", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i9.done);
                        i9.destroy();
                        break;
                    case "\"Aged\" wine":
                        itemDialogue i10 = itemDialogue.create("Please don't make me drink this...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i10.done);
                        i10.destroy();
                        break;
                    case "Library card":
                        itemDialogue i11 = itemDialogue.create("This isn't a library?", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i11.done);
                        i11.destroy();
                        break;
                    case "Drilling Book":
                        itemDialogue i12 = itemDialogue.create("I don't want to read this.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i12.done);
                        i12.destroy();
                        break;
                    case "Fountain pen":
                        itemDialogue i13 = itemDialogue.create("I don't have anything to write on.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i13.done);
                        i13.destroy();
                        i13 = itemDialogue.create("Or any ink.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i13.done);
                        i13.destroy();
                        break;
                    case "Red Seal":
                        itemDialogue i14 = itemDialogue.create("I'll use this when I need to seal something.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i14.done);
                        i14.destroy();
                        break;
                    case "Unlabeled drink":
                        itemDialogue i15 = itemDialogue.create("It didn't actually taste that bad.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i15.done);
                        i15.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "Marine life book":
                        itemDialogue i16 = itemDialogue.create("I don't think I need to read this again.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i16.done);
                        i16.destroy();
                        break;
                    case "Old rations":
                        itemDialogue i17 = itemDialogue.create("Just as I thought!", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i17.done);
                        i17.destroy();
                        i17 = itemDialogue.create("They taste terrible...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i17.done);
                        i17.destroy();
                        playerData.items.Remove(item);
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
                    case "\"Aged\" wine":
                        itemDialogue i10 = itemDialogue.create("I really didn't need to pick this up...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i10.done);
                        i10.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "Library card":
                        itemDialogue i11 = itemDialogue.create("It's counting on me to take care of it...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i11.done);
                        i11.destroy();
                        break;
                    case "Drilling Book":
                        itemDialogue i12 = itemDialogue.create("I can't get rid of this! It's a library book.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i12.done);
                        i12.destroy();
                        break;
                    case "Fountain pen":
                        itemDialogue i13 = itemDialogue.create("I think it would be immoral to throw this away now.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i13.done);
                        i13.destroy();
                        break;
                    case "Red Seal":
                        itemDialogue i14 = itemDialogue.create("What if I need to forge government documents?", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i14.done);
                        i14.destroy();
                        break;
                    case "Unlabeled drink":
                        itemDialogue i15 = itemDialogue.create("I didn't want to drink a decades old drink anyway.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i15.done);
                        i15.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "Marine life book":
                        itemDialogue i16 = itemDialogue.create("I can't get rid of this! It's a library book.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i16.done);
                        i16.destroy();
                        break;
                    case "Old rations":
                        itemDialogue i17 = itemDialogue.create("Probably the best choice, I don't think they would taste very good.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i17.done);
                        i17.destroy();
                        playerData.items.Remove(item);
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
                    case "\"Aged\" wine":
                        itemDialogue i10 = itemDialogue.create("An old bottle of wine I found, it smells terrible.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i10.done);
                        i10.destroy();
                        break;
                    case "Library card":
                        itemDialogue i11 = itemDialogue.create("An old library card, the name is worn off, but it's supposed to say MY name.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i11.done);
                        i11.destroy();
                        break;
                    case "Drilling Book":
                        itemDialogue i12 = itemDialogue.create("A book filled with mostly drilling information.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i12.done);
                        i12.destroy();
                        break;
                    case "Fountain pen":
                        itemDialogue i13 = itemDialogue.create("An ill-gotten fancy fountain pen.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i13.done);
                        i13.destroy();
                        i13 = itemDialogue.create("It's moral ambiguity makes it more valuable.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i13.done);
                        i13.destroy();
                        break;
                    case "Red Seal":
                        itemDialogue i14 = itemDialogue.create("A red mayorial seal. Apparently they're still used now.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i14.done);
                        i14.destroy();
                        break;
                    case "Unlabeled drink":
                        itemDialogue i15 = itemDialogue.create("A drink from a very long time ago", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i15.done);
                        i15.destroy();
                        i15 = itemDialogue.create("Dispite it's age, it looks like it could heal me in combat.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i15.done);
                        i15.destroy();
                        playerData.items.Remove(item);
                        break;
                    case "Marine life book":
                        itemDialogue i16 = itemDialogue.create("A book about the local marine life.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i16.done);
                        i16.destroy();
                        i16 = itemDialogue.create("It's a bit water damaged, but still readable.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i16.done);
                        i16.destroy();
                        break;
                    case "Old rations":
                        itemDialogue i17 = itemDialogue.create("Some old rations, they look like they're still good.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i17.done);
                        i17.destroy();
                        i17 = itemDialogue.create("Even after so long, they could probably heal me in combat.", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i17.done);
                        i17.destroy();
                        playerData.items.Remove(item);
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
