using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class decision : textBox
{
    protected GameObject text2;
    public Vector3 text2pos;

    public GameObject controller;
    public bool isUp = true;
    public bool complete = false;

    public static Sprite selector;

    public decision(string name, string text, bool isRight, string text2) : base(name, text, isRight)
    {
        this.text2 = GameObject.Instantiate(textPrefab);
        this.text2.GetComponent<TextMeshPro>().text = text2;

        int height = (int)(text.Length + text2.Length / charsPerLine) + 1;
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

        controller = new();
        controller.AddComponent<SpriteRenderer>();
        controller.AddComponent<decisionController>();
        controller.GetComponent<decisionController>().d = this;
        controller.GetComponent<decisionController>().pos = box.transform.position;
        controller.GetComponent<SpriteRenderer>().sprite = selector;

        this.text2.GetComponent<TextMeshPro>().sortingOrder = 11;

        this.text2.transform.position = box.transform.position + textpos - new Vector3(0, 0.25f, 0);
        this.text.transform.position = box.transform.position + textpos + new Vector3(0, 0.25f, 0);

        controller.GetComponent<decisionController>().text = this.text;
        controller.GetComponent<decisionController>().text2 = this.text2;

    }

    public override void move(Vector3 v)
    {
        base.move(v);
        text2.transform.Translate(v);
    }

    public override void moveTo(Vector3 v)
    {
        base.moveTo(v);
        if (text2 is not null)
        {
            text2.transform.Translate(v);
            text2.transform.Translate(new Vector3(0, -0.25f, 0));
        }
        else
        {
            text.transform.Translate(new Vector3(0, -0.25f, 0));
        }
        if (text is not null)
        {
            text.transform.Translate(new Vector3(0, 0.25f, 0));
        }
        else
        {
            text2.transform.Translate(new Vector3(0, 0.25f, 0));
        }
        
    }

    public void endDecision(bool isup)
    {
        GameObject.Destroy(controller);
        if (isup)
        {
            GameObject.Destroy(text2);
            Debug.Log("destroyed text2");
        }
        else
        {
            GameObject.Destroy(text);
            Debug.Log("destroyed text");
        }
        isUp = isup;
        complete = true;
        
    }

    public override GameObject getText()
    {
        if (isUp)
        {
            return text;
        }
        else
        {
            return text2;
        }
        return null;
    }
}
