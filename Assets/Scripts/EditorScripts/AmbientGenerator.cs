using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientGenerator : MonoBehaviour
{
    public GameObject column;

    void Start()
    {
        int mainCoordinate = -60;
        int x;
        int y;
        int z = mainCoordinate;
        int deviation; 

        for (int k = 0; k < 10; k++)
        {
            x = mainCoordinate;
            z += 12;
            int tall;

            for (int i = 0; i < 10; i++)
            {
                x += Random.Range(10, 15);
                int scale = Random.Range(1, 4); //������ �������

                if (scale <= 2)
                {
                    y = Random.Range(-7, -3); //��������� �� ������ ��� ��������� ������� (��������)
                    tall = 1;
                }
                else
                {
                    y = 30; //��������� �� ������ ��� ������� ������� 
                    tall = 3;
                }                
                                                               
                GameObject newObject = Instantiate(column, new Vector3(x, y, deviation = z + Random.Range(-3, 4)), Quaternion.identity); //������� �������
                newObject.transform.localScale = new Vector3(scale, tall, scale); // ����������� ����������� ������� �������

            }
        }
    }
}