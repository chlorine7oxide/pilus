using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class realcombatController : MonoBehaviour
{
    public int playerHealth = 1, playerMax;
    public int playerDef;

    public GameObject controller;
    public GameObject barPrefab;
    public GameObject barFront, barBack;

    private void Start()
    {
        StartCoroutine(started());

        controller = GameObject.FindGameObjectWithTag("boss1Controller");

        barFront = Instantiate(barPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);
        barBack = Instantiate(barPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);
        barFront.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        barFront.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        barFront.transform.position = this.gameObject.transform.position;
        barBack.transform.position = this.gameObject.transform.position;
    }

    public void takeDamage(int damage)
    {
        controller.GetComponent<boss1Controller>().mcHp -= damage - playerDef;
        if (controller.GetComponent<boss1Controller>().mcHp <= 0)
        {
            // Game over
            Debug.Log("dead player");
        }
        barFront.transform.localScale = new Vector3((float)controller.GetComponent<boss1Controller>().mcHp / playerMax, 1, 1);
    }

    public IEnumerator started()
    {
        yield return new WaitForEndOfFrame();
        playerData.setStats();
        print(playerData.hp);
        playerHealth = playerData.hp;
        playerMax = playerData.hp;
        playerDef = playerData.def;
        controller.GetComponent<boss1Controller>().mcHp = playerData.hp;
        controller.GetComponent<boss1Controller>().companionHp = 100;
    }
}
