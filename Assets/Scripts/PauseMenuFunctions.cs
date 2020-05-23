using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuFunctions : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !panel.activeSelf)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FPSCameraLook>().enabled = false;

            if (GameObject.FindGameObjectWithTag("Flag") && GameObject.FindGameObjectWithTag("Flag").GetComponentInChildren<FPSCameraLook>() != null)
                GameObject.FindGameObjectWithTag("Flag").GetComponentInChildren<FPSCameraLook>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            panel.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && panel.activeSelf)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FPSCameraLook>().enabled = true;

            if (GameObject.FindGameObjectWithTag("Flag") && GameObject.FindGameObjectWithTag("Flag").GetComponentInChildren<FPSCameraLook>() != null)
                GameObject.FindGameObjectWithTag("Flag").GetComponentInChildren<FPSCameraLook>().enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            panel.SetActive(false);
        }
    }
}
