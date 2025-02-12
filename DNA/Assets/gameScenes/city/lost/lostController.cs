using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lostController : MonoBehaviour
{
    public string direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (playerData.visitedDowntown)
            {
                SceneManager.LoadScene("downtown");
            }


            DontDestroyOnLoad(this.gameObject);

            int rand = Random.Range(1, 3);
            direction = direction + rand.ToString();

            
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());

            SceneManager.LoadScene(direction);
        }
    }

    public int lost = 0;
    public string SceneName = "citymain";

    public Sprite port, port2;

    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals(SceneName))
        {
            if (SceneManager.GetActiveScene().name.Equals("citymain"))
            {
                Destroy(this.gameObject);
            }
            else
            {
                lost++;
                SceneName = SceneManager.GetActiveScene().name;
                switch (lost)
                {
                    case 2:
                        {
                            StartCoroutine(dialogue1());
                            break;
                        }
                    case 5:
                        {
                            StartCoroutine(dialogue2());
                            break;
                        }
                }
            }

        }
    }

    public IEnumerator dialogue1()
    {
        generalText t = generalText.create("Are you sure you know where you're going?", port2, null);

        yield return new WaitUntil(() => t.done);

        t.destroy();
        t = generalText.create("Of course I do!", port, null);

        yield return new WaitUntil(() => t.done);

        t.destroy();
    }

    public IEnumerator dialogue2()
    {
        generalText t = generalText.create("Okay, I actually have no idea where we are.", port, null);

        yield return new WaitUntil(() => t.done);

        t.destroy();
        t = generalText.create("Right...", port2, null);

        yield return new WaitUntil(() => t.done);
        t.changeText("I know where to go so just follow my directions");
        yield return new WaitUntil(() => t.done);
        t.destroy();

        yield return new WaitForEndOfFrame();

        StartCoroutine(directions());
    }

    public IEnumerator directions()
    {
        string prev = SceneManager.GetActiveScene().name, target = null;

        for (int i = 0; i < 5; i++)
        {
            switch (SceneManager.GetActiveScene().name) {
                case "up1":
                    target = "down"; // SE
                    generalText t = generalText.create("Go South-East.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                case "up2":
                    target = "left"; // W
                    t = generalText.create("Go West.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                case "left1":
                    target = "down"; // S
                    t = generalText.create("Go South.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                case "left2":
                    target = "up"; // N
                    t = generalText.create("Go North.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                case "right1":
                    target = "right"; // NE
                    t = generalText.create("Go North-East.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                case "right2":
                    target = "up"; // N
                    t = generalText.create("Go North.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                case "down1":
                    target = "right"; // E
                    t = generalText.create("Go East.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
                case "down2":
                    target = "left"; // SW
                    t = generalText.create("Go South-West.", port2, null);
                    yield return new WaitUntil(() => t.done);
                    t.destroy();
                    break;
            }

            print(target);

            yield return new WaitUntil(() => !SceneManager.GetActiveScene().name.Equals(prev));

            prev = SceneManager.GetActiveScene().name;

            if (!prev.ToLower().Contains(target.ToLower()))
            {
                i--;
                generalText t = generalText.create("That was the wrong way.", port2, null);
                yield return new WaitUntil(() => t.done);
                t.changeText("It's alright, though.");
                yield return new WaitUntil(() => t.done);
                t.changeText("I'll take us a different way.");
                yield return new WaitUntil(() => t.done);
                t.destroy();
            }
            else if (i == 4)
            {
                SceneManager.LoadScene("downtown");
                playerData.visitedDowntown = true;
                print("not lost");
                Destroy(this.gameObject);
            }

        }
    }

}
