using UnityEngine;

public class EndlessLevelPartCreator : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int maxZPosition = 46;
    public Vector3 lastPosition;

    public int[,] difficultMatrix = new int[3, 2];
    public bool firstPart = false;
    public GameObject additionalStars;

    void Start()
    {
        ObjectSpawner();
        if (firstPart)
        {
            additionalStars.SetActive(true);
        }
    }

    /// <summary>
    /// Создает объекты сегмента уровня
    /// </summary>
    public void ObjectSpawner()
    {
        int posColumnX = difficultMatrix[0, 0];
        int posColumnZ = difficultMatrix[1, 0];
        int posColumnY = difficultMatrix[2, 0];

        int slopeColumn = difficultMatrix[0, 1];
        int swingColumn = difficultMatrix[1, 1];
        int fallingColumn = difficultMatrix[2, 1];

        Debug.Log("time = " + Time.time
            + "; posColumnX = " + posColumnX + "; posColumnZ = " + posColumnZ + "; posColumnY = " + posColumnY
            + "; slopeColumn = " + slopeColumn + "; swingColumn = " + swingColumn + "; fallingColumn = " + fallingColumn);

        Vector3 initialPosition = transform.localPosition;
        initialPosition.y -= 1.5f;

        while (lastPosition.z < maxZPosition)
        {
            int direction = Random.Range(0, 2);
            if (direction == 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            initialPosition.x += Random.Range(0.5f, posColumnX + 1f) * direction;
            if (initialPosition.x >= 6 || initialPosition.x <= -6)
            {
                initialPosition.x /= 2;
            }

            if (slopeColumn == 0)
            {
                initialPosition.z += 2.25f + Random.Range(0, posColumnZ + 1);
            }
            else
            {
                initialPosition.z += 2.25f + Random.Range(2, posColumnZ + 1);
            }

            initialPosition.y += Random.Range(0, posColumnY + 1) * direction;
            if (initialPosition.y >= 6)
            {
                initialPosition.y /= 2;
            }
            if (initialPosition.y < -1.5f)
            {
                initialPosition.y = -1.5f;
            }

            GameObject spawnedPrefab = Instantiate(prefabToSpawn, initialPosition, Quaternion.identity);
            spawnedPrefab.transform.SetParent(transform);
            lastPosition = spawnedPrefab.transform.localPosition;

            if (spawnedPrefab.transform.localPosition.z > 48)
            {
                Destroy(spawnedPrefab);
            }

            // Получаем компонент ActiveEndlessColumn на созданном объекте
            ActiveEndlessColumn activeColumn = spawnedPrefab.GetComponent<ActiveEndlessColumn>();

            if (activeColumn != null)
            {
                // Устанавливаем значения переменных на объекте ActiveEndlessColumn
                activeColumn.slopeColumn = slopeColumn;
                activeColumn.swingColumn = swingColumn;
                activeColumn.fallingColumn = fallingColumn;
            }
        }
    }
}