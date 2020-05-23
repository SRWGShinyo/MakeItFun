using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientBehaviour : MonoBehaviour
{
    public static AmbientBehaviour ambientActive = null;

    private void Awake()
    {
        if (ambientActive != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            ambientActive = this;
        }
    }
}
