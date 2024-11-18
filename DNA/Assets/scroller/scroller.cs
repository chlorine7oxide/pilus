using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scroller
{
    public GameObject[] buttons;
    public GameObject[] texts;
    protected int numExtras;
    protected float width;
    protected float height;
    protected string[] data;
    public GameObject controller;

    public static string result = null;

    public static Sprite box, arrow;
    public static GameObject buttonprefab, textprefab;

    public scroller(float width, float height, string[] data, int numExtras)
    {
        this.numExtras = numExtras;
        this.width = width;
        this.height = height;
        this.data = data;

        buttons = new GameObject[2 * numExtras + 1];
        texts = new GameObject[2 * numExtras + 1];

        for (int i = 0; i < numExtras; i++)
        {
            this.buttons[i] = GameObject.Instantiate(buttonprefab);
            this.buttons[i].transform.Translate(new Vector3(0, (i + 1) * height / (2 * numExtras + 1)), 0);
        }
        this.buttons[numExtras] = GameObject.Instantiate(buttonprefab);
        for (int i = 0; i < numExtras; i++)
        {
            this.buttons[i + numExtras + 1] = GameObject.Instantiate(buttonprefab);
            this.buttons[i + numExtras + 1].transform.Translate(new Vector3(0, -(i + 1) * height / (2 * numExtras + 1)), 0);
        }

        controller = new GameObject();

        for(int i = 0; i < buttons.Length;i++)
        {
            buttons[i].transform.localScale = new Vector3(width, height / (2 * numExtras + 1), 1);
            texts[i] = GameObject.Instantiate(textprefab);
            texts[i].transform.Translate(new Vector3(0, -(i - numExtras) * height / (2 * numExtras + 1)), 0);
        }

        controller.AddComponent<scrollercontroller>();
        controller.GetComponent<scrollercontroller>().texts = texts;
        controller.GetComponent<scrollercontroller>().data = data;
        controller.GetComponent<scrollercontroller>().center = 0;
        controller.GetComponent<scrollercontroller>().numExtras = numExtras;
    }
}
