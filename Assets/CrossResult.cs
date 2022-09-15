using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CrossResult : MonoBehaviour
{

    [SerializeField] private Vector3 firstVector3;
    [SerializeField] private Vector3 secondVector3;
    [SerializeField] private Vector3 thirdVector3;
    [SerializeField] float gizmoLength = 4.5f;
    [SerializeField] float area =0.0f;
    [SerializeField] private Vector3 result2;
    [SerializeField] Vector3 firstForce;
    [SerializeField]Vector3 secondForce  ;

    void Update()
    {
        secondVector3 = new Vector3(firstVector3.y, -firstVector3.x, firstVector3.z);
        thirdVector3 = CrossProduct(firstVector3,secondVector3);
      
        Debug.Log(thirdVector3 == Vector3.Cross(firstVector3, secondVector3));
        Debug.Log(thirdVector3);
        Debug.Log(Vector3.Cross(firstVector3, secondVector3));
        CalculateArea(firstVector3,secondVector3,thirdVector3);

    }

    Vector3 CrossProduct(Vector3 firstVector3,Vector3 seconVector3)//CORRECTO
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

        Gizmos.DrawLine(Vector3.zero, firstVector3  * gizmoLength);
        Gizmos.color = Color.blue;                    
        Gizmos.DrawLine(Vector3.zero, secondVector3 * gizmoLength);
        Gizmos.color = Color.green;                   
        Gizmos.DrawLine(Vector3.zero, thirdVector3.normalized  * gizmoLength);
        
    }

    private void CalculateArea(Vector3 firstVector3, Vector3 secondVector3, Vector3 thirdVector3)
    {
        Vector3[] vector = new Vector3[3];
        vector[0] = firstVector3;
        vector[1] = secondVector3;
        vector[2] = thirdVector3;
        //checkear que esten todos en el mismo plano
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
    
  //cambiar maginitud y hacer  el corte ahi vector cruze de lineas

        firstForce = vector[0] - vector[1];
        secondForce = vector[0] - vector[2];
        Debug.Log(Vector3.Cross(firstForce,secondForce));
        result2 = CrossProduct(firstForce, secondForce);
        
         area = (MathF.Sqrt(MathF.Pow(result2.x, 2) + MathF.Pow(result2.y, 2) + MathF.Pow(result2.z, 2)))/2.0f;
        
    }

}
