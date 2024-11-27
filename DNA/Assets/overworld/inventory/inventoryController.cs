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
    }

    public void enterGene()
    {
        gene.transform.position = inv.transform.position;
        inv.transform.position = new Vector3(1000, 0, 0);
    }

    public void toBase()
    {
        inv.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        gene.transform.position = new Vector3(1000, 0, 0);
        StopAllCoroutines();
        StartCoroutine(mainSel());
    }

    /*
    public void enterItem()
    {

    }

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
                //StartCoroutine(eyeWarning());
                break;
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
            toBase();
            yield break;
        }

        playerData.equiped[pos] = g[d.result];
        geneButtons[pos].GetComponent<SpriteRenderer>().sprite = g[d.result].icon;
        d.destroy();

        StartCoroutine(geneMenuSel());
    }

    /*
    public IEnumerator itemSel()
    {

    }

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
            case 1:
                enterGene();
                StartCoroutine(geneMenuSel());
                break;

                /*
            case 0:
                enterItem();
                StartCoroutine(itemSel());
                break;
            case 2:
                enterSetting();
                StartCoroutine(settings());
                break;
                */
        }

        s.destroy();

    }

}
