using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class RobotScheduler : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    AudioSource voice;

    [System.Serializable]
    public enum EventAction
    {
        MOVE,
        TALK,
        CONTROLLER,
        EVENT,
        ROTATION
    }

    [System.Serializable]
    public class stringContainer
    {
        public enum faceExpression
        {
            NORMAL,
            HAPPY,
            TRUST,
            FRUSTR
        }

        public List<string> strings;
        public List<faceExpression> expressions;
    }

    public List<EventAction> actions = new List<EventAction>();
    public List<GameObject> pivots = new List<GameObject>();
    public List<stringContainer> talks = new List<stringContainer>();

    public GameObject textPanel;
    public TextMeshProUGUI textMesh;
    public FPSPlayerMovement controller;
    public FPSCameraLook rotator;
    public GameObject player;

    public Queue<EventAction> queueAc = new Queue<EventAction>();
    public Queue<GameObject> queueGO = new Queue<GameObject>();
    public Queue<stringContainer> stringContainersQueue = new Queue<stringContainer>();

    Animator anim;
    ExpressionModifier express;

    void Start()
    {
        express = GetComponent<ExpressionModifier>();
        voice = GetComponent<AudioSource>();
        DOTween.Init();

        foreach (EventAction ea in actions)
            queueAc.Enqueue(ea);

        foreach (GameObject go in pivots)
            queueGO.Enqueue(go);

        foreach (stringContainer sc in talks)
            stringContainersQueue.Enqueue(sc);

        anim = GetComponent<Animator>();
        playNextAction();
    }

    private void Update()
    {
        if (player)
            transform.LookAt(player.transform);
    }

    public void playNextAction()
    {
        if (queueAc.Count == 0)
            return;

        EventAction ac = queueAc.Dequeue();

        switch(ac)
        {
            case EventAction.MOVE:
                StartCoroutine(moveTo(queueGO.Dequeue().transform.position));
                break;
            case EventAction.TALK:
                StartCoroutine(talk(stringContainersQueue.Dequeue()));
                break;
            case EventAction.CONTROLLER:
                controller.enabled = !controller.isActiveAndEnabled;
                playNextAction();
                break;
            case EventAction.EVENT:
                GameObject.FindGameObjectWithTag("Event").GetComponent<IEvent>().startEvent();
                break;
            case EventAction.ROTATION:
                rotator.enabled = !rotator.enabled;
                playNextAction();
                break;
        }
    }

    private IEnumerator moveTo(Vector3 position)
    {
        anim.SetBool("Roll_Anim", true);
        yield return new WaitForSeconds(2.4f);
        transform.DOMove(position, 2f);
        yield return new WaitForSeconds(1.8f);
        anim.SetBool("Roll_Anim", false);
        playNextAction();
    }

    private IEnumerator talk(stringContainer sc)
    {
        int counter = 0;
        textPanel.transform.DOScale(1, 1f);
        foreach (string st in sc.strings)
        {
            express.changeExpression(sc.expressions[counter]);
            textMesh.text = "";
            foreach (char c in st)
            {
                textMesh.text += c;
                voice.Stop();
                AudioClip clip = clips[Random.Range(0, clips.Count)];
                voice.clip = clip;
                voice.Play();
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(3f);
            counter++;
        }
        textPanel.transform.DOScale(0, 1f);
        playNextAction();
    }
}
