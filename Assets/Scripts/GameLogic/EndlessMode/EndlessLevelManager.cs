using PlayerPrefsSavingMethods;
using UnityEngine;

public class EndlessLevelManager : MonoBehaviour
{
    public int score = 0;
    public int indexLevelPart = 1;
    public bool scoreUp = false;

    public GameObject levelPartPrefab;
    public GameObject gameBoundaryObject; // ������ �� ������ "GameBoundary"

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
        levelParts = new GameObject[3]; // ������� ������ ��� ������� ��� ������
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
            UpdateGameBoundaryPosition(indexLevelPart); // ��������� ������� ������� GameBoundary

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
        // ��������� ���������
        int difficult = index + score;

        // ��������� ���������� ��� ������ levelPart
        float zPosition = pos * 50f;
        Vector3 position = new Vector3(0f, 0f, zPosition);

        // ������� ����� ������ levelPart �� ����������� �����������
        levelParts[index] = Instantiate(levelPartPrefab, position, Quaternion.identity);

        if (levelElementsInteractiveObject != null)
        {
            levelParts[index].transform.SetParent(levelElementsInteractiveObject.transform);
        }

        // �������� ������ �� ��������� EndlessLevelPartCreator ���������� �������
        EndlessLevelPartCreator levelPartCreator = levelParts[index].GetComponent<EndlessLevelPartCreator>();

        // ���� ������ ���������, �������� ��� �������� difficult
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
        // ������� ������ levelPart �� ���������� �������
        if (levelParts[index] != null)
        {
            Destroy(levelParts[index]);
        }
    }
    void UpdateGameBoundaryPosition(int index)
    {
        // ��������� ������� ������� GameBoundary �� ������� ���������� ������� levelPart � ��������� �� ��� Y �� -4
        Vector3 newPosition = levelParts[index].transform.position;
        newPosition.y -= 4f;
        gameBoundaryObject.transform.position = newPosition;
    }
    /// <summary>
    /// ������� ������� ��������� � ��������� ��������� ������������� �������� ������
    /// </summary>
    /// <param name="difficult">�������� ��������� ����</param>
    public int[,] CalculateDifficulty(int difficult)
    {
        int[,] difficultMatrix = new int[3, 2];

        int[] maxValues = { 5, 5, 5, 5, 5, 5 }; // ������������ �������� ��� ������� ��������� ������� (��� �������� � ������������ ����� ���������)

        if (difficult >= 0 && difficult <= 5) //�������� ����
        {
            difficultMatrix[0, 0] = difficult; // posColumnX

            // ��������� �������� ��������� ��������
            difficultMatrix[1, 0] = 0; // posColumnZ
            difficultMatrix[2, 0] = 0; // posColumnY
            difficultMatrix[0, 1] = 0; // slopeColumn
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 6 && difficult <= 10) //�������� ���� + ���������� �������
        {
            int randomPosX = Random.Range(1, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // ��������� difficult �� �������� randomPosX

            int randomPosZ = Random.Range(0, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ

            // ��������� �������� ��������� ��������
            difficultMatrix[2, 0] = 0; // posColumnY
            difficultMatrix[0, 1] = 0; // slopeColumn
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 11 && difficult <= 15) //�������� ���� + ���������� ������� + ��������� ������
        {
            int randomPosX = Random.Range(2, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // ��������� difficult �� �������� randomPosX

            int randomPosZ = Random.Range(1, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // ��������� difficult �� �������� randomPosZ

            int randomPosY = Random.Range(0, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY

            // ��������� �������� ��������� ��������
            difficultMatrix[0, 1] = 0; // slopeColumn
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 16 && difficult <= 20) //������ ����� ������ ������� + ������ �������
        {
            int randomPosX = Random.Range(3, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // ��������� difficult �� �������� randomPosX

            int randomPosZ = Random.Range(2, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // ��������� difficult �� �������� randomPosZ

            int randomPosY = Random.Range(1, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY
            difficult -= randomPosY; // ��������� difficult �� �������� randomPosY

            int randomSlope = Random.Range(0, Mathf.Min(maxValues[3], difficult + 1));
            difficultMatrix[0, 1] = randomSlope; // slopeColumn

            // ��������� �������� ��������� ��������
            difficultMatrix[1, 1] = 0; // swingColumn
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult >= 21 && difficult <= 25) //������ ����� ������ ������� + ������ ������� + ������� �������
        {
            int randomPosX = Random.Range(3, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // ��������� difficult �� �������� randomPosX

            int randomPosZ = Random.Range(3, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // ��������� difficult �� �������� randomPosZ

            int randomPosY = Random.Range(2, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY
            difficult -= randomPosY; // ��������� difficult �� �������� randomPosY

            int randomSlope = Random.Range(1, Mathf.Min(maxValues[3], difficult + 1));
            difficultMatrix[0, 1] = randomSlope; // slopeColumn
            difficult -= randomSlope; // ��������� difficult �� �������� randomSlope

            int randomSwing = Random.Range(0, Mathf.Min(maxValues[4], difficult + 1));
            difficultMatrix[1, 1] = randomSwing; // swingColumn

            // ��������� �������� ��������� ��������
            difficultMatrix[2, 1] = 0; // fallingColumn
        }
        else if (difficult > 25) //������ ��������� (� ��������� ���������)
        {
            int randomPosX = Random.Range(3, Mathf.Min(maxValues[0], difficult + 1));
            difficultMatrix[0, 0] = randomPosX; // posColumnX
            difficult -= randomPosX; // ��������� difficult �� �������� randomPosX

            int randomPosZ = Random.Range(3, Mathf.Min(maxValues[1], difficult + 1));
            difficultMatrix[1, 0] = randomPosZ; // posColumnZ
            difficult -= randomPosZ; // ��������� difficult �� �������� randomPosZ

            int randomPosY = Random.Range(3, Mathf.Min(maxValues[2], difficult + 1));
            difficultMatrix[2, 0] = randomPosY; // posColumnY
            difficult -= randomPosY; // ��������� difficult �� �������� randomPosY

            int randomSlope = Random.Range(2, Mathf.Min(maxValues[3], difficult + 1));
            difficultMatrix[0, 1] = randomSlope; // slopeColumn
            difficult -= randomSlope; // ��������� difficult �� �������� randomSlope

            int randomSwing = Random.Range(1, Mathf.Min(maxValues[4], difficult + 1));
            difficultMatrix[1, 1] = randomSwing; // swingColumn
            difficult -= randomSwing; // ��������� difficult �� �������� randomSwing

            int randomFalling = Random.Range(0, Mathf.Min(maxValues[5], difficult + 1));
            difficultMatrix[2, 1] = randomFalling; // fallingColumn
        }
        return difficultMatrix;
    }
}