using UnityEngine;

public class strongBone : Gene
{
    public strongBone(Sprite s, Sprite t, Sprite b, Sprite j)
    {
        name = "Stronger Bones";
        description = "A couple extra inches of calcium never hurt anybody. Increased defense";
        type = "normal";
        icon = s;
        equipped = false;
        jar = j;
        top = t;
        bottom = b;
    }
}
