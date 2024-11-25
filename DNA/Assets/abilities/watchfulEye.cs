using UnityEngine;

public class watchfulEye : Gene
{
    public watchfulEye()
    {
        name = "Watchful Eye";
        description = "Don't think about the fact that this used to belong to someone. Improves your checking ability";
        type = "eye";
        icon = Resources.Load<Sprite>("abilities/watchfulEye");
        equipped = false;
    }
}
