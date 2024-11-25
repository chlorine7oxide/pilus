using UnityEngine;

public class strongBone : Gene
{
    public strongBone()
    {
        name = "Stronger Bones";
        description = "A couple extra inches of calcium never hurt anybody. Increased defense";
        type = "normal";
        icon = Resources.Load<Sprite>("abilities/strongBone");
        equipped = false;
    }
}
