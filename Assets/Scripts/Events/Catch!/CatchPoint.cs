using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPoint : MonoBehaviour
{
    static CatchPoint lastActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (lastActivated == this)
                return;

            lastActivated = this;
            GameObject.FindGameObjectWithTag("Flag").GetComponent<CatchMasterFlag>().moveToNext();
        }
    }
}
