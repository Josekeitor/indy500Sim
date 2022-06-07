using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPosition : MonoBehaviour
{
    Track track;
    // Start is called before the first frame update
    void Start()
    {
        track = gameObject.AddComponent(typeof(Track)) as Track;

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < track.cars.Count; i++)
        {
            Coche carroActual = track.cars[i];
            Debug.Log("El carro " + carroActual.getIndex()+ " en posicion:" + i);
        }
        
    }
}
