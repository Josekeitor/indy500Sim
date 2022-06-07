using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionHanlder : MonoBehaviour
{
    List<Coche> cars;
    List<TextMeshProUGUI> textComponents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cars.Sort();
        for (int i = 0; i < cars.Count; i++) {
            if(cars[i].getIndex() == 0){
                textComponents[i].SetText("Player - " + (i + 1));
            }
            else{
                textComponents[i].SetText("AI "+ cars[i].getIndex() + " - "+ (i + 1));

            }
        }
        
    }

    public void setTextComponents(List<TextMeshProUGUI> textComponents) {
        this.textComponents = textComponents;
    }

    public void setCars(List<Coche> cars){
        this.cars = cars;
    }
}
