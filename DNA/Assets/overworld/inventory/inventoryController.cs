using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    public Sprite sel;

    public bool active = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !active)
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
        inv.transform.position = GameObject.FindGameObjectWithTag("player").transform.position;
    }

    public void close()
    {
        inv.transform.position = new Vector3(1000, 0, 0);
        gene.transform.position = new Vector3(1000, 0, 0);
        item.transform.position = new Vector3(1000, 0, 0);
    }

    public void enterGene()
    {
        gene.transform.position = inv.transform.position;
        inv.transform.position = new Vector3(1000, 0, 0);
    }

    public void toBase()
    {
        inv.transform.position = GameObject.FindGameObjectWithTag("player").transform.position;
        gene.transform.position = new Vector3(1000, 0, 0);
        item.transform.position = new Vector3(1000, 0, 0);
        StopAllCoroutines();
        StartCoroutine(mainSel());
    }

    
    public void enterItem()
    {
        item.transform.position = inv.transform.position;
        inv.transform.position = new Vector3(1000, 0, 0);
    }

    /*
    public void enterSetting()
    {

    }
    */

    public IEnumerator geneMenuSel()
    {
        Debug.Log("Gene");
        staticSelector s = staticSelector.create(geneButtons, 2, sel);

        yield return new WaitUntil(() => (s.done || Input.GetKeyDown(KeyCode.X)));

        if (!s.done)
        {
            toBase();
            yield break;
        }

        switch (s.result)
        {
            case 0:
                itemDialogue i = itemDialogue.create("I don't think it's a good idea to take this one out.", inventoryTester.port, geneButtons[0].transform.position);
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
        geneDescription.create("eye", eyebuttons[1].transform.position + new Vector3(0.5f, 0, 0));

        Gene[] e = playerData.eyes.ToArray();
        List<Sprite> eyeIcons = new();

        for (int i = 0; i < playerData.eyes.Count; i++)
        {
            eyeIcons.Add(playerData.eyes[i].icon);
        }

        dynamicSelector d = dynamicSelector.create(eyebuttons, eyeIcons.ToArray(), sel);

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
        geneDescription.create("arm", armButtons[1].transform.position + new Vector3(0.5f, 0, 0));

        Gene[] a = playerData.arms.ToArray();
        List<Sprite> armIcons = new();

        for (int i = 0; i < playerData.arms.Count; i++)
        {
            armIcons.Add(playerData.arms[i].icon);
        }

        dynamicSelector d = dynamicSelector.create(armButtons, armIcons.ToArray(), sel);

        yield return new WaitUntil(() => d.done || Input.GetKeyDown(KeyCode.X));

        if (!d.done)
        {
            d.destroy();
            toBase();
            yield break;
        }

        playerData.equiped[pos] = a[d.result];
        geneButtons[pos].GetComponent<SpriteRenderer>().sprite = a[d.result].icon;
        d.destroy();

        StartCoroutine(geneMenuSel());
    }

    public IEnumerator generalSel(int pos)
    {
        geneDescription.create("gene", generalButtons[1].transform.position + new Vector3(0.5f, 0, 0));

        Gene[] g = playerData.genes.ToArray();
        List<Sprite> geneIcons = new();

        for (int i = 0; i < playerData.genes.Count; i++)
        {
            geneIcons.Add(playerData.genes[i].icon);
        }

        dynamicSelector d = dynamicSelector.create(generalButtons, geneIcons.ToArray(), sel);

        yield return new WaitUntil(() => d.done || Input.GetKeyDown(KeyCode.X));

        if (!d.done)
        {
            d.destroy();
            toBase();
            yield break;
        }

        playerData.equiped[pos] = g[d.result];
        geneButtons[pos].GetComponent<SpriteRenderer>().sprite = g[d.result].icon;
        d.destroy();

        StartCoroutine(geneMenuSel());
    }

    
    public IEnumerator itemSel()
    {
        string[] items = playerData.items.ToArray();

        Vector3[] pos = new Vector3[5];
        for(int i = 0;i < 5; i++)
        {
            pos[i] = itemButtons[i].transform.position;
        }

        if (items.Length == 0)
        {
            items = new string[] { "No items" };
        }

        dynamicSelectorText d = dynamicSelectorText.create(pos, items, sel);

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
                    case "item1":
                        itemDialogue i = itemDialogue.create("item 1 used", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i.done);
                        i.destroy();
                        playerData.items.Remove(item);
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
                } // item use funtionality and text
                break;
            case 1:// drop
                playerData.items.Remove(item);
                switch (item) { 
                    case "item1":
                        itemDialogue i = itemDialogue.create("item 1 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i.done);
                        i.destroy();
                        break;
                    case "item2":
                        itemDialogue i2 = itemDialogue.create("item 2 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i2.done);
                        i2.destroy();
                        break;
                    case "item3":
                        itemDialogue i3 = itemDialogue.create("item 3 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i3.done);
                        i3.destroy();
                        break;
                    case "item4":
                        itemDialogue i4 = itemDialogue.create("item 4 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i4.done);
                        i4.destroy();
                        break;
                    case "item5":
                        itemDialogue i5 = itemDialogue.create("item 5 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i5.done);
                        i5.destroy();
                        break;
                    case "item6":
                        itemDialogue i6 = itemDialogue.create("item 6 dropped", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        i6 = itemDialogue.create("tragic...", inventoryTester.port, portrait.transform.position);
                        yield return new WaitUntil(() => i6.done);
                        i6.destroy();
                        break;
                } // item drop text and functionality
                break;
            case 2: // inspect
                switch (item)
                {
                    case "item1":
                        itemDialogue i = itemDialogue.create("its an item", inventoryTester.port, portrait.transform.position);
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
                } // item inspect text and functionality
                break;
        }

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
        staticSelector s = staticSelector.create(baseButtons, 1, sel);
        
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
