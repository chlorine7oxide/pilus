using UnityEngine;

public class watchfulEye : Gene
{
    public watchfulEye(Sprite s)
    {
        name = "Watchful Eye";
        description = "Don't think about the fact that this used to belong to someone. Improves your checking ability";
        type = "eye";
        icon = s;
        equipped = false;
    }
}
