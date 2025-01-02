using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rat : MonoBehaviour
{
    public bool entered = false;
    public GameObject ratTailPrefab;
    public GameObject ratPrefab;
    public Sprite portrait;

    public GameObject alertPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (!entered)
            {
                entered = true;
                combatData.playerPos = GameObject.FindGameObjectWithTag("player").transform.position;
                combatData.scene = SceneManager.GetActiveScene().name;
                combatStarter combatStarter = new combatStarter(2, 2, new string[] { "rat", "ratTail" }, this.gameObject);
            }
        }
    }

    private void Start()
    {
        if (playerData.ratBeaten)
        {
            Destroy(this.gameObject);
        }
        ratEnemy.ratPrefab = ratPrefab;
        ratTailEnemy.ratTailPrefab = ratTailPrefab;
        ratEnemy.portrait = portrait;
        StartCoroutine(run());
    }

    public IEnumerator run()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject alert = Instantiate(alertPrefab, this.gameObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        //alert.GetComponent<alert>().target = this.gameObject;
        GameObject g = GameObject.FindGameObjectWithTag("player");
        while (!entered)
        {
            this.gameObject.transform.Translate((g.transform.position - this.gameObject.transform.position).normalized * 4 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
