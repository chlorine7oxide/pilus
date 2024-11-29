using UnityEngine;

public class mechanicalArm : Gene
{
    public mechanicalArm(Sprite s, Sprite t, Sprite b, Sprite j)
    {
        name = "Mechanical Arm";
        description = "Not much of a gene, but will help your combat abilities nonetheless. Grants a more powerful attack.";
        type = "arm";
        icon = s;
        equipped = false;
        jar = j;
        top = t;
        bottom = b;
    }
}
