using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofDrapeauOnRandom : MonoBehaviour
{
    public float beBackAfter = 4f;
    bool isAway = false;
    float randomAfterGo;
    public GameObject whereToSpawn;
    public GameObject poof;

    AudioSource away;
    private void Start()
    {
        randomAfterGo = Random.Range(4f, 9f);
        away = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAway)
        {
            if (randomAfterGo < 0)
            {
                disappear();
                isAway = true;
                randomAfterGo = Random.Range(4f, 9f);
                return;
            }

            randomAfterGo -= Time.deltaTime;
        }

        else
        {
            if (beBackAfter < 0)
            {
                appear();
                isAway = false;
                beBackAfter = Random.Range(4f, 9f);
            }

            beBackAfter -= Time.deltaTime;
        }
    }

    private void disappear()
    {
        foreach (Transform tr in transform)
        {
            if (tr.gameObject.GetComponent<MeshRenderer>())
                tr.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        Instantiate(poof, whereToSpawn.transform.position, whereToSpawn.transform.rotation);
        away.Stop();
        away.Play();
    }

    private void appear()
    {
        foreach (Transform tr in transform)
        {
            if (tr.gameObject.GetComponent<MeshRenderer>())
                tr.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        Instantiate(poof, whereToSpawn.transform.position, whereToSpawn.transform.rotation);
        away.Stop();
        away.Play();
    }
}
