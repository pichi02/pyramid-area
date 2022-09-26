using System;
using System.Numerics;
using UnityEngine;
using Plane = System.Numerics.Plane;
using Vector3 = UnityEngine.Vector3;

public class CrossResult : MonoBehaviour
{

    [SerializeField] private Vector3 firstVector3;
    [SerializeField] private Vector3 secondVector3;
    [SerializeField] private Vector3 thirdVector3;
    [SerializeField] float gizmoLength = 4.5f;
    [SerializeField] float area = 0.0f;
    [SerializeField] double pyramidSurface = 0;
    [SerializeField] private Vector3 result2;
    [SerializeField] Vector3 firstForce;
    [SerializeField] Vector3 secondForce;
   [SerializeField] Vector3 firstNormalized;
   [SerializeField] Vector3 secondNormalized;
  [SerializeField] Vector3 thirdNormalized;

    void Update()
    {
        secondVector3 = new Vector3(firstVector3.y, -firstVector3.x, firstVector3.z);
        thirdVector3 = CrossProduct(firstVector3, secondVector3);

        //Debug.Log(thirdVector3 == Vector3.Cross(firstVector3, secondVector3));
        //Debug.Log(thirdVector3);
        //Debug.Log(Vector3.Cross(firstVector3, secondVector3));
        CalculateArea(firstVector3, secondVector3, thirdVector3);
       // PyramidSurface(firstNormalized, secondNormalized, thirdNormalized);
    }

    Vector3 CrossProduct(Vector3 firstVector3, Vector3 seconVector3)//CORRECTO
    {

        Vector3 result;

        float i = (firstVector3.y * secondVector3.z) - (firstVector3.z * secondVector3.y);
        float j = (firstVector3.x * secondVector3.z) - (firstVector3.z * secondVector3.x);
        float k = (firstVector3.x * secondVector3.y) - (firstVector3.y * secondVector3.x);
        result = new Vector3(i, -j, k);
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, firstVector3 * gizmoLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, secondVector3 * gizmoLength);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, thirdVector3 * gizmoLength);

     Gizmos.color = Color.yellow;
     Gizmos.DrawLine(Vector3.zero, firstNormalized);
      Gizmos.color = Color.black;
      Gizmos.DrawLine(Vector3.zero, secondNormalized);
      Gizmos.color = Color.magenta;
      Gizmos.DrawLine(Vector3.zero, thirdNormalized);

        //Piramide
       //Gizmos.color = Color.red;
       //Gizmos.DrawLine(firstNormalized  , secondNormalized);
       //Gizmos.color = Color.blue;       
       //Gizmos.DrawLine(secondNormalized , thirdNormalized);
       //Gizmos.color = Color.green;      
       //Gizmos.DrawLine(thirdNormalized  , firstNormalized);
    }

    
    private void CalculateArea(Vector3 firstVector3, Vector3 secondVector3, Vector3 thirdVector3)
    {
        Vector3[] vector = new Vector3[3];
        vector[0] = firstVector3;
        vector[1] = secondVector3;
        vector[2] = thirdVector3;

        float length;
        if ((vector[0].x >= 0 && vector[1].x >= 0 && vector[2].x >= 0) || (vector[0].x < 0 && vector[1].x < 0 & vector[2].x < 0))
        {
            Debug.Log("Eje x");
            for (int i = 0; i < vector.Length - 1; i++)
            {
                for (int j = 0; j < vector.Length - i - 1; j++)
                {
                    if (MathF.Abs(vector[j].x) > MathF.Abs(vector[j + 1].x))
                    {
                        Vector3 aux = vector[j + 1];
                        vector[j + 1] = vector[j];
                        vector[j] = aux;
                    }
                }
            }
            firstNormalized = vector[0];
            secondNormalized = cutPyramid(vector[0], vector[1], 0);
            thirdNormalized = cutPyramid(vector[0], vector[2], 0);

        }
        if ((vector[0].y >= 0 && vector[1].y >= 0 && vector[2].y >= 0) || (vector[0].y < 0 && vector[1].y < 0 & vector[2].y < 0))
        {
            Debug.Log("Eje Y");
            for (int i = 0; i < vector.Length - 1; i++)
            {
                for (int j = 0; j < vector.Length - i - 1; j++)
                {
                    if (MathF.Abs(vector[j].y) > MathF.Abs(vector[j + 1].y))
                    {
                        Vector3 aux = vector[j+1];
                        vector[j+1] = vector[j];
                        vector[j] = aux;
                      
                    }
                }
            }

            firstNormalized = vector[0];
            secondNormalized = cutPyramid(vector[0], vector[1], 1);
            thirdNormalized = cutPyramid(vector[0], vector[2], 1);

        }
        if ((vector[0].z >= 0 && vector[1].z >= 0 && vector[2].z >= 0) || (vector[0].z < 0 && vector[1].z < 0 & vector[2].z < 0))
        {
            Debug.Log("Eje Z");
            for (int i = 0; i < vector.Length - 1; i++)
            {
                for (int j = 0; j < vector.Length - i - 1; j++)
                {
                    if (MathF.Abs(vector[j].z) > MathF.Abs(vector[j + 1].z))
                    {
                        Vector3 aux = vector[j + 1];
                        vector[j + 1] = vector[j];
                        vector[j] = aux;
                    }
                }
            }

            firstNormalized = vector[0];
            secondNormalized = cutPyramid(vector[0], vector[1], 2);
           thirdNormalized = cutPyramid(vector[0], vector[2], 2);



        }
        //checkear que esten todos en el mismo plano
      
        length = vector[0].magnitude;

       // secondNormalized = pointCutCalculator(Vector3.zero, vector[1],length);
       // thirdNormalized = pointCutCalculator(Vector3.zero, vector[2], length);



       // Debug.Log(Vector3.Cross(firstForce, secondForce));
        result2 = CrossProduct(firstForce, secondForce);

        area = (MathF.Sqrt(MathF.Pow(result2.x, 2) + MathF.Pow(result2.y, 2) + MathF.Pow(result2.z, 2))) / 2.0f;


    }

    Vector3 cutPyramid(Vector3 origin, Vector3 toCut,int axis)
    {

        Vector3 cut = new Vector3();
        float test;
        float test3;
        float newX;
        float newY;
        float newZ;
        switch (axis)
        {
            case 0:


                test = MathF.Atan(toCut.x / toCut.y);
                newY = origin.x / MathF.Tan(test);
                test3 = MathF.Atan(toCut.x / toCut.z);
                newZ = origin.x / MathF.Tan(test3);
                cut = new Vector3(origin.x, newY, newZ);
                break;
            case 1:

                test = MathF.Atan(toCut.y / toCut.x);
                newX = origin.y / MathF.Tan(test);
                test3 = MathF.Atan(toCut.y / toCut.z);
                newZ = origin.y / MathF.Tan(test3);
                cut = new Vector3(newX, origin.y, newZ);
                break;
            case 2:

                test = MathF.Atan(toCut.z / toCut.y);
                newY = origin.z / MathF.Tan(test);
                test3 = MathF.Atan(toCut.z / toCut.x);
                newX = origin.z / MathF.Tan(test3);
                cut = new Vector3(newX, newY, origin.z);
                break;
       }



        return cut;
    }
    // Sacar area de la piramide - la base((base*altura/2))
    void PyramidSurface(Vector3 normalized1, Vector3 normalized2, Vector3 normalized3)
    {
        double x = (normalized1.x + normalized2.x + normalized3.x) / 3;
        double y = (normalized1.y + normalized2.y + normalized3.y) / 3;
        double pyramidHeight = Math.Sqrt((x * x) + (y * y));

        pyramidSurface = (pyramidHeight * area) / 3;


    }

}
