using UnityEngine;

public class mechanicalArm : Gene
{
    public mechanicalArm()
    {
        name = "Mechanical Arm";
        description = "Not much of a gene, but will help your combat abilities nonetheless. Grants a more powerful attack.";
        type = "arm";
        icon = Resources.Load<Sprite>("abilities/mechanicalArm");
        equipped = false;
    }
}
