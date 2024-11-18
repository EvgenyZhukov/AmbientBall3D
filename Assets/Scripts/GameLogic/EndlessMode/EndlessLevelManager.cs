using PlayerPrefsSavingMethods;
using UnityEngine;

public class EndlessLevelManager : MonoBehaviour
{
    public int score = 0;
    public int indexLevelPart = 1;
    public bool scoreUp = false;

    public GameObject levelPartPrefab;
    public GameObject gameBoundaryObject; // Ссылка на объект "GameBoundary"

    public GameObject[] levelParts;
    public GameObject levelElementsInteractiveObject;


    bool firstSpawn = true;


    private void Awake()
    {
        bool inProgressTemp = SaveLoadData.GetInProgressTemp();

        if (inProgressTemp)
        {
            score = SaveLoadData.GetEndlessScoreTemp();
        }
        else
        {
            score = SaveLoadData.GetStarsEndlessMode();
        }
    }

    void Start()
    {
        levelParts = new GameObject[3]; // Создаем первые три префаба при старте
        CreateLevelPart(0, score + 0);
        CreateLevelPart(1, score + 1);
        CreateLevelPart(2, score + 2);
        UpdateGameBoundaryPosition(0);
    }
    void Update()
    {
        
        if (scoreUp)
        {
            score++;
            indexLevelPart++;
            if (indexLevelPart >= 3)
            {
                indexLevelPart = 0;
            }
            DestroyLevelPart(indexLevelPart);
            CreateLevelPart(indexLevelPart, score + 1);
            UpdateGameBoundaryPosition(indexLevelPart); // Обновляем позицию объекта GameBoundary

            SaveLoadData.SetEndlessScoreTemp(score);
            SaveLoadData.SetStarsEndlessMode(score);
            if (score > SaveLoadData.GetStarsEndlessModeTotal())
            {
                SaveLoadData.SetStarsEndlessModeTotal(score);
            }
            scoreUp = false;
        }
    }
    void CreateLevelPart(int index, int pos)
    {
        // Вычисляем сложность
        int difficult = index + score;

        // Вычисляем координаты для нового levelPart
        float zPosition = pos * 50f;
        Vector3 position = new Vector3(0f, 0f, zPosition);

        // Создаем новый префаб levelPart на вычисленных координатах
        levelParts[index] = Instantiate(levelPartPrefab, position, Quaternion.identity);

        if (levelElementsInteractiveObject != null)
        {
            levelParts[index].transform.SetParent(levelElementsInteractiveObject.transform);
        }

        // Получаем ссылку на компонент EndlessLevelPartCreator созданного префаба
        EndlessLevelPartCreator levelPartCreator = levelParts[index].GetComponent<EndlessLevelPartCreator>();

        // Если найден компонент, передаем ему значение difficult
        if (levelPartCreator != null)
        {
            levelPartCreator.difficultMatrix = CalculateDifficulty(difficult);
            if (firstSpawn)
            {
                levelPartCreator.firstPart = true;
                firstSpawn = false;
            }
        }
    }
    void DestroyLevelPart(int index)
    {
        // Удаляем префаб levelPart по указанному индексу
        if (levelParts[index] != null)
        {
            Destroy(levelParts[index]);
        }
    }
    void UpdateGameBoundaryPosition(int index)
    {
        // Обновляем позицию объекта GameBoundary на позицию созданного префаба levelPart с смещением по оси Y на -4
        Vector3 newPosition = levelParts[index].transform.position;
        newPosition.y -= 4f;
        gameBoundaryObject.transform.position = newPosition;
    }
    /// <summary>
    /// Создает матрицу сложности и вычисляет параметры генерируемого сегмента уровня
    /// </summary>
    /// <param name="difficult">Базисная сложность игры</param>
    public int[,] CalculateDifficulty(int difficult)
    {
        int[,] difficultMatrix = new int[3, 2];

        int[] maxValues = { 5, 5, 5, 5, 5, 5 }; // Максимальные значения для каждого измерения массива (для значений с расстояниями между колоннами)

        if (difficult >= 0 && difficult <= 5) //смещение пути
        {
            difficultMatrix[0, 0] = difficult; // posColumnX

            // Остальные значения оставляем нулевыми
            difficultMatrix[1, 0] = 0; // posColumnZ
            difficultMatrix[2, 0] = 0; // posColumnY
            difficultMatrix[0, 1] = 0; // slopeColumn
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 6 && difficult <= 10) //смещение пути + увеличение разрыва
        {
            int randomPosX = Random.Range(1, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // Уменьшаем difficult на значение randomPosX

            int randomPosZ = Random.Range(0, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ

            // Остальные значения оставляем нулевыми
            difficultMatrix[2, 0] = 0; // posColumnY
            difficultMatrix[0, 1] = 0; // slopeColumn
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 11 && difficult <= 15) //смещение пути + увеличение разрыва + изменение высоты
        {
            int randomPosX = Random.Range(2, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // Уменьшаем difficult на значение randomPosX

            int randomPosZ = Random.Range(1, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // Уменьшаем difficult на значение randomPosZ

            int randomPosY = Random.Range(0, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY

            // Остальные значения оставляем нулевыми
            difficultMatrix[0, 1] = 0; // slopeColumn
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 16 && difficult <= 20) //полный набор первой степени + наклон колонны
        {
            int randomPosX = Random.Range(3, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // Уменьшаем difficult на значение randomPosX

            int randomPosZ = Random.Range(2, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // Уменьшаем difficult на значение randomPosZ

            int randomPosY = Random.Range(1, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY
            difficult -= randomPosY; // Уменьшаем difficult на значение randomPosY

            int randomSlope = Random.Range(0, Mathf.Min(maxValues[3], difficult + 1));
            difficultMatrix[0, 1] = randomSlope; // slopeColumn

            // Остальные значения оставляем нулевыми
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 21 && difficult <= 25) //полный набор первой степени + наклон колонны + качение колонны
        {
            int randomPosX = Random.Range(3, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // Уменьшаем difficult на значение randomPosX

            int randomPosZ = Random.Range(3, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // Уменьшаем difficult на значение randomPosZ

            int randomPosY = Random.Range(2, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY
            difficult -= randomPosY; // Уменьшаем difficult на значение randomPosY

            int randomSlope = Random.Range(1, Mathf.Min(maxValues[3], difficult + 1));
            difficultMatrix[0, 1] = randomSlope; // slopeColumn
            difficult -= randomSlope; // Уменьшаем difficult на значение randomSlope

            int randomSwing = Random.Range(0, Mathf.Min(maxValues[4], difficult + 1));
            difficultMatrix[1, 1] = randomSwing; // swingColumn

            // Остальные значения оставляем нулевыми
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult > 25) //полная сложность (с падающими колоннами)
        {
            int randomPosX = Random.Range(3, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // Уменьшаем difficult на значение randomPosX

            int randomPosZ = Random.Range(3, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // Уменьшаем difficult на значение randomPosZ

            int randomPosY = Random.Range(3, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY
            difficult -= randomPosY; // Уменьшаем difficult на значение randomPosY

            int randomSlope = Random.Range(2, Mathf.Min(maxValues[3], difficult + 1));
            difficultMatrix[0, 1] = randomSlope; // slopeColumn
            difficult -= randomSlope; // Уменьшаем difficult на значение randomSlope

            int randomSwing = Random.Range(1, Mathf.Min(maxValues[4], difficult + 1));
            difficultMatrix[1, 1] = randomSwing; // swingColumn
            difficult -= randomSwing; // Уменьшаем difficult на значение randomSwing

            int randomFalling = Random.Range(0, Mathf.Min(maxValues[5], difficult + 1));
            difficultMatrix[2, 1] = randomFalling; // fallingColumn
        }
        return difficultMatrix;
    }
}