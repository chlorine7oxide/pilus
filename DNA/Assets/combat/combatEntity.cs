using UnityEngine;

public abstract class combatEntity
{
    public int hp, maxhp, def;
    public bool active = true;

    public GameObject entity;
    public GameObject hpBar, hpBar2;

    public static GameObject barPrefab;

    public combatEntity(int hp, int def)
    {
        this.hp = hp;
        this.maxhp = hp;
        this.def = def;
        hpBar = GameObject.Instantiate(barPrefab);
        hpBar2 = GameObject.Instantiate(barPrefab);
        hpBar.transform.Translate(new Vector3(0, 1, 0));
        hpBar2.transform.Translate(new Vector3(0, 1, 0));
        hpBar.transform.localScale = new Vector3(1, 0.2f, 1);
        hpBar2.transform.localScale = new Vector3(1, 0.2f, 1);
        hpBar.GetComponent<SpriteRenderer>().color = Color.red;
        hpBar2.GetComponent<SpriteRenderer>().color = Color.green;
        hpBar.GetComponent<SpriteRenderer>().sortingOrder = 1;
        hpBar2.GetComponent<SpriteRenderer>().sortingOrder = 2;
        hpBar2.AddComponent<hpBar>();
        hpBar2.GetComponent<hpBar>().entity = this;
    }

    public void takeDamage(int damage)
    {
        hp -= Mathf.Max((damage - def), 0);
        if (hp <= 0)
        {
            hp = 0;
            active = false;
        }
    }
}
