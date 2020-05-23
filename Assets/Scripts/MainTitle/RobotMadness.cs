using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMadness : MonoBehaviour
{
    Animator anim;
    float reopen;
    float ball;

    public GameObject start;
    public GameObject goTo;
    bool isBall = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ball = Random.Range(4, 9);
        reopen = Random.Range(4, 9);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBall)
        {
            if (ball < 0)
            {
                StartCoroutine(move());
            }
            ball -= Time.deltaTime;
        }

        else
        {
            if (reopen < 0)
            {
                anim.SetBool("Roll_Anim", false);
                isBall = false;
                transform.DOMove(start.transform.position, 1f);
                reopen = Random.Range(4, 9);
            }

            reopen -= Time.deltaTime;
        }
    }

    private IEnumerator move()
    {
        anim.SetBool("Roll_Anim", true);
        yield return new WaitForSeconds(2.4f);
        isBall = true;
        ball = Random.Range(4, 9);
        transform.DOMove(goTo.transform.position, 1f);
    }
}
