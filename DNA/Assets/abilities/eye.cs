using UnityEngine;

public class eye : Gene
{
    public eye(Sprite s, Sprite t, Sprite b, Sprite j)
    {
        name = "Eye";
        description = "It's your remaining eye. Lets you check an enemy's stats.";
        type = "eye";
        icon = s;
        equipped = true;
        jar = j;
        top = t;
        bottom = b;
    }
}
