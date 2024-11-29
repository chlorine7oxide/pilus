using UnityEngine;

public class courage : Gene
{
    public courage(Sprite s, Sprite t, Sprite b, Sprite j)
    {
        name = "Courage";
        description = "Nobody can stop you! Lets you insult the enemy.";
        type = "normal";
        icon = s;
        equipped = true;
        jar = j;
        top = t;
        bottom = b;
    }
}
