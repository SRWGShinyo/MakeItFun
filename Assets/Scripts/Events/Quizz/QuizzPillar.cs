using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzPillar : MonoBehaviour
{
    public static QuizzPillar selectedAnswer = null;
    public int answer;

    public Material unSelected;
    public Material selected;

    public void selectAnswer()
    {
        if (selectedAnswer == this || !EventQuizz.quizzHasStarted)
            return;

        if (selectedAnswer != null)
            selectedAnswer.gameObject.GetComponent<QuizzPillar>().unSelectAnswer();

        selectedAnswer = this;
        GetComponent<MeshRenderer>().material = selected;
    }

    public void unSelectAnswer()
    {
        GetComponent<MeshRenderer>().material = unSelected;
        selectedAnswer = null;
    }
}
