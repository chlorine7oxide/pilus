using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour
{
    public GameObject guard;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public GameObject itemMenu, cameraObj;
    public GameObject[] itemButtons;

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public Sprite portrait, sel;

    public bool active = false;

    public GameObject UiTextPrefab;

    protected void Start()
    {
        dynamicSelectorText.textPrefabUI = UiTextPrefab;

        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (playerData.birthdayGuess == 0)
            {
                interact();
            }
            
            else if (playerData.birthdayGuess > 1)
            {
                guard.GetComponent<textInteraction>().enabled = true;
                Destroy(this.gameObject);
            }
        }
    }

    public bool interactable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerData.birthdayGuess == 2)
        {
            guard.GetComponent<textInteraction>().enabled = true;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("player"))
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            interactable = false;
        }
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.Z) && playerData.birthdayGuess == 1 && !active)
        {
            StartCoroutine(birthdayGuess());
            //interactable = false;
        }
        if (!active)
        {
            face.transform.position = new Vector3(-100, -100, 0);
            months.transform.position = new Vector3(-100, -100, 0);
            days.transform.position = new Vector3(-100, -100, 0);
        }
    }

    public void interact()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];
        GameObject p3 = speakers[2];

        msgs.Enqueue(new msg(testFace, () => "I'm afraid I can't let you pass.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Why not?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "It's my job, I have to make sure nobody gets in here.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "But how come, what's behind here?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "It's the vault, all the treasures of the kingdom are behind this door.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "The kingdom?", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "I didn't know this city was part of any kingdom?", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "It's the kingdom of this city!", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "You would be suprised how much treasure there is in here.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "There can't be very much can there?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Can you show me?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "I'm afraid not, I can't let anyone in.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "You're making me think that it's just empty behind that door.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "I assure you it isn't, you will have to trust me.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Why would I trust you?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "You've given me every reason to believe there isn't anything behind this door.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "If there isn't anything behind that door, why would I be guarding it?", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "You're trying to hide the real treasure from us!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "If I'm hiding the real treasure why don't you go and find it.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Well...", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "You've got me there.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Enough of this!", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Let us through or we'll have to force you to leave.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Force me, how would you go about that?", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "...", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "We'll answer your riddles!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "You seem like the type of guard to give us riddles.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Ha!", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Fine then, if FAILING a riddle will make you leave, I'll give you something you can't POSSIBLY know.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "When", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Is", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "...my birthday?", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "If you can tell me that, I'll let you pass.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "That seems a little unfair...", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "very unfair, can you at least tell us the month?", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "No!", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "I'm not telling you the month, being unfair is the whole point.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Just come to me any time you're ready to get this wrong.", "e", p3));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
        speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -3.1f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

        StartCoroutine(msgListener());
    }

    public IEnumerator msgListener()
    {
        yield return new WaitUntil(() => !msgController.inText);

        playerData.birthdayGuess = 1;
    }

    public GameObject months, days, face, cam;

    public Vector3 monthspos, dayspos, facepos;

    public Sprite maskNormal, maskExcited;

    public IEnumerator birthdayGuess() // actual birthday is Oct 20
    {
        active = true;

        months.transform.position = monthspos + cam.transform.position + new Vector3(0, 0, 10);
        days.transform.position = dayspos + cam.transform.position + new Vector3(0, 0, 10);
        face.transform.position = facepos + cam.transform.position + new Vector3(0, 0, 10);

        string[] monthString = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        dynamicSelectorText monthsSelector;

        monthsSelector = dynamicSelectorText.create(new Vector3[] { months.transform.position }, monthString, sel);

        StartCoroutine(faceController(face, monthsSelector, 9));

        yield return new WaitUntil(() => monthsSelector.done);

        int month = monthsSelector.result;
        monthsSelector.destroy();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        monthsSelector = dynamicSelectorText.create(new Vector3[] { days.transform.position }, new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" }, sel);
        StartCoroutine(faceController(face, monthsSelector, 19));
        yield return new WaitUntil(() => monthsSelector.done);

        int day = monthsSelector.result;
        monthsSelector.destroy();


        if (month == 9 && day  == 19) // correct
        {
            Destroy(face);
            Destroy(months);
            Destroy(days);
            active = false;
            StartCoroutine(pass());
        }
        else if (!verifyDay(month, day)) // invalid date
        {
            generalText t = generalText.create(monthString[month] + " " +(day + 1).ToString() + "?", maskNormal, null);

            yield return new WaitUntil(() => t.done);

            t.changeText("That's not even a real date...");

            yield return new WaitUntil(() => t.done);

            t.changeText("Look, I feel bad so I'll give you another try.");

            yield return new WaitUntil(() => t.done);

            t.destroy();
        }
        else // valid date but wrong
        {
            generalText t = generalText.create(monthString[month] + " " + (day + 1).ToString() + "?", maskNormal, null);

            yield return new WaitUntil(() => t.done);

            t.changeText("Nope.");

            yield return new WaitUntil(() => t.done);

            t.changeText("That was such a terrible guess I'll let you try again.");

            yield return new WaitUntil(() => t.done);

            t.destroy();
        }


        face.transform.position = new Vector3(-100, -100, 0);
        months.transform.position = new Vector3(-100, -100, 0);
        days.transform.position = new Vector3(-100, -100, 0);
        active = false;
    }

    public IEnumerator faceController(GameObject face, dynamicSelectorText selector, int trigger)
    {
        yield return new WaitUntil(() => selector is not null);
        float t = 0;

        Vector3 ogPos = face.transform.position;

        while (!selector.done)
        {
            if (selector.pos == trigger) // x-displacement is 0.1sin(2pi10t)
            {
                face.GetComponent<SpriteRenderer>().sprite = maskExcited;
                face.transform.position = ogPos + new Vector3(0.1f * Mathf.Sin(2 * Mathf.PI * 10 * t), 0, 0);
            }
            else
            {
                face.GetComponent<SpriteRenderer>().sprite = maskNormal;
            }

            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        face.transform.position = ogPos;
        
    }

    public IEnumerator pass()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];
        GameObject p3 = speakers[2];

        msgs.Enqueue(new msg(testFace, () => "How did you know.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Just a hunch.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "It was pretty obvious really.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Alright, alright, you did do a good job, goodbye now.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "What about letting us into the vault!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "I was hoping you'd forget about that...", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "But a deal is a deal.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "Here you go.", "e", p3));
        msgs.Enqueue(new msg(testFace, () => "The door is unlocked.", "e", p3));
        

        playerData.birthdayGuess = 2;

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-7f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-5f, -2.8f, 0);
        speakers[2].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -3.1f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();

        yield return new WaitUntil(() => !msgController.inText);
        guard.GetComponent<textInteraction>().enabled = true;
        Destroy(this.gameObject);
    }

    public bool verifyDay(int month, int day)
    {
        switch (month)
        {
            case 1:
                return day < 28;
            case 3:
                return day < 30;
            case 5:
                return day < 30;
            case 8:
                return day < 30;
            case 10:
                return day < 30;
        }
        return true;
        
    }
}
