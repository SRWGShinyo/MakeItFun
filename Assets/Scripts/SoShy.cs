using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoShy : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkIfVisible();
    }

    private void checkIfVisible()
    {
        Vector3 viewpos = cam.WorldToViewportPoint(transform.position);
        if (viewpos.x >= 0 && viewpos.x <= 1 && viewpos.y >= 0 && viewpos.y <= 1 && viewpos.z > 0)
        {
            MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
                mesh.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }

        else
        {
            MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
                mesh.enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}
