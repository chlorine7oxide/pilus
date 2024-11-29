using UnityEngine;

public class arm : Gene
{
    public arm(Sprite s, Sprite t, Sprite b, Sprite j)
    {
        name = "Arm";
        description = "It's your regular arm - probably should've worked out a little more before this. Grants a regular attack.";
        type = "arm";
        icon = s;
        equipped = true;
        jar = j;
        top = t;
        bottom = b;
    }
}
