using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchMasterFlag : MonoBehaviour
{
    public List<GameObject> allPointsInOrder;
    int indexPos = 0;
    public GameObject explosion;
    public AudioSource sfeet;
    public GameObject explosionSpawner;

    public void moveToNext()
    {
        indexPos = (indexPos + 1) % allPointsInOrder.Count;
        sfeet.Play();
        Instantiate(explosion, explosionSpawner.transform.position, explosionSpawner.transform.rotation);
        gameObject.transform.position = allPointsInOrder[indexPos].transform.position;
    }
}
