using UnityEngine;

public class strongBone : Gene
{
    public strongBone(Sprite s)
    {
        name = "Stronger Bones";
        description = "A couple extra inches of calcium never hurt anybody. Increased defense";
        type = "normal";
        icon = s;
        equipped = false;
    }
}
