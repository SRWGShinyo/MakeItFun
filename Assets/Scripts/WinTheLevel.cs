using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTheLevel : MonoBehaviour
{
    public FPSPlayerMovement movement;
    public FPSCameraLook look;

    public GameObject obsvPoint;
    public Camera cam;

    public GameObject winPanel;
    public string nextLevel;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Flag")
        {
            movement.enabled = false;
            look.enabled = false;

            GetComponent<Animator>()?.SetBool("isGround", true);
            GetComponent<Animator>()?.SetBool("isRunning", false);

            cam.transform.DOMove(obsvPoint.transform.position, 2f);
            cam.transform.DORotate(obsvPoint.transform.rotation.eulerAngles, 2f);

            hit.gameObject.GetComponent<AudioSource>().Play();
            Cursor.lockState = CursorLockMode.None;
            winPanel.transform.DOScale(1, 1f);
        }
    }
}
