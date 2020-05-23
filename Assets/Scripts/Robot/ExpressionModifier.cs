using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionModifier : MonoBehaviour
{
    public GameObject happyL;
    public GameObject happyR;

    public GameObject normL;
    public GameObject normR;

    public GameObject FrustrL;
    public GameObject FrustrR;

    public GameObject TrustL;
    public GameObject TrustR;


    public void changeExpression(RobotScheduler.stringContainer.faceExpression expression)
    {
        switch(expression)
        {
            case RobotScheduler.stringContainer.faceExpression.NORMAL:
                deactivateAll();
                activateOnly(normL, normR);
                break;
            case RobotScheduler.stringContainer.faceExpression.HAPPY:
                deactivateAll();
                activateOnly(happyL, happyR);
                break;
            case RobotScheduler.stringContainer.faceExpression.FRUSTR:
                deactivateAll();
                activateOnly(FrustrL, FrustrR);
                break;
            case RobotScheduler.stringContainer.faceExpression.TRUST:
                deactivateAll();
                activateOnly(TrustL, TrustR);
                break;
            default:
                deactivateAll();
                activateOnly(normL, normR);
                break;
        }
    }

    private void deactivateAll()
    {
        happyL.SetActive(false);
        happyR.SetActive(false);
        TrustL.SetActive(false);
        TrustR.SetActive(false);
        normL.SetActive(false);
        normR.SetActive(false);
        FrustrL.SetActive(false);
        FrustrR.SetActive(false);
    }

    private void activateOnly(GameObject eyeL, GameObject eyeR)
    {
        eyeL.SetActive(true);
        eyeR.SetActive(true);
    }
}
