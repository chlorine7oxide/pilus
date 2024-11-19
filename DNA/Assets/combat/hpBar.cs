using UnityEngine;

public class hpBar : MonoBehaviour
{
    public combatEntity entity;

    void Update()
    {
        this.transform.localScale = new Vector3((float)entity.hp / entity.maxhp, 0.2f, 1);
    }
}
