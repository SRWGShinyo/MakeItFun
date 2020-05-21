using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : MonoBehaviour
{

    HatEvent hats;

    private void Start()
    {
        hats = GameObject.FindGameObjectWithTag("Event").GetComponent<HatEvent>();
    }

    public IEnumerator moveTo(Vector3 newPoint, float speed)
    {
        transform.DOMove(newPoint, speed);
        yield return new WaitForSeconds(speed);
    }

    public void notifyCheck()
    {
        StartCoroutine(hats.unravel(this));
    }
}
