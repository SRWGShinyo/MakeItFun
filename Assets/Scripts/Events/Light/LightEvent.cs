using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : MonoBehaviour, IEvent
{
    public int triesRef = 5;
    int tries;
    public int numberTing = 3;
    public List<GameObject> pillars;
    public List<GameObject> startPos;
    public List<GameObject> endPos;

    public GameObject flag;
    public GameObject player;
    public GameObject playerPos;

    public AudioClip poof;
    public AudioClip ding;
    public AudioClip bzz;

    Queue<string> orderExpected = new Queue<string>();

    public FPSCameraLook rotator;
    public FPSPlayerMovement controller;

    RobotScheduler robot;
    AudioSource source;

    public void startEvent()
    {
        foreach (GameObject go in pillars)
            go.SetActive(true);

        robot = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotScheduler>();
        source = GetComponent<AudioSource>();
        tries = triesRef;
        StartCoroutine(setUpScene());
    }

    public IEnumerator setUpScene()
    {
        while (RenderSettings.ambientIntensity > 0.1f)
        {
            RenderSettings.ambientIntensity -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        movePillars();

        rotator.enabled = false;
        controller.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        player.transform.position = playerPos.transform.position;
        player.transform.rotation = new Quaternion(0, 0, 0, 0);

        StartCoroutine(composeNewCode());
    }

    public IEnumerator composeNewCode()
    {
        List<int> indexes = new List<int>();

        for (int i = 0; i < numberTing; i++)
        {
            indexes.Add(Random.Range(0, pillars.Count));
        }

        foreach (int i in indexes)
        {
            orderExpected.Enqueue(pillars[i].name);
            StartCoroutine(pillars[i].GetComponent<LightPillar>().emitir());
            yield return new WaitForSeconds(1f);
        }

        tries -= 1;
        numberTing += 1;
        StartCoroutine(AllowPlaying());
    }

    public void notify(string name)
    {
        string expected = orderExpected.Dequeue();

        if (name != expected) // The player made a mistake
        {
            orderExpected.Clear();
            numberTing -= triesRef - tries;
            tries = triesRef;

            source.clip = bzz;
            source.Play();

            robot.stringContainersQueue.Enqueue(new RobotScheduler.stringContainer() { strings = new List<string>() {"Oooh...too bad...", "Let's try again !" } });
            robot.queueAc.Enqueue(RobotScheduler.EventAction.TALK);
            robot.queueAc.Enqueue(RobotScheduler.EventAction.EVENT);
            robot.playNextAction();
        }

        else
        {
            if (orderExpected.Count == 0)
            {
                if (tries <= 0) // Game is won;
                {
                    foreach(GameObject go in pillars)
                    {
                        go.GetComponent<LightPillar>().IEmit();
                    }
                    flag.SetActive(true);
                    robot.stringContainersQueue.Enqueue(new RobotScheduler.stringContainer() { strings = new List<string>() { "You did it ! You're the best :D" } });
                    robot.queueAc.Enqueue(RobotScheduler.EventAction.TALK);
                    source.clip = poof;
                    source.Play();
                    robot.playNextAction();
                }

                else
                {
                    source.clip = ding;
                    source.Play();
                    StartCoroutine(setUpScene());
                }
            }
        }
    }

    private IEnumerator AllowPlaying()
    {
        List<int> index = new List<int>(){ 0, 1, 2, 3 };
        List<int> positions = new List<int>() { 0, 1, 2, 3 };

        while (index.Count != 0)
        {
            int rdm = Random.Range(0, index.Count);
            int rdm1 = Random.Range(0, positions.Count);
            int mychosenIndex = index[rdm];
            pillars[mychosenIndex].transform.position = endPos[positions[rdm1]].transform.position;
            index.RemoveAt(rdm);
            positions.RemoveAt(rdm1);
        }

        while (RenderSettings.ambientIntensity < 1.2f)
        {
            RenderSettings.ambientIntensity += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        rotator.enabled = true;
        controller.enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
    }

    private void movePillars()
    {
        for (int i = 0; i < pillars.Count; i++)
        {
            pillars[i].transform.position = startPos[i].transform.position;
        }
    }
}
