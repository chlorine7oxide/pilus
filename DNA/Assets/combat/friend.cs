using UnityEngine;

public class friend : combatEntity
{
    public GameObject friendObj;

    public friend(int hp, int def) : base(hp, def)
    {
        friendObj = new GameObject("mc");
        friendObj.tag = "player";
    }
}
