using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Track : MonoBehaviour
{
    public List<Coche> cars;
    public List<GameObject> carObjects;
    public List<TextMeshProUGUI> textComponents;

    ParticleSystem ps;
    PositionHanlder ph;
    int[] positions;


    // Start is called before the first frame update
    void Start()
    {
        
        GameObject empty = new GameObject();
        empty.AddComponent<ParticleSystem>();
        empty.AddComponent<PositionHanlder>();
        ps = empty.GetComponent<ParticleSystem>();
        ph = empty.GetComponent<PositionHanlder>();

        cars = new List<Coche>();
        for(int i = 0; i < carObjects.Count;  i++) {
            carObjects[i].AddComponent<Coche>();
            Coche car = carObjects[i].GetComponent<Coche>();
            car.setIndex(i);
            cars.Add(car);
            if(i == 0){
                cars[i].playerCar = true;
            }
            cars[i].carObject = carObjects[i];
            
        }
        ps.setCars(cars);
        ps.particles = 10;
        ph.setCars(cars);
        ph.setTextComponents(textComponents);
        positions = new int[cars.Count];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
