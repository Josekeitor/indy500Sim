using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

José Carlos Acosta García   A01650306
Karina Amador               A01181204
Angel Limones Quirino       A00825333
Alfredo Jose Welsh Martinez A00825988

*/

public class ParticleSystem : MonoBehaviour
{
    public int particles;
    List<Particle> scripts;
    List<Coche> cars;

    // Start is called before the first frame update
    void Start()
    {
        scripts = new List<Particle>();
        for(int i = 0; i < particles; i++){
            GameObject particle = new GameObject();
            particle.AddComponent<Particle>();
            Particle p = particle.GetComponent<Particle>();
            scripts.Add(p);
            p.r = Random.Range(0.2f, 0.5f);
            float x = Random.Range(19.0f, 21.0f);
            float y = 0 + 2 * p.r;
            float z = Random.Range(-8.0f, -11.0f);
            p.p = new Vector3(x, y, z);
            p.forces = Vector3.zero;
            p.forces.x = Random.Range(-5.0f, 5.0f);
            p.forces.z = Random.Range(-5.0f, 5.0f);
            p.g = 9.81f;
            p.mass = p.r * 2.0f;
            p.rc = 0.1f;
            p.dragUp = 0.000001f;
            p.dragDown = 0.1f;
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < scripts.Count; i++) {
            for(int j = 0; j < cars.Count; j++){
                Particle particle = scripts[i];
                Coche car = cars[j];
                if(particle.CheckCollision(car)){
                    particle.Bounce(car);
                    car.damage();
                }
            }
        }


        for(int i = 0; i < scripts.Count; i++){
            
            scripts[i].isColliding = false;
            for (int j = 0; j < scripts.Count; j++) {
                
                if (i != j) {
                    Particle one = scripts[i];
                    Particle two = scripts[j];
                    if(one.CheckCollision(two)){
                        one.isColliding = true;
                        two.isColliding = true;
                    }
                }
            }
        }
        
    }

    public void setCars(List<Coche> cars){
        this.cars = cars;
    }

}
