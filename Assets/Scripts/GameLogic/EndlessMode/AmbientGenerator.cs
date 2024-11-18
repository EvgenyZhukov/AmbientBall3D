using System.Linq;
using UnityEngine;

public class AmbientGenerator : MonoBehaviour
{
    public GameObject prefabToGenerate; // Префаб, который нужно генерировать
    public int numRows = 5; // Количество рядов
    public float rowSpacing = 10f; // Расстояние между рядами
    public float objectSpacing = 10f; // Расстояние между объектами в ряду

    void Start()
    {
        GeneratePrefabs();
    }

    void GeneratePrefabs()
    {
        Vector3 startPosition = transform.position; // Начальная позиция для генерации
        Transform parentTransform = transform; // Ссылка на трансформ объекта, на котором находится скрипт

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                // Создаем случайные отклонения
                float offsetX = Random.Range(-3f, 3f);
                float offsetY = Random.Range(-6f, 0f);
                float offsetZ = Random.Range(-3f, 3f);

                Vector3 randomOffset = new Vector3(offsetX, offsetY, offsetZ);

                // Генерируем позицию с учетом отклонений
                Vector3 spawnPosition = startPosition + new Vector3(col * objectSpacing, 0f, row * rowSpacing) + randomOffset;

                // Создаем объект с учетом случайных отклонений
                GameObject spawnedPrefab = Instantiate(prefabToGenerate, spawnPosition, Quaternion.identity, parentTransform);

                // Создаем случайный масштаб
                float randomScale = Random.Range(1.2f, 2.5f);
                spawnedPrefab.transform.localScale = new Vector3(randomScale, spawnedPrefab.transform.localScale.y, randomScale);

                // Получаем коллайдер объекта
                Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();

                // Получаем все коллайдеры с тегом "EndlessColumn"
                Collider[] endlessColumnColliders = GameObject.FindGameObjectsWithTag("EndlessColumn")
                    .Select(obj => obj.GetComponent<Collider>())
                    .ToArray();

                // Проверяем пересечение с коллайдерами объектов "EndlessColumn"
                foreach (Collider columnCollider in endlessColumnColliders)
                {
                    if (prefabCollider.bounds.Intersects(columnCollider.bounds))
                    {
                        // Уничтожаем генерируемый объект
                        Destroy(spawnedPrefab);
                        break; // Прерываем цикл, так как объект уже был уничтожен
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
                int scale = Random.Range(1, 3); //Размер колонны

                if (scale <= 2)
                {
                    y = Random.Range(-7, -3); //Положение по высоте для небольшой колонны (диапазон)
                    tall = 1;
                }
                else
                {
                    y = 30; //Положение по высоте для большой колонны 
                    tall = 3;
                }                
                                                               
                GameObject newObject = Instantiate(column, new Vector3(x, y, deviation = z + Random.Range(-3, 4)), Quaternion.identity); //Создает колонну
                newObject.transform.localScale = new Vector3(scale, tall, scale); // Присваивает создаваемой колонне масштаб

            }
        }
        
    }
}
*/