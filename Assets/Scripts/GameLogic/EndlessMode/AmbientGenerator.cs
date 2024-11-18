using System.Linq;
using UnityEngine;

public class AmbientGenerator : MonoBehaviour
{
    public GameObject prefabToGenerate; // ������, ������� ����� ������������
    public int numRows = 5; // ���������� �����
    public float rowSpacing = 10f; // ���������� ����� ������
    public float objectSpacing = 10f; // ���������� ����� ��������� � ����

    void Start()
    {
        GeneratePrefabs();
    }

    void GeneratePrefabs()
    {
        Vector3 startPosition = transform.position; // ��������� ������� ��� ���������
        Transform parentTransform = transform; // ������ �� ��������� �������, �� ������� ��������� ������

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                // ������� ��������� ����������
                float offsetX = Random.Range(-3f, 3f);
                float offsetY = Random.Range(-6f, 0f);
                float offsetZ = Random.Range(-3f, 3f);

                Vector3 randomOffset = new Vector3(offsetX, offsetY, offsetZ);

                // ���������� ������� � ������ ����������
                Vector3 spawnPosition = startPosition + new Vector3(col * objectSpacing, 0f, row * rowSpacing) + randomOffset;

                // ������� ������ � ������ ��������� ����������
                GameObject spawnedPrefab = Instantiate(prefabToGenerate, spawnPosition, Quaternion.identity, parentTransform);

                // ������� ��������� �������
                float randomScale = Random.Range(1.2f, 2.5f);
                spawnedPrefab.transform.localScale = new Vector3(randomScale, spawnedPrefab.transform.localScale.y, randomScale);

                // �������� ��������� �������
                Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();

                // �������� ��� ���������� � ����� "EndlessColumn"
                Collider[] endlessColumnColliders = GameObject.FindGameObjectsWithTag("EndlessColumn")
                    .Select(obj => obj.GetComponent<Collider>())
                    .ToArray();

                // ��������� ����������� � ������������ �������� "EndlessColumn"
                foreach (Collider columnCollider in endlessColumnColliders)
                {
                    if (prefabCollider.bounds.Intersects(columnCollider.bounds))
                    {
                        // ���������� ������������ ������
                        Destroy(spawnedPrefab);
                        break; // ��������� ����, ��� ��� ������ ��� ��� ���������
                    }
                }
            }
        }
    }
}


/*
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
                int scale = Random.Range(1, 3); //������ �������

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
*/