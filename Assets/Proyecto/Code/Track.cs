using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    List<Coche> cars;
    public List<GameObject> carObjects;
    ParticleSystem ps;
    int[] positions;

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject empty = new GameObject();
        empty.AddComponent<ParticleSystem>();
        ps = empty.GetComponent<ParticleSystem>();
        cars = new List<Coche>();
        for(int i = 0; i < carObjects.Count;  i++) {
            carObjects[i].AddComponent<Coche>();
            Coche car = carObjects[i].GetComponent<Coche>();
            cars.Add(car);
            if(i == 0){
                cars[i].playerCar = true;
            }
            cars[i].carObject = carObjects[i];
            
        }
        ps.setCars(cars);
        ps.particles = 10;
        positions = new int[cars.Count];
    }

    // Update is called once per frame
    void Update()
    {
        // [5, 4, 3, 2, 1, 0]
        /*for(int i = 0; i < cars.Count; i++) {
            
            while(cars[i].getProgress() > positions[j]){
                int current = positions[j];
                positions[j + 1] = current;
                position[j] = i;
                j++;
            }

            // Meter el primero en pos 0
            // El segundo, checar si es más grande que el 0, recorrer.

        }
        */
        
    }
}
