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

        Debug.Log(thirdVector3 == Vector3.Cross(firstVector3, secondVector3));
        Debug.Log(thirdVector3);
        Debug.Log(Vector3.Cross(firstVector3, secondVector3));
        CalculateArea(firstVector3, secondVector3, thirdVector3);

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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(firstNormalized, secondNormalized);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(secondNormalized, thirdNormalized);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(thirdNormalized, firstNormalized);







    }

    private void CalculateArea(Vector3 firstVector3, Vector3 secondVector3, Vector3 thirdVector3)
    {
        Vector3[] vector = new Vector3[3];
        vector[0] = firstVector3;
        vector[1] = secondVector3;
        vector[2] = thirdVector3;


        if ((vector[0].x >= 0 && vector[1].x >= 0 && vector[2].x >= 0) || (vector[0].x < 0 && vector[1].x < 0 & vector[2].x < 0))
        {
            Debug.Log("Eje x");
            for (int i = 0; i < vector.Length - 1; i++)
            {
                for (int j = 0; j < vector.Length - i - 1; j++)
                {
                    if (MathF.Abs(vector[j].x) > MathF.Abs(vector[j + 1].x))
                    {
                        Vector3 aux = vector[j];
                        vector[j] = vector[j + 1];
                        vector[j] = aux;
                    }
                }
            }
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
                        Vector3 aux = vector[j];
                        vector[j] = vector[j + 1];
                        vector[j] = aux;
                    }
                }
            }
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
                        Vector3 aux = vector[j];
                        vector[j] = vector[j + 1];
                        vector[j] = aux;
                    }
                }
            }
        }
        //checkear que esten todos en el mismo plano
       


        firstNormalized = vector[0].normalized * vector[0].magnitude;
        secondNormalized = vector[1].normalized * vector[0].magnitude;
        thirdNormalized = vector[2].normalized * vector[0].magnitude;


        firstForce = vector[0] - vector[1];
        secondForce = vector[0] - vector[2];
        Debug.Log(Vector3.Cross(firstForce, secondForce));
        result2 = CrossProduct(firstForce, secondForce);

        area = (MathF.Sqrt(MathF.Pow(result2.x, 2) + MathF.Pow(result2.y, 2) + MathF.Pow(result2.z, 2))) / 2.0f;


    }

}
