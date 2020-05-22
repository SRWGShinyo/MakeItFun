using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEvent : MonoBehaviour, IEvent
{
    string eventState = "INIT";
    RobotScheduler robot;

    public GameObject cyborg;
    public GameObject flag;
    public GameObject mirror;
    public GameObject playermirr;

    private void Start()
    {
        robot = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotScheduler>();
    }

    public void startEvent()
    {
        switch (eventState)
        {
            case "INIT":
                StartCoroutine(InitEvent());
                break;
            case "CLOSED":
                StartCoroutine(SwitchEyesOpen());
                break;
        }
    }

    private IEnumerator InitEvent()
    {
        while (RenderSettings.ambientIntensity > 0)
        {
            RenderSettings.ambientIntensity -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        playermirr.SetActive(false);
        eventState = "CLOSED";
        robot.playNextAction();
    }

    private IEnumerator SwitchEyesOpen()
    {
        flag.SetActive(true);
        mirror.SetActive(true);

        Destroy(cyborg.GetComponentInChildren<Camera>().gameObject);

        while (RenderSettings.ambientIntensity < 1.2f)
        {
            RenderSettings.ambientIntensity += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        eventState = "END";
        robot.playNextAction();
    }
}
