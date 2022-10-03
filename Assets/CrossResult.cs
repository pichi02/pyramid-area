using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CrossResult : MonoBehaviour
{
    [SerializeField] private Vector3 firstVector3;
    [SerializeField] private Vector3 secondVector3;
    [SerializeField] private Vector3 thirdVector3;
    [SerializeField] private Vector3 result2;
    [SerializeField] Vector3 firstForce;
    [SerializeField] Vector3 secondForce;
    [SerializeField] Vector3 firstNormalized;
    [SerializeField] Vector3 secondNormalized;
    [SerializeField] Vector3 thirdNormalized;

    [SerializeField] float gizmoLength = 4.5f;

    [SerializeField] double area = 0;
    [SerializeField] double pyramid;
    void Update()
    {
        secondVector3 = new Vector3(firstVector3.y, -firstVector3.x, firstVector3.z);
        thirdVector3 = CrossProduct(firstVector3, secondVector3);
        CalculateArea(firstVector3, secondVector3, thirdVector3);
        pyramid = PyramidSurface(firstNormalized, secondNormalized, thirdNormalized);
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
                        Vector3 aux = vector[j + 1];
                        vector[j + 1] = vector[j];
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

        length = vector[0].magnitude;
        result2 = CrossProduct(firstForce, secondForce);
        area = Pythagoras(result2) / 2.0f;
    }

    Vector3 cutPyramid(Vector3 origin, Vector3 toCut, int axis)
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

    double PyramidSurface(Vector3 vector1, Vector3 vector2, Vector3 vector3)
    {
        double totalSurface = TriangleArea(vector1, vector2) + TriangleArea(vector1, vector3) + TriangleArea(vector3, vector2);

        return totalSurface;
    }

    double TriangleArea(Vector3 vector1, Vector3 vector2)
    {
        //LARGO DE LA BASE DEL TRIANGULO
        double triangleBase = MathF.Sqrt(MathF.Pow(vector2.x - vector1.x, 2) + MathF.Pow(vector2.y - vector1.y, 2) + MathF.Pow(vector2.z - vector1.z, 2));
        //PUNTO MEDIO ENTRE LOS 2 VECTORES 
        Vector3 middlePoint;
        middlePoint.x = (vector1.x + vector2.x) / 2;
        middlePoint.y = (vector1.y + vector2.y) / 2;
        middlePoint.z = (vector1.z + vector2.z) / 2;
        //USANDO LA DISTANCIA DEL PUNTO MEDIO AL 0,0 PARA SABER LA ALTURA
        double triangleHeight = Pythagoras(middlePoint);
        //AREA DEL TRIANGULO
        return triangleBase * triangleHeight / 2;
    }

    double Pythagoras(Vector3 vector)
    {
        return MathF.Sqrt(MathF.Pow(vector.x, 2) + MathF.Pow(vector.y, 2) + MathF.Pow(vector.z, 2));
    }
}