using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EventQuizz : MonoBehaviour, IEvent
{
    public static bool quizzHasStarted = false;

    public GameObject answerA;
    public GameObject answerB;
    public GameObject answerC;
    public GameObject answerD;

    public TextMeshProUGUI question;
    public GameObject quizzPanel;

    public GameObject groundFloor;

    quizzQuestion activeQuestion;

    public List<GameObject> pillars;
    public GameObject flagPole;

    [System.Serializable]
    public class quizzQuestion
    {
        public string question;
        public int answerIndex;
        public string robotComment;
        public List<string> answers;
    }

    public List<quizzQuestion> listQuestions = new List<quizzQuestion>();

    Queue<quizzQuestion> questions = new Queue<quizzQuestion>();
    RobotScheduler robot;
    AudioSource source;
    int expectedAnswer = -1;

    public AudioClip dingding;
    public AudioClip dzz;
    public AudioClip poof;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        robot = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotScheduler>();
        foreach (quizzQuestion qQ in listQuestions)
        {
            questions.Enqueue(qQ);
        }
    }

    public void startEvent()
    {
        quizzHasStarted = true;
        StopAllCoroutines();

        quizzQuestion qQ = questions.Dequeue();
        activeQuestion = qQ;
        question.text = qQ.question;
        GameObject[] images = { answerA, answerB, answerC, answerD };
        for (int i = 0; i < qQ.answers.Count; i++)
        {
            images[i].GetComponentInChildren<TextMeshProUGUI>().text = qQ.answers[i];
            images[i].SetActive(true);
        }

        expectedAnswer = qQ.answerIndex;
        StartCoroutine(bringBackTheLight());
    }

    public void notifyAnswer(int answer)
    {
        quizzHasStarted = false;
        if (answer == -1)
            return;

        if (answer == expectedAnswer)
        {
            source.Stop();
            source.clip = dingding;
            source.Play();
            expectedAnswer = -1;
            if (questions.Count == 0)
            {
                youWin();
                return;
            }

            StartCoroutine(setUpNewQuestion());
        }

        else
        {
            source.Stop();
            source.clip = dzz;
            source.Play();
            quizzPanel.transform.DOScale(0, 0.2f);
            
            RobotScheduler.stringContainer sC = new RobotScheduler.stringContainer() { expressions = new List<RobotScheduler.stringContainer.faceExpression>() { RobotScheduler.stringContainer.faceExpression.FRUSTR },
                                                                                       strings = new List<string>() { "How...too bad..."}
            };

            robot.stringContainersQueue.Enqueue(sC);
            robot.queueAc.Enqueue(RobotScheduler.EventAction.TALK);
            robot.playNextAction();
            groundFloor.SetActive(false);
        }
    }

    private IEnumerator setUpNewQuestion()
    {
        foreach (GameObject go in pillars)
            go.GetComponent<QuizzPillar>().unSelectAnswer();

        quizzPanel.transform.DOScale(0, 1);
        yield return new WaitForSeconds(1f);
        GameObject[] images = { answerA, answerB, answerC, answerD };
        foreach (GameObject im in images)
            im.SetActive(false);

        while (RenderSettings.ambientIntensity > 0.1f)
        {
            RenderSettings.ambientIntensity -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        RobotScheduler.stringContainer sC = new RobotScheduler.stringContainer()
        {
            expressions = new List<RobotScheduler.stringContainer.faceExpression>() { RobotScheduler.stringContainer.faceExpression.NORMAL, RobotScheduler.stringContainer.faceExpression.HAPPY  },
            strings = new List<string>() { activeQuestion.robotComment, "Let's move on !" }
        };

        robot.stringContainersQueue.Enqueue(sC);
        robot.queueAc.Enqueue(RobotScheduler.EventAction.TALK);
        robot.queueAc.Enqueue(RobotScheduler.EventAction.EVENT);
        robot.playNextAction();
    }

    private IEnumerator bringBackTheLight()
    {
        quizzPanel.transform.DOScale(1, 1);
        yield return new WaitForSeconds(3f);

        while (RenderSettings.ambientIntensity < 1.2f)
        {
            RenderSettings.ambientIntensity += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void youWin()
    {
        quizzPanel.transform.DOScale(0, 1f);

        RobotScheduler.stringContainer sC = new RobotScheduler.stringContainer()
        {
            expressions = new List<RobotScheduler.stringContainer.faceExpression>() { RobotScheduler.stringContainer.faceExpression.HAPPY, RobotScheduler.stringContainer.faceExpression.HAPPY },
            strings = new List<string>() { "Ehehehe ! You're the best !", "Just take the flag !" }
        };
        robot.stringContainersQueue.Enqueue(sC);
        robot.queueAc.Enqueue(RobotScheduler.EventAction.TALK);
        robot.playNextAction();

        foreach (GameObject go in pillars)
            go.SetActive(false);

        source.Stop();
        source.clip = poof;
        source.Play();
        flagPole.SetActive(true);
    }
}
