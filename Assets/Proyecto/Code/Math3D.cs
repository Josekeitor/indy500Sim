using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

 public class Math3D
 {
     public static float Dot(Vector3 a, Vector3 b)
     {
         // a = (ax, ay, az)
         // b = (bx, by, bz)
         // dot = ax*bx+ay*by+az*bz
         return a.x * b.x + a.y * b.y + a.z * b.z;
     }

     public static Vector3 Cross(Vector3 a, Vector3 b)
     {
         // a =      (ax, ay, az)
         // b =      (bx, by, bz)
         // cross =  (ay*bz-az*by,  az*bx-ax*bz, ax*by-ay*bx)
         return new Vector3(a.y*b.z-a.z*b.y, a.z*b.x-a.x*b.z, a.x*b.y-a.y*b.x);
     }

     public static Vector3 Add(Vector3 a, Vector3 b)
     {
         return new Vector3(a.x+b.x, a.y+b.y, a.z+b.z);
     }

     public static Vector3 Subtract(Vector3 a, Vector3 b)
     {
         return new Vector3(b.x - a.x, b.y - a.y, b.z - a.z);
     }

     public static Vector3 Scalar(Vector3 a, float s)
     {
         return new Vector3(a.x*s, a.y*s, a.z*s);
     }

     public static float Magnitude(Vector3 v) // The length of the vector
     {
         return Mathf.Sqrt(v.x*v.x + v.y*v.y + v.z*v.z);
     }

     public static Vector3 Normalize(Vector3 v) // Same vector, but magnitude = 1
     {
         float mag = Magnitude(v);
         return new Vector3(v.x/mag, v.y/mag, v.z/mag);
     }

     public static float AngleBetween(Vector3 a, Vector3 b)
     {
         // dot(a,b) = |a|*|b|*cos(angle)
         // angle = arccos(dot(au, bu))
         return Mathf.Acos(Dot(Normalize(a), Normalize(b)));
     }

     public static Vector3 ScalarMult(float alpha, Vector3 v){
         return new Vector3(alpha*v.x, alpha*v.y, alpha*v.z);
     }

     public static Vector3 Reflect(Vector3 n, Vector3 v) {
         Vector3 nu = Normalize(n);
         float dotNV = Dot(nu, v);
         Vector3 parallel = ScalarMult(dotNV, nu);
         Vector3 orthogonal = v - parallel;
         Vector3 reflected = parallel - orthogonal;
         return reflected;
     }

     public static Vector3 SphericalToCartesian(float i, float a, float r, Vector3 sc) {
         float x, y, z;

         i *= Mathf.Deg2Rad;
         a *= Mathf.Deg2Rad;

         x = r * Mathf.Sin(i) * Mathf.Sin(a) + sc.x;
         y = r * Mathf.Cos(i) + sc.y;
         z = r * Mathf.Sin(i) * Mathf.Cos(a) + sc.z;

         return new Vector3(x, y, z);
     }
 }