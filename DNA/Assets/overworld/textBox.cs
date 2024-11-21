using UnityEngine;
using TMPro;
using System;

public class textBox
{
    public GameObject box;
    public GameObject text, name;

    public static GameObject textPrefab;
    public static float charsPerLine = 50f, heightPerLine = 2f;
    public static Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;
    public Vector3 textpos, namePos;

    public textBox(string text, string name, bool isRight)
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
        textpos = new Vector3(0, (height - 1) * heightPerLine, 0);
        namePos = new Vector3(-5f, height * heightPerLine, 0);

        moveTo(new Vector3(0, -5f, 0));
    }

    public float getHeight()
    {
        return ((int)(text.GetComponent<TextMeshPro>().text.Length / charsPerLine) + 1) * heightPerLine;
    }

    public void move(Vector3 v)
    {
        box.transform.Translate(v);
        text.transform.Translate(v);
        name.transform.Translate(v);
    }

    public void moveTo(Vector3 v)
    {
        box.transform.position = v;
        text.transform.position = v;
        name.transform.position = v;
    }
}
