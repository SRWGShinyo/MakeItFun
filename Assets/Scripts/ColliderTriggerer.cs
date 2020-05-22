using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTriggerer : MonoBehaviour
{

    public int toLoadOnDeath = 5;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "MagicHat")
        {
            hit.gameObject.GetComponent<HatScript>().notifyCheck();
            return;
        }

        if (hit.gameObject.tag == "Death")
        {
            SceneManager.LoadScene(toLoadOnDeath);
            return;
        }
    }
}
