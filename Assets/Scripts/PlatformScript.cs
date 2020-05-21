using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float amplitude = 5f;
    public float speedSec = 3f;
    public bool goTop = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(move());
    }


    private IEnumerator move()
    {
        while (true)
        {
            if (goTop)
            {
                transform.DOMove(new Vector3(transform.position.x, transform.position.y + amplitude, transform.position.z), speedSec);
                yield return new WaitForSeconds(speedSec);
            }

            else
            {
                transform.DOMove(new Vector3(transform.position.x, transform.position.y - amplitude, transform.position.z), speedSec);
                yield return new WaitForSeconds(speedSec);
            }

            yield return new WaitForSeconds(1f);
            goTop = !goTop;
        }
    }
}
