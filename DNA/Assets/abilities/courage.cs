using UnityEngine;

public class courage : Gene
{
    public courage(Sprite s)
    {
        name = "Courage";
        description = "Nobody can stop you! Lets you insult the enemy.";
        type = "normal";
        icon = s;
        equipped = true;
    }
}
