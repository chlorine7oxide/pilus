using UnityEngine;

public class courage : Gene
{
    public courage()
    {
        name = "Courage";
        description = "Nobody can stop you! Lets you insult the enemy.";
        type = "normal";
        icon = Resources.Load<Sprite>("abilities/courage");
        equipped = true;
    }
}
