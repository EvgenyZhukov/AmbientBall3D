using UnityEngine;

public class ActiveEndlessColumn : MonoBehaviour
{
    // Уровни сложности свойств колонн (получаем из генератора участка уровня)
    public int slopeColumn; // наклон колонны
    public int swingColumn; // качение колонны
    public int fallingColumn; // падающая колонна

    // Переменные для качения
    float linearPendulum;
    public bool activeX;
    public float amplitudoX;
    [Range(0f, 1f)]
    public float periodX;
    public bool activeY;
    public float amplitudoY;
    [Range(0f, 1f)]
    public float periodY;

    //Переменные для падения колонны
    public float timer = 0f;        // Таймер
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
    /// Наклоняет колонну при ее создании
    /// </summary>
    void SlopeInitiation(int slopeColumnLocal)
    {
        float randomProbability = Random.Range(0f, 1f); // Случайное число от 0 до 1

        if (randomProbability <= ProbabilityOperator(slopeColumnLocal, 1.4f))
        {
            int rotate_coef = 2;

            // Выполняем поворот
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
    /// Назначает свойства качения колонны
    /// </summary>
    void PendulumInitiation(int swingColumnLocal)
    {
        float randomProbability = Random.Range(0f, 1f); // Случайное число от 0 до 1

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
    /// Вычисляет амплитуду качения
    /// </summary>
    /// <param name="swingColumnLocal"></param>
    float SwingAmplitudo(int swingColumnLocal)
    {
        float swingAmplitudo = swingColumnLocal / 2 * ChoiceDirection();
        return swingAmplitudo;
    }

    /// <summary>
    /// Вычисляет период качения
    /// </summary>
    /// <param name="swingColumnLocal"></param>
    float SwingPeriod(int swingColumnLocal)
    {
        float swingPower = Random.Range(0.4f, 0.8f) + swingColumnLocal * 0.1f;
        return swingPower;
    }

        /// <summary>
        /// Назначает свойства падения колонны
        /// </summary>
        void FallInitiation(int fallingColumnLocal)
    {
        float randomProbability = Random.Range(0f, 1f); // Случайное число от 0 до 1

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
    /// Таймер для обратного отсчета падения колонны
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
    /// Определяет направление (возвращает 1 или -1)
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
    /// Определяет, является ли колонна активной, с определенной вероятностью
    /// </summary>
    /// <param name="columnProperties">Параметр - свойство колонны (slopeColumn, swingColumn, fallingColumn)</param>
    /// <returns></returns>
    float ProbabilityOperator(int columnProperties, float coef)
    {
        float executionProbability = 0f; // Вероятность выполнения метода

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

    #region Логика качения по принципу маятника
    /// <summary>
    /// Двигает колонну - анимация качения
    /// </summary>
    void TransformPositionXY()
    {
        transform.position = transform.TransformPoint(activeX ? Pendulum(periodX, amplitudoX) : 0.0f,
                                                        activeY ? Pendulum(periodY, amplitudoY) : 0.0f,
                                                        0.0f);
    }
    /// <summary>
    /// Маятник (основано на синусоиде)
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
