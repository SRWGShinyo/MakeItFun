using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour
{
    public Material emission;
    public Material nothing;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public IEnumerator emitir()
    {
        gameObject.GetComponent<MeshRenderer>().material = emission;
        source.Play();
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<MeshRenderer>().material = nothing;
    }

    public void IEmit()
    {
        gameObject.GetComponent<MeshRenderer>().material = emission;
    }

    public void emitAndNotify()
    {
        StartCoroutine(emitir());
        GameObject.FindGameObjectWithTag("Event").GetComponent<LightEvent>().notify(gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            emitAndNotify();
    }
}
