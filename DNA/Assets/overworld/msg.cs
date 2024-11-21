using UnityEngine;

public class msg
{
    public delegate string TextMsg();
    public Sprite face;
    public string name;
    public TextMsg text;
    public GameObject speaker;

    public msg(Sprite face, TextMsg text, string name, GameObject speaker)
    {
        this.face = face;
        this.text = text;
        this.name = name;
        this.speaker = speaker;
    }
}
