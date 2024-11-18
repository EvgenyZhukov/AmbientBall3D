using UnityEngine;

public class ActiveEndlessColumn : MonoBehaviour
{
    // ������ ��������� ������� ������ (�������� �� ���������� ������� ������)
    public int slopeColumn; // ������ �������
    public int swingColumn; // ������� �������
    public int fallingColumn; // �������� �������

    // ���������� ��� �������
    float linearPendulum;
    public bool activeX;
    public float amplitudoX;
    [Range(0f, 1f)]
    public float periodX;
    public bool activeY;
    public float amplitudoY;
    [Range(0f, 1f)]
    public float periodY;

    //���������� ��� ������� �������
    public float timer = 0f;        // ������
    public float timerLimit = 0f;
    public bool activated = false;
    public Rigidbody block;

    public AudioSource columnSound;
    bool soundLocker = false;

    void Start()
    {
        if (slopeColumn != 0)
        {
            SlopeInitiation(slopeColumn);
        }
        if (slopeColumn == 0 && swingColumn != 0)
        {
            PendulumInitiation(swingColumn);
        }
        if (slopeColumn == 0 && swingColumn == 0 && fallingColumn != 0)
        {
            FallInitiation(fallingColumn);
        }
    }

    void FixedUpdate()
    {
        TransformPositionXY();
    }

    void Update()
    {
        if (activated && fallingColumn != 0)
        {
            FallTimer();
        }
    }

    /// <summary>
    /// ��������� ������� ��� �� ��������
    /// </summary>
    void SlopeInitiation(int slopeColumnLocal)
    {
        float randomProbability = Random.Range(0f, 1f); // ��������� ����� �� 0 �� 1

        if (randomProbability <= ProbabilityOperator(slopeColumnLocal, 1.4f))
        {
            int rotate_coef = 2;

            // ��������� �������
            if (slopeColumnLocal <= 3)
            {
                transform.Rotate(slopeColumnLocal * rotate_coef * ChoiceDirection(), 0f, 0f);
            }
            else
            {
                transform.Rotate(slopeColumnLocal * rotate_coef * ChoiceDirection(), 0f, slopeColumnLocal * rotate_coef * ChoiceDirection());
            }
        }
        else
        {
            slopeColumn = 0;
        }
    }

    /// <summary>
    /// ��������� �������� ������� �������
    /// </summary>
    void PendulumInitiation(int swingColumnLocal)
    {
        float randomProbability = Random.Range(0f, 1f); // ��������� ����� �� 0 �� 1

        if (randomProbability <= ProbabilityOperator(swingColumnLocal, 2.4f))
        {

            int swingVariant = Random.Range(0, 3);

            switch (swingVariant)
            {
                case 0:
                    activeX = true;
                    amplitudoX = SwingAmplitudo(swingColumnLocal);
                    periodX = SwingPeriod(swingColumnLocal);
                    break;
                case 1:
                    activeY = true;
                    amplitudoY = SwingAmplitudo(swingColumnLocal);
                    periodY = SwingPeriod(swingColumnLocal);
                    break;
                case 2:
                    activeX = true;
                    amplitudoX = SwingAmplitudo(swingColumnLocal);
                    periodX = SwingPeriod(swingColumnLocal);
                    break;
                case 3:
                    activeY = true;
                    amplitudoY = SwingAmplitudo(swingColumnLocal);
                    periodY = SwingPeriod(swingColumnLocal);
                    break;
                default:
                    break;
            }
        }
        else
        {
            swingColumn = 0;
        }
    }

    /// <summary>
    /// ��������� ��������� �������
    /// </summary>
    /// <param name="swingColumnLocal"></param>
    float SwingAmplitudo(int swingColumnLocal)
    {
        float swingAmplitudo = swingColumnLocal / 2 * ChoiceDirection();
        return swingAmplitudo;
    }

    /// <summary>
    /// ��������� ������ �������
    /// </summary>
    /// <param name="swingColumnLocal"></param>
    float SwingPeriod(int swingColumnLocal)
    {
        float swingPower = Random.Range(0.4f, 0.8f) + swingColumnLocal * 0.1f;
        return swingPower;
    }

        /// <summary>
        /// ��������� �������� ������� �������
        /// </summary>
        void FallInitiation(int fallingColumnLocal)
    {
        float randomProbability = Random.Range(0f, 1f); // ��������� ����� �� 0 �� 1

        if (randomProbability <= ProbabilityOperator(fallingColumnLocal, 4f))
        {
            switch (fallingColumnLocal)
            {
                case 0:
                    break;
                case 1:
                    timerLimit = 4f;
                    break;
                case 2:
                    timerLimit = 3.5f;
                    break;
                case 3:
                    timerLimit = 3f;
                    break;
                case 4:
                    timerLimit = 2.5f;
                    break;
                default:
                    timerLimit = 2f;
                    break;
            }
        }
    }
    /// <summary>
    /// ������ ��� ��������� ������� ������� �������
    /// </summary>
    void FallTimer()
    {
        timer += 1f * Time.deltaTime;

        if (timer < timerLimit)
        {
            activated = false;
            block.isKinematic = false;

            if (!soundLocker)
            {
                columnSound.pitch = Random.Range(0.9f, 1f);
                columnSound.Play();
                soundLocker = true;
            }
        }
    }
    /// <summary>
    /// ���������� ����������� (���������� 1 ��� -1)
    /// </summary>
    /// <returns></returns>
    int ChoiceDirection()
    {
        int number = Random.Range(0, 2);
        if (number == 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
    /// <summary>
    /// ����������, �������� �� ������� ��������, � ������������ ������������
    /// </summary>
    /// <param name="columnProperties">�������� - �������� ������� (slopeColumn, swingColumn, fallingColumn)</param>
    /// <returns></returns>
    float ProbabilityOperator(int columnProperties, float coef)
    {
        float executionProbability = 0f; // ����������� ���������� ������

        switch (columnProperties)
        {
            case 0:
                executionProbability = 0.0f;
                break;
            case 1:
                executionProbability = 0.05f * coef;
                break;
            case 2:
                executionProbability = 0.1f * coef;
                break;
            case 3:
                executionProbability = 0.15f * coef;
                break;
            case 4:
                executionProbability = 0.20f * coef;
                break;
            case 5:
                executionProbability = 0.25f * coef;
                break;
            default:
                executionProbability = 0.25f * coef;
                break;
        }
        return executionProbability;
    }

    #region ������ ������� �� �������� ��������
    /// <summary>
    /// ������� ������� - �������� �������
    /// </summary>
    void TransformPositionXY()
    {
        transform.position = transform.TransformPoint(activeX ? Pendulum(periodX, amplitudoX) : 0.0f,
                                                        activeY ? Pendulum(periodY, amplitudoY) : 0.0f,
                                                        0.0f);
    }
    /// <summary>
    /// ������� (�������� �� ���������)
    /// </summary>
    /// <param name="period"></param>
    /// <param name="amplitudo"></param>
    /// <returns></returns>
    float Pendulum(float period, float amplitudo)
    {
        linearPendulum = Mathf.Sin(Time.timeSinceLevelLoad * period) * amplitudo * period * Time.deltaTime;
        return linearPendulum;
    }
    #endregion
}
