using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class bookSelector : MonoBehaviour
{
    public GameObject[] books;
    public Sprite selector, sel;
    public GameObject itemMenu, cameraObj;
    public bool active = false;
    public GameObject[] itemButtons;
    public GameObject[] bookObjs, indicatorObjs;

    public Sprite grey, greyB, blue, blueB;

    public IEnumerator select()
    {
        yield return new WaitForEndOfFrame();

        List<GameObject> l = new();
        for (int i = 0;i < 5; i++)
        {
            if (playerData.libraryBooks[i].Equals(""))
            {
                l.Add(books[i]);
                bookObjs[i].SetActive(false);
                indicatorObjs[i].SetActive(true);
            }
            else
            {
                bookObjs[i].SetActive(true);
                indicatorObjs[i].SetActive(false);
                if (i == 0 || i == 1 || i == 3)
                {
                    switch (playerData.libraryBooks[i])
                    {
                        case "Drilling Book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = grey;
                            break;
                        case "Marine life book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = blue;
                            break;
                    }
                }
                else
                {
                    switch (playerData.libraryBooks[i])
                    {
                        case "Drilling Book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = greyB;
                            break;
                        case "Marine life book":
                            bookObjs[i].GetComponent<SpriteRenderer>().sprite = blueB;
                            break;

                    }

                }
            }
        }

        if (l.Count == 0)
        {
            yield break;
        }

        active = true;
        staticSelector s = staticSelector.create(l.ToArray(), l.Count, selector);
        yield return new WaitUntil(() => s.done || Input.GetKeyDown(KeyCode.X));

        if (!s.done)
        {
            active = false;
            s.destroy();
            this.gameObject.transform.position = new Vector3(-100, -100, 0);
            yield break;
        }

        int sRes = s.result;

        s.destroy();

        this.gameObject.transform.position = new Vector3(-100, -100, 0);
        itemMenu.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);

        string[] items = playerData.items.ToArray();
        Vector3[] pos = new Vector3[5];
        for (int i = 0; i < 5; i++)
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
            active = false;
            itemMenu.transform.position = new Vector3(-100, -100, 0);
            StartCoroutine(select());
            this.gameObject.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 0);
            yield break;
        }

        if (items[d.result].ToLower().Contains("book"))
        {
            playerData.items.Remove(items[d.result]);
            for (int i = 0; i < 5; i++)
            {
                if (books[i].Equals(l[sRes]))
                {
                    playerData.libraryBooks[i] = items[d.result];
                }
            }
            print(playerData.libraryBooks[0]+"+"+ playerData.libraryBooks[1]+"+"+ playerData.libraryBooks[2]+"+"+ playerData.libraryBooks[3]+"+"+ playerData.libraryBooks[4]);
        }

        d.destroy();
        itemMenu.transform.position = new Vector3(-100, -100, 0);
        this.gameObject.transform.position = new Vector3(-100, -100, 0);

        active = false;
    }



    
    private void Start()
    {
        playerData.items.Add("Drilling Book");
        playerData.items.Add("Marine life book");
        playerData.items.Add("Marine life book");
        playerData.items.Add("Marine life book");
        playerData.items.Add("Marine life book");
    }
    
}
