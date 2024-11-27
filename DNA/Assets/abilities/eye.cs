using UnityEngine;

public class eye : Gene
{
    public eye(Sprite s)
    {
        name = "Eye";
        description = "It's your remaining eye. Lets you check an enemy's stats.";
        type = "eye";
        icon = s;
        equipped = true;
    }
}
