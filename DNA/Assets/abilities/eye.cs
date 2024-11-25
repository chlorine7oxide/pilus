using UnityEngine;

public class eye : Gene
{
    public eye()
    {
        name = "Eye";
        description = "It's your remaining eye. Lets you check an enemy's stats.";
        type = "eye";
        icon = Resources.Load<Sprite>("abilities/eye");
        equipped = true;
    }
}
