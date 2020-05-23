using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatEvent : MonoBehaviour, IEvent 
{
    public List<HatScript> hats = new List<HatScript>();
    public List<GameObject> positions = new List<GameObject>();

    public Light spot1;
    public Light spot2;
    public Light spot3;

    public GameObject flagPole;
    public HatScript goodHat;

    public int shuffleTime;

    public GameObject bottomPit;

    public void startEvent()
    {
        goodHat = hats[0];
        StartCoroutine(letsMagic());
    }

    private IEnumerator letsMagic()
    {
        spot1.enabled = true;
        yield return new WaitForSeconds(0.2f);
        spot2.enabled = true;
        yield return new WaitForSeconds(0.2f);
        spot3.enabled = true;
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(hats[0].moveTo(positions[0].transform.position, 1f));
        StartCoroutine(hats[1].moveTo(positions[1].transform.position, 1f));
        StartCoroutine(hats[2].moveTo(positions[2].transform.position, 1f));

        yield return new WaitForSeconds(1f);

        flagPole.SetActive(false);
        StartCoroutine(shuffle());
    }
    
    private IEnumerator shuffle()
    {
        for (int i = 0; i < shuffleTime; i++)
        {
            int firstHat = Random.Range(0, 3);
            int secondHat = Random.Range(0, 3);
            while (secondHat == firstHat)
            {
                secondHat = Random.Range(0, 3);
            }

            StartCoroutine(hats[firstHat].moveTo(positions[secondHat].transform.position, 1f - 0.07f * i));
            StartCoroutine(hats[secondHat].moveTo(positions[firstHat].transform.position, 1f - 0.07f * i));

            HatScript hat = hats[firstHat];
            hats[firstHat] = hats[secondHat];
            hats[secondHat] = hat;

            yield return new WaitForSeconds(1 - 0.07f * i);
        }

        flagPole.transform.position = goodHat.transform.position;
        flagPole.SetActive(true);
        GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotScheduler>().playNextAction();
    }

    public IEnumerator unravel(HatScript chosenHat)
    {
        StartCoroutine(hats[0].moveTo(new Vector3(hats[0].transform.position.x, hats[0].transform.position.y + 50, hats[0].transform.position.z), 1f));
        StartCoroutine(hats[1].moveTo(new Vector3(hats[1].transform.position.x, hats[1].transform.position.y + 50, hats[1].transform.position.z), 1f));
        StartCoroutine(hats[2].moveTo(new Vector3(hats[2].transform.position.x, hats[2].transform.position.y + 50, hats[2].transform.position.z), 1f));

        yield return new WaitForSeconds(1f);

        if (chosenHat != goodHat)
        {
            bottomPit.SetActive(false);
            RobotScheduler robot = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotScheduler>();
            robot.stringContainersQueue.Enqueue(new RobotScheduler.stringContainer() { strings = new List<string>() { "Ohhh...too bad !" }, expressions = new List<RobotScheduler.stringContainer.faceExpression> { RobotScheduler.stringContainer.faceExpression.FRUSTR} });
            robot.queueAc.Enqueue(RobotScheduler.EventAction.TALK);
            robot.playNextAction();
        }

        else
        {
            RobotScheduler robot = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotScheduler>();
            robot.stringContainersQueue.Enqueue(new RobotScheduler.stringContainer() { strings = new List<string>() { "Well done !" }, expressions = new List<RobotScheduler.stringContainer.faceExpression> { RobotScheduler.stringContainer.faceExpression.HAPPY } });
            robot.queueAc.Enqueue(RobotScheduler.EventAction.TALK);
            robot.playNextAction();
            RenderSettings.ambientIntensity = 1.2f;
        }
    }
}
