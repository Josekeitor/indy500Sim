using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

José Carlos Acosta García   A01650306
Karina Amador               A01181204
Angel Limones Quirino       A00825333
Alfredo Jose Welsh Martinez A00825988

*/

public class Particle : MonoBehaviour
{
    public float mass;
    public float g;         // gravedad
    public float r;         // radio
    public float rc;        // Restitution Coefficient (elastic=1, inelastic = 0)
    public Vector3 p;       // position
    public Vector3 forces;
 
    Vector3 a;          // acceleration
    Vector3 prev;       // previous position
    Vector3 temp;       // temporal position
    GameObject sph;     // game object for the particle
    Vector3 originalColor;
    Vector3 collisionColor = new Vector3(1.0f, 0.0f, 0.0f);
    public bool isColliding;
    MeshRenderer mr;

    Vector3 cubeDimension = new Vector3(15.0f, 15.0f, 15.0f);
    Vector3 cubePosition = new Vector3(0f, 7.5f, 0f);

    public float dragUp;
    public float dragDown;

    public Vector3 drag; 

    float maxY;
    float minY; 
    float maxX;
    float minX;
    float maxZ;
    float minZ;


    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
        sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph.transform.position = p;
        sph.transform.localScale = new Vector3(r * 2, r * 2, r * 2);

        float cr = Random.Range(0.0f, 1.0f);
        float cg = Random.Range(0.0f, 1.0f);
        float cb = Random.Range(0.0f, 1.0f);

        originalColor = new Vector3(cr, cg, cb);
        mr = sph.GetComponent<MeshRenderer>();
        mr.material.color = new Color(originalColor.x, originalColor.y, originalColor.z);
        prev = p;
        a = Vector3.zero;

        maxY = cubeDimension.y;
        minY =  0; 
        maxX = cubeDimension.x / 2;
        minX = -(cubeDimension.x / 2);
        maxZ = cubeDimension.z / 2;
        minZ = -(cubeDimension.z / 2);

        drag.y = 1;
        drag.x = 1;
        drag.z = 1;
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.frameCount > 20)
        {
            forces.y += -g * mass * Time.deltaTime;
            if (p.y > prev.y) drag = -forces * dragUp;
            else if (p.y < prev.y) drag = -forces * dragDown;
            else drag = Vector3.zero;
            forces += drag;
            CheckFloor();
            Verlet(Time.deltaTime);
        }

        if(isColliding){
            SetCollisionColor();
        } else if(!isColliding){
            ResetColor();
        }

    }

    void Verlet(float dt)
    {
        temp = p;                           // save p temporarily
        a = forces / mass;                  // a = F/m
        p = 2 * p - prev + (a * dt * dt);     // Verlet
        prev = temp;                        // restore previous position
        sph.transform.position = p;
    }

    void CheckFloor()
    {
        if(p.y + r >= maxY) {
            //Chocando arriba
            forces.y = -forces.y * rc;
            float diff = p.y - prev.y;
            p.y = prev.y;
            prev.y += diff;
        }

        if(p.y - r <= minY) {
            //Chocando abajo
            forces.y = -forces.y * rc;
            float diff = prev.y - p.y;
            p.y = prev.y;
            prev.y -= diff;
        }
        
    }

    public void Bounce(Coche other) {
        if(p.y + r >= other.pos.y + other.r) {
            //Chocando arriba
            forces.y = -forces.y * rc;
            forces.y += other.getForce().y;
            float diff = p.y - prev.y;
            p.y = prev.y;
            prev.y += diff;
        }

        if(p.y - r <= other.pos.y - other.r) {
            //Chocando abajo
            forces.y = -forces.y * rc;
            forces.y += other.getForce().y;
            float diff = prev.y - p.y;
            p.y = prev.y;
            prev.y -= diff;
        }


        if(p.x + r >= other.pos.x + other.r) {
            //Chocando derecha
            forces.x = -forces.x * rc;
            forces.x += other.getForce().x;
            float diff = p.x - prev.x;
            p.x = prev.x;
            prev.x += diff;
        }

        if(p.x - r <= other.pos.x - other.r) {
            //Chocando izquierda
            forces.x = -forces.x * rc;
            forces.x += other.getForce().x;
            float diff = prev.x - p.x;
            p.x = prev.x;
            prev.x -= diff;
        }

        if(p.z + r >= other.pos.z + other.r) {
            //Chocando adelate
            forces.z = -forces.z * rc;
            forces.z += other.getForce().z;
            float diff = p.z - prev.z;
            p.z = prev.z;
            prev.z += diff;
        }

        if(p.z - r <= other.pos.z - other.r) {
            //Chocando atras
            forces.z = -forces.z * rc;
            forces.z += other.getForce().z;
            float diff = prev.z - p.z;
            p.z = prev.z;
            prev.z -= diff;
        }
    }

    public bool CheckCollision(Particle other) {
        float dx =  other.p.x - p.x;
        float dy = other.p.y - p.y;
        float dz = other.p.z - p.z;

        float sumR = other.r + r;
        sumR *= sumR;

        return sumR > dx * dx + dy * dy + dz * dz;
    }

    public bool CheckCollision(Coche other) {
        float dx =  other.pos.x - p.x;
        float dy = other.pos.y - p.y;
        float dz = other.pos.z - p.z;

        float sumR = other.r + r;
        sumR *= sumR;

        return sumR > dx * dx + dy * dy + dz * dz;
    }    

    public void SetCollisionColor() {
        mr.material.color = new Color(collisionColor.x, collisionColor.y, collisionColor.z);             
    }

    public void ResetColor() {
        mr.material.color = new Color(originalColor.x, originalColor.y, originalColor.z);
    }

    public void disable() {
        mr.enabled = false;
    }

    public void enable() {
        mr.enabled = true;
    }

    public Vector3 getPosition() {
        return sph.transform.position;
    }

    
}
