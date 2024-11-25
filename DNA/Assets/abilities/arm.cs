using UnityEngine;

public class arm : Gene
{
    public arm()
    {
        name = "Arm";
        description = "It's your regular arm - probably should've worked out a little more before this. Grants a regular attack.";
        type = "arm";
        icon = Resources.Load<Sprite>("abilities/arm");
        equipped = true;
    }
}
