using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitQuestion : MonoBehaviour
{
    public Material unTouched;
    public Material touched;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Stepped !");
            if (QuizzPillar.selectedAnswer == null)
                return;

            StartCoroutine(submit());
        }
    }

    private IEnumerator submit()
    {
        GetComponent<MeshRenderer>().material = touched;
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("Event").GetComponent<EventQuizz>().notifyAnswer(QuizzPillar.selectedAnswer.answer);
        QuizzPillar.selectedAnswer = null;
        GetComponent<MeshRenderer>().material = unTouched;
    }
}
