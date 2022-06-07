using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coche : MonoBehaviour, IComparable
{
    // Start is called before the first frame update

    public GameObject carObject;
    Vector3[] originals;
    public Vector3 pos;
    float param;
    List<Vector3> firstGuides;
    List<Vector3> secondGuides;
    bool inFirstCurve;
    int n;
    float accelerationFactor;
    MeshFilter mf;
    public Vector3 offset;
    public float r;
    public bool playerCar;
    float mass;
    int currentLap;
    int halfLapCounter;
    int carIndex;


    Vector3 EvalBezier(float t) {
        Vector3 bez = Vector3.zero;
        for (int i = 0; i < n; i++) {
            float u = Combination(n - 1, i) * Mathf.Pow(1.0f - t, n - 1 - i) *
            Mathf.Pow(t, i);
            if(inFirstCurve) {
                 bez += u * firstGuides[i];
            } else {
                 bez += u * secondGuides[i];
                
            }
           
        }
        return bez;
    }

    float Combination(int n, int i){
        return (float)Factorial(n) / (Factorial(i) * Factorial(n - i));
    }

    int Factorial(int n) {
        if (n == 0) return 1;
        else return n * Factorial(n - 1);
    }

    public int CompareTo(object obj) {
        if (obj == null) return 1;

        Coche otherCoche = obj as Coche;
        if (otherCoche != null)
            return this.getProgress().CompareTo(otherCoche.getProgress());
        else
           throw new ArgumentException("Object is not a Coche");
    }



    Vector3 Interpolar(Vector3 A, Vector3 B, float t) {
        return A + t * (B - A);
    }

    Vector3[] ApplyTransform(Matrix4x4 m, Vector3[] verts) {
        int num = verts.Length;
        Vector3[] result = new Vector3[num];
        for(int i =0; i< num; i++){
            Vector3 v = verts[i];
            Vector4 temp = new Vector4(v.x, v.y, v.z, 1);
            result[i] = m * temp;
        }
        return result;
    }
    public void setIndex (int index){
        carIndex = index;
    }
    public int getIndex (){
        return carIndex;
    }

    void Start()
    {
        firstGuides = new List<Vector3>();
        secondGuides = new List<Vector3>();
        inFirstCurve = true;
        param = 0.001f;

        currentLap = 0;
        r = 1;
        mass = 500;
        originals = carObject.GetComponent<MeshFilter>().mesh.vertices;
        

        firstGuides.Add(new Vector3(0, 0, 0));
        firstGuides.Add(new Vector3(26.3f, 0, 0));
        firstGuides.Add(new Vector3(96.7f, 0, -56.1f));
        firstGuides.Add(new Vector3(12.3f, 0, -228));
        firstGuides.Add(new Vector3(-237f, 0, -272));
        firstGuides.Add(new Vector3(-528f, 0, -407));
        

        secondGuides.Add(new Vector3(-528, 0, -407));
        secondGuides.Add(new Vector3(-653, 0, -409));
        secondGuides.Add(new Vector3(-445, 0, -126));
        secondGuides.Add(new Vector3(-558, 0, -27));
        secondGuides.Add(new Vector3(-299, 0, 26));
        secondGuides.Add(new Vector3(0, 0, 0));
        
        n = firstGuides.Count;

    }

    // Update is called once per frame
    void Update()
    {   
       

        accelerationFactor = 1;
        if(playerCar){
            if(Input.GetKey("up")){
                accelerationFactor = 2.0f;
            } else if (Input.GetKey("down")) {
                accelerationFactor = 0.5f;
            }
        }
        

        if (param >= 1.0f) {
            param = 0.001f;
            inFirstCurve = !inFirstCurve;
            if (inFirstCurve) currentLap++;
            halfLapCounter ++;
        }
        Vector3 prev = EvalBezier(param);
        param += accelerationFactor * 0.001f;
        pos = EvalBezier(param);
        Matrix4x4 t = Transformations.TranslateM(pos.x, pos.y, pos.z);
        
        Vector3 du = (pos - prev).normalized;
        float alpha = Mathf.Atan2(-du.z, du.x) * Mathf.Rad2Deg;
        Matrix4x4 r = Transformations.RotateM(alpha, Transformations.AXIS.AX_Y);

        carObject.GetComponent<MeshFilter>().mesh.vertices = ApplyTransform(t * r, originals);
        carObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
    }

    public Vector3 getForce() {
        Vector3 norm = Math3D.Normalize(pos);

        return new Vector3 (mass * accelerationFactor * norm.x, 0 , mass * accelerationFactor * norm.z);
    }

    public float getProgress() {
        return  param*0.5f + halfLapCounter*0.5f;
    }
}
