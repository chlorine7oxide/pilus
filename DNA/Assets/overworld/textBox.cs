using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class textBox
{
    public GameObject box;
    public GameObject name;
    protected GameObject text;

    public static GameObject textPrefab;
    public static float charsPerLine = 26f, heightPerLine = 0.35f;
    public static Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;
    public Vector3 textpos, namePos;

    public textBox(string name, string text, bool isRight)
    {
        this.text = GameObject.Instantiate(textPrefab);
        this.text.GetComponent<TextMeshPro>().text = text;
        this.name = GameObject.Instantiate(textPrefab);
        this.name.GetComponent<TextMeshPro>().text = name;

        int height = (int)(text.Length / charsPerLine) + 1;
        box = new();
        box.AddComponent<SpriteRenderer>();
        switch (height)
        {
            case 1:
                box.GetComponent<SpriteRenderer>().sprite = boxSprite1;
                break;
            case 2:
                box.GetComponent<SpriteRenderer>().sprite = boxSprite2;
                break;
            case 3:
                box.GetComponent<SpriteRenderer>().sprite = boxSprite3;
                break;
            case 4:
                box.GetComponent<SpriteRenderer>().sprite = boxSprite4;
                break;
        }
        //textpos = new Vector3(0, (height - 1) * heightPerLine - 0.25f * (height - 1), 0);
        textpos = Vector3.zero;
        namePos = new Vector3(-5f, height * heightPerLine, 0);

        box.GetComponent<SpriteRenderer>().flipX = isRight;

        box.GetComponent<SpriteRenderer>().sortingOrder = 10;
        this.text.GetComponent<TextMeshPro>().sortingOrder = 11;
        this.name.GetComponent<TextMeshPro>().sortingOrder = 11;
        this.text.transform.position = box.transform.position;

        moveTo(new Vector3(0, -5f, 0));
    }

    public virtual float getHeight()
    {
        return ((int)(text.GetComponent<TextMeshPro>().text.Length / charsPerLine) + 1) * heightPerLine + 1.15f;
    }

    public virtual void move(Vector3 v)
    {
        box.transform.Translate(v);
        name.transform.Translate(v);
        if (text is not null)
        {
            text.transform.Translate(v);
        }

        
    }

    public virtual void moveTo(Vector3 v)
    {
        box.transform.position = v;
        name.transform.position = v + namePos;
        if (text is not null)
        {
            text.transform.position = v + textpos;
        }
    }

    public virtual GameObject getText()
    {
        return text;
    }
}
