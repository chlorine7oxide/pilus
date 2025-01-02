using System.Collections;
using UnityEngine;

public class ratEnemy : combatEntity
{
    public static GameObject ratPrefab;
    public bool msgSeen = false;
    public bool enrageSeen = false;

    public static Sprite portrait;

    public ratEnemy(int hp, int def) : base(hp, def)
    {
        entity = GameObject.Instantiate(ratPrefab);
        entity.tag = "enemy";
    }

    public void scratch(combatEntity target)
    {
        target.takeDamage(25);
    }

    public bool dodging = false;

    public void dodge()
    {
        dodging = true;
        entity.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public override void takeDamage(int damage)
    {
        if (!dodging)
        {
            if (GameObject.FindGameObjectWithTag("RatEnemy"))
            {
                base.takeDamage(damage / 3);
                if (!msgSeen)
                {
                    msgSeen = true;
                    generalText.create("It's blocking with it's tail!", portrait, null, true);
                }
            }
            else
            {
                base.takeDamage(damage);
            }
            
        }
        else
        {
            dodging = false;
            entity.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
