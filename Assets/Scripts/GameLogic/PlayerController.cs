using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefsSavingMethods;

/// <summary>
/// Осуществляет управление игроком (перемещение и изменение свойств игрока)
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Скрипты")]
    public CameraController cameraController;
    public ScriptUI scriptUI;
    public AudioScript audioScript;
    [Header("Внешние объекты")]
    public Transform mediator;       // Трансформ объекта посредника
    public GameObject cam;
    [Header("Управление игроком")]
    public Rigidbody playerRb;       // Переменная объекта (игрока) которым управляем
    [SerializeField] private float moveVertical;
    [SerializeField] private float moveHorizontal;
    private Vector3 alignment;       // Rotation объекта посредника
    private const float DIAGONAL_COEF = 0.7071f;
    private float accelerometerSensivity = 2.5f;
    public float correct;            // Величина калибровки акселерометра при смене положения
    [SerializeField] public bool grounded = true;     // Определяет, стоит ли игрок на земле
    [SerializeField] private bool jumpActivated = false;
    [SerializeField] private float jumpTimer = 0f;
    [SerializeField] private bool fallActivated = false;
    [SerializeField] private float fallTimer = 0f;
    private bool hitSoundActivated = false;
    private float hitSoundTimer = 0f;
    private float afterJumpDelay = 0.35f;
    private float afterFallDelay = 0.25f;
    private float afterHitDelay = 0.25f;
    [SerializeField] private float speed;
    private float moveDeadZone = 0.125f;
    private float verticalMinusCorrect = 1.7f;
    [Header("Изменение свойств игрока")]
    [SerializeField] private bool propertiesChangeIteration = false;
    [SerializeField] private float jumpForce;          // Сила прыжка, текущая
    [SerializeField] private float weight;             // Масса игрока, текущая
    [SerializeField] public float moveForce;          // Сила движения, текущая
    private float standartMoveForce = 500f;      // Сила движения, стандарт
    private float doubleMoveForce = 900f;        // Увеличенная сила перемещения игрока
    private float bigWeightMoveForce = 3000f;    // Сила движения, при высоком весе
    private float standartWeight = 1f;           // Масса игрока, стандарт
    private float bigWeight = 10f;                // Увеличенная масса игрока
    private float standartJumpForce = 450f;      // Сила прыжка, стандарт
    private float doubleJumpForce = 750f;        // Сила прыжка, увеличеная
    private float bigWeightJumpForce = 5000f;     // Сила прыжка, при высоком весе
    public float moveForceInJump = 125f;        // Сила перемещения игрока в прыжке
    public float moveForceInJumpBigWeight = 700f;
    private bool changePropertiesButtonActive = true;
    public float moveForceInElevator = 200f;
    /// <summary>
    /// 0 - стандартный/белый, 1 - скоростной/красный, 2 - прыгучий/желтый, 3 - тяжелый/зеленый, 4 - магнитный/синий
    /// </summary>
    public int playerForm;
    [Header("Материалы линий, различные цвета")]
    public Material ballMaterialWhite;
    public Material ballMaterialRed;
    public Material ballMaterialBlue;
    public Material ballMaterialYellow;
    public Material ballMaterialGreen;
    [Header("Переменные объекты, линии на шаре и шар-игрок")]
    public GameObject line1;
    public GameObject line2;
    public GameObject line3;
    [Header("Статусы изменений и переменные для этапа трансформации")]
    [SerializeField] private bool materialChangeIteration = false;
    [SerializeField] private bool increaseForm = false;
    [SerializeField] private bool decreaseForm = false;
    private float x; // Масштаб объекта линии для изменения ее ширины
    private float formSpeed = 3f; //Скорость изменения размера линии
    public bool magneticForm = false;
    public int sceneNumber;
    public bool preSetProperties;
    [Header("Анимация телепортации")]
    public bool animationDecreaseStage;
    public bool animationIncreaseStage;
    public float sizeChangeSpeed = 3f;
    public float playerScale = 1f;

    void Start()
    {
        audioScript = FindObjectOfType<AudioScript>();
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PropertiesStart();
        scriptUI.ResetAccelerometerZero();
    }
    void Update()
    {
        BallTransformator();
        ColorChange();
        PropertiesChange();
        JumpAndFallTimer();
        RollingSound();
        //TeleportationAnimation();
    }
    void FixedUpdate()
    {
            MoveControl();
    }
    #region Изменение свойств и цвета линий шара, анимация телепортации
    /// <summary>
    /// Анимирует изменение цвета полос на шаре
    /// </summary>
    private void BallTransformator()
    {
        switch ((increaseForm, decreaseForm))
        {
            case (true, false):
                x = line1.transform.localScale.x;
                line1.transform.localScale = new Vector3(x + formSpeed * Time.deltaTime, 1.02f, 1.02f);
                line2.transform.localScale = line1.transform.localScale;
                line3.transform.localScale = line1.transform.localScale;
                if (x >= 0.9f)
                {
                    increaseForm = false;
                    decreaseForm = true;
                    ChangeStatus();
                }
                break;
            case (false, true):
                x = line1.transform.localScale.x;
                line1.transform.localScale = new Vector3(x - formSpeed * Time.deltaTime, 1.02f, 1.02f);
                line2.transform.localScale = line1.transform.localScale;
                line3.transform.localScale = line1.transform.localScale;
                if (x <= 0.3f)
                {
                    decreaseForm = false;
                    line1.transform.localScale = new Vector3(0.3f, 1.02f, 1.02f);
                    line2.transform.localScale = new Vector3(0.3f, 1.02f, 1.02f);
                    line3.transform.localScale = new Vector3(0.3f, 1.02f, 1.02f);
                    changePropertiesButtonActive = true;
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Меняет цвет полос на шаре
    /// </summary>
    private void ColorChange()
    {
        if (materialChangeIteration)
        {
            switch (playerForm)
            {
                case 0:
                    line1.GetComponent<MeshRenderer>().material = ballMaterialWhite;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialWhite;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialWhite;
                    materialChangeIteration = false;
                    break;
                case 1:
                    line1.GetComponent<MeshRenderer>().material = ballMaterialRed;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialRed;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialRed;
                    materialChangeIteration = false;
                    break;
                case 2:
                    line1.GetComponent<MeshRenderer>().material = ballMaterialYellow;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialYellow;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialYellow;
                    materialChangeIteration = false;
                    break;
                case 3:
                    line1.GetComponent<MeshRenderer>().material = ballMaterialGreen;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialGreen;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialGreen;
                    materialChangeIteration = false;
                    break;
                case 4:
                    line1.GetComponent<MeshRenderer>().material = ballMaterialBlue;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialBlue;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialBlue;
                    materialChangeIteration = false;
                    break;
            }
        }
    }
    /// <summary>
    /// Изменяет свойства игрока
    /// </summary>
    private void PropertiesChange()
    {
        if (propertiesChangeIteration)
        {
            switch (playerForm)
            {
                case 0:
                    SaveLoadData.SetPropertiesFormNum(playerForm);
                    if (grounded)
                    {
                        moveForce = standartMoveForce;
                    }
                    else
                    {
                        moveForce = moveForceInJump;
                    }
                    jumpForce = standartJumpForce;
                    ChangeMass(standartWeight);
                    magneticForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 1:
                    SaveLoadData.SetPropertiesFormNum(playerForm);
                    if (grounded)
                    {
                        moveForce = doubleMoveForce;
                    }
                    else
                    {
                        moveForce = moveForceInJump;
                    }
                    jumpForce = standartJumpForce;
                    ChangeMass(standartWeight);
                    magneticForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 2:
                    SaveLoadData.SetPropertiesFormNum(playerForm);
                    if (grounded)
                    {
                        moveForce = standartMoveForce;
                    }
                    else
                    {
                        moveForce = moveForceInJump;
                    }
                    jumpForce = doubleJumpForce;
                    ChangeMass(standartWeight);
                    magneticForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 3:
                    SaveLoadData.SetPropertiesFormNum(playerForm);
                    if (grounded)
                    {
                        moveForce = bigWeightMoveForce;
                    }
                    else
                    {
                        moveForce = moveForceInJumpBigWeight;
                    }
                    jumpForce = bigWeightJumpForce;
                    ChangeMass(bigWeight);
                    magneticForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 4:
                    SaveLoadData.SetPropertiesFormNum(playerForm);
                    if (grounded)
                    {
                        moveForce = standartMoveForce;
                    }
                    else
                    {
                        moveForce = moveForceInJump;
                    }
                    jumpForce = standartJumpForce;
                    ChangeMass(standartWeight);
                    magneticForm = true;
                    propertiesChangeIteration = false;
                    break;
            }
        }
    }
    /// <summary>
    /// Присваивает телу игрока массу согласно текущим свойствам
    /// </summary>
    private void ChangeMass(float weight)
    {
        playerRb.GetComponent<Rigidbody>().mass = weight;
    }
    /// <summary>
    /// Присваивает стандартные свойства при старте
    /// </summary>
    private void PropertiesStart()
    {
        if (preSetProperties)
        {
            switch (sceneNumber)
            {
                case 1:
                    PlayerForm_0();
                    SaveLoadData.SetPropertiesFormNum(0);
                    break;
                case 2:
                    PlayerForm_0();
                    SaveLoadData.SetPropertiesFormNum(0);
                    break;
                case 4:
                    PlayerForm_2();
                    SaveLoadData.SetPropertiesFormNum(2);
                    break;
                case 5:
                    PlayerForm_3();
                    SaveLoadData.SetPropertiesFormNum(3);
                    break;
                default:
                    PlayerForm_1();
                    SaveLoadData.SetPropertiesFormNum(1);
                    break;
            }
        }
        else
        {
            switch (SaveLoadData.GetPropertiesFormNum())
            {
                case 0:
                    PlayerForm_0();
                    break;
                case 1:
                    PlayerForm_1();
                    break;
                case 2:
                    PlayerForm_2();
                    break;
                default:
                    PlayerForm_3();
                    break;
            }
        }
    }
    private void PlayerForm_0()
    {
        playerForm = 0;
        line1.GetComponent<MeshRenderer>().material = ballMaterialWhite;
        line2.GetComponent<MeshRenderer>().material = ballMaterialWhite;
        line3.GetComponent<MeshRenderer>().material = ballMaterialWhite;
        jumpForce = standartJumpForce;
        moveForce = standartMoveForce;
        ChangeMass(standartWeight);
    }
    private void PlayerForm_1()
    {
        playerForm = 1;
        line1.GetComponent<MeshRenderer>().material = ballMaterialRed;
        line2.GetComponent<MeshRenderer>().material = ballMaterialRed;
        line3.GetComponent<MeshRenderer>().material = ballMaterialRed;
        jumpForce = standartJumpForce;
        moveForce = doubleMoveForce;
        ChangeMass(standartWeight);
    }
    private void PlayerForm_2()
    {
        playerForm = 2;
        line1.GetComponent<MeshRenderer>().material = ballMaterialYellow;
        line2.GetComponent<MeshRenderer>().material = ballMaterialYellow;
        line3.GetComponent<MeshRenderer>().material = ballMaterialYellow;
        jumpForce = doubleJumpForce;
        moveForce = standartMoveForce;
        ChangeMass(standartWeight);
    }
    private void PlayerForm_3()
    {
        playerForm = 3;
        line1.GetComponent<MeshRenderer>().material = ballMaterialGreen;
        line2.GetComponent<MeshRenderer>().material = ballMaterialGreen;
        line3.GetComponent<MeshRenderer>().material = ballMaterialGreen;
        jumpForce = bigWeightJumpForce;
        moveForce = bigWeightMoveForce;
        ChangeMass(bigWeight);
    }
    void TeleportationAnimation()
    {
        if (animationDecreaseStage)
        {
            scriptUI.DeactivateUI();
            if (playerScale > 0)
            {
                playerScale -= sizeChangeSpeed * Time.deltaTime;
                transform.localScale = new Vector3(playerScale, playerScale, playerScale);
            }
            else if (playerScale <= 0)
            {
                playerScale = 0;
                transform.localScale = new Vector3(playerScale, playerScale, playerScale);
                animationDecreaseStage = false;
                animationIncreaseStage = true;
            }
        }
        if (animationIncreaseStage)
        {
            if (playerScale < 1)
            {
                playerScale += sizeChangeSpeed * Time.deltaTime;
                transform.localScale = new Vector3(playerScale, playerScale, playerScale);
            }
            else if (playerScale >= 1)
            {
                playerScale = 1;
                transform.localScale = new Vector3(playerScale, playerScale, playerScale);
                animationIncreaseStage = false;
                scriptUI.ActivateUI();
            }
        }
    }
    #endregion

    #region Управление игроком
    /// <summary>
    /// Включает флаги изменения материала и свойств
    /// </summary>
    private void ChangeStatus()
    {
        materialChangeIteration = true;
        propertiesChangeIteration = true;
    }
    /// <summary>
    /// Управление передвижением игрока
    /// </summary>
    private void MoveControl()
    {
        // Без этого костыля скорость движения шарика зависила от угла обзора камеры

        alignment = cam.transform.rotation.eulerAngles; // Получает значение поворота камеры
        alignment.x = alignment.x + cameraController.Y;  // Выравнивает поворот объета перпендикулярно горизонтали

        mediator.position = cameraController.target.position;    // Объект посредник получает позицию шара-игрока
        mediator.rotation = Quaternion.Euler(alignment);    // Объект посредник получает нивелированый поворот камеры

        if (!scriptUI.accelerometerActive)
        {
            moveVertical = scriptUI.JoystickMove.Vertical;
            moveHorizontal = scriptUI.JoystickMove.Horizontal;
            MoveInputLimiter();
        }

        if (scriptUI.accelerometerActive && !scriptUI.accelerometerLocker)
        {
            //Debug.Log("acceleration.y = " + Input.acceleration.y + "; acceleration.y - correct = " + (Input.acceleration.y - correct) + "; acceleration.x = " + Input.acceleration.x);
            if (moveVertical >= 0)
            {
                moveVertical = (Input.acceleration.y - correct) * accelerometerSensivity;
            }
            if (moveVertical < 0)
            {
                moveVertical = (Input.acceleration.y - correct) * verticalMinusCorrect * accelerometerSensivity;
            }
            moveHorizontal = Input.acceleration.x * accelerometerSensivity;
            //Debug.Log("moveVertical = " + moveVertical + "; moveHorizontal = " + moveHorizontal);
            MoveInputLimiter();
        }
        if (!scriptUI.playerAddForceLocker)
        {
            if (moveHorizontal <= -moveDeadZone || moveHorizontal >= moveDeadZone)
            {
                playerRb.AddForce(mediator.transform.right * moveHorizontal * moveForce * Time.fixedDeltaTime);
            }
            if (moveVertical <= -moveDeadZone || moveVertical >= moveDeadZone)
            {
                playerRb.AddForce(mediator.transform.forward * moveVertical * moveForce * Time.fixedDeltaTime);
            }
        }
    }
    void MoveInputLimiter()
    {
        if (moveVertical < -1)
        {
            moveVertical = -1;
        }
        else if (moveVertical > 1)
        {
            moveVertical = 1;
        }

        if (moveHorizontal < -1)
        {
            moveHorizontal = -1;
        }
        else if (moveHorizontal > 1)
        {
            moveHorizontal = 1;
        }
    }
    /// <summary>
    /// Кнопка прыжка
    /// </summary>
    public void JumpButton()
    {
        if (grounded && !jumpActivated)
        {
            audioScript.PlayJumpSound();
            playerRb.AddForce(Vector3.up * jumpForce);
            grounded = false;
            jumpActivated = true;
        }
        //else Debug.Log("you not grounded!");
    }
    public void JumpAndFallTimer()
    {
        if (jumpActivated)
        {
            if (jumpTimer < afterJumpDelay)
            {
                jumpTimer += 1f * Time.deltaTime;
            }
            else if (jumpTimer >= afterJumpDelay)
            {
                jumpActivated = false;
                jumpTimer = 0;
            }
        }
        if (fallActivated)
        {
            if (fallTimer < afterFallDelay)
            {
                fallTimer += 1f * Time.deltaTime;
            }
            else if (fallTimer >= afterFallDelay)
            {
                grounded = false;
                fallActivated = false;
                fallTimer = 0;
            }
        }
        if (hitSoundActivated)
        {
            if (hitSoundTimer < afterHitDelay)
            {
                hitSoundTimer += 1f * Time.deltaTime;
            }
            else if (hitSoundTimer >= afterHitDelay)
            {
                hitSoundTimer = 0;
                hitSoundActivated = false;
            }
        }
    }
    /// <summary>
    /// Кнопка изменения свойств игрока
    /// </summary>
    public void ChangePropertiesButton()
    {
        if (changePropertiesButtonActive)
        {
            audioScript.transformSound.Play();
            if (sceneNumber == 9)
            {
                if (playerForm == 1)
                {
                    playerForm = 2;
                    increaseForm = true;
                }
                else if (playerForm == 2)
                {
                    playerForm = 1;
                    increaseForm = true;
                }
            }
            else if (sceneNumber >= 5 && sceneNumber != 9)
            {
                if (playerForm < 3)
                {
                    playerForm += 1;
                    increaseForm = true;
                }
                else
                {
                    playerForm = 1;
                    increaseForm = true;
                }
            }
            else if (sceneNumber == 4)
            {
                if (playerForm < 2)
                {
                    playerForm += 1;
                    increaseForm = true;
                }
                else
                {
                    playerForm = 1;
                    increaseForm = true;
                }
            }
            else if (sceneNumber == 3)
            {
                if (playerForm < 1)
                {
                    playerForm += 1;
                    increaseForm = true;
                }
                else
                {
                    playerForm = 0;
                    increaseForm = true;
                }
            }
            else
            {
                playerForm = 0;
                increaseForm = true;
            }
            changePropertiesButtonActive = false;
        }
    }
    /// <summary>
    /// Приземление игрока
    /// </summary>
    /// <param name="other">Коллайдер поверхности земли</param>
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = true;
            fallActivated = false;
            fallTimer = 0;
            switch (playerForm)
            {
                case 0:
                    moveForce = standartMoveForce;
                    break;
                case 1:
                    moveForce = doubleMoveForce;
                    break;
                case 2:
                    moveForce = standartMoveForce;
                    break;
                case 3:
                    moveForce = bigWeightMoveForce;
                    break;
                case 4:
                    moveForce = standartMoveForce;
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// Переход в фазу прыжка игрока
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if (playerForm == 3)
            {
                moveForce = moveForceInJumpBigWeight;
            }
            else
            {
                moveForce = moveForceInJump;
            }
            fallActivated = true;
        }
    }
    #endregion
    #region Звуки
    void RollingSound()
    {
        speed = playerRb.velocity.magnitude;

        if (grounded == true && speed != 0)
        {
            switch (playerForm)
            {
                case 0:
                    audioScript.rollingSound.volume = Mathf.Clamp(speed / 8, 0.0f, 1f);
                    audioScript.rollingSound.pitch = Mathf.Clamp(speed / 22, 0.42f, 0.44f);
                    break;
                case 1:
                    audioScript.rollingSound.volume = Mathf.Clamp(speed / 8, 0.0f, 1f);
                    audioScript.rollingSound.pitch = Mathf.Clamp(speed / 22, 0.42f, 0.44f);
                    break;
                case 2:
                    audioScript.rollingSound.volume = Mathf.Clamp(speed / 8, 0.0f, 1f);
                    audioScript.rollingSound.pitch = Mathf.Clamp(speed / 22, 0.42f, 0.44f);
                    break;
                case 3:
                    audioScript.rollingSound.volume = Mathf.Clamp(speed / 2.5f, 0.0f, 1f);
                    audioScript.rollingSound.pitch = 0.42f;
                    break;
                case 4:
                    audioScript.rollingSound.volume = Mathf.Clamp(speed / 8, 0.0f, 1f);
                    audioScript.rollingSound.pitch = Mathf.Clamp(speed / 22, 0.42f, 0.44f);
                    break;
                default:
                    break;
            }
        }
        else
        {
            audioScript.rollingSound.volume = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hitSoundActivated)
        {
            if (other.CompareTag("Side")) // Воспроизводит звук удара о стенки колонн
            {
                //Debug.Log("Удар о колонну");
                if (speed > 2)
                {
                    audioScript.hitSound.volume = Mathf.Clamp(speed / 10, 0.0f, 1f);
                    audioScript.hitSound.Play();
                    hitSoundActivated = true;
                    //Debug.Log("hit sound volume = " + audioScript.hitSound.volume);
                }
            }
            else if (other.CompareTag("Ground") && !grounded) // Воспроизводит звук удара о "землю" т.е. вверх колонн
            {
                //Debug.Log("Удар о землю");
                audioScript.hitSound.volume = Mathf.Clamp(speed / 10, 0.0f, 1f);
                audioScript.hitSound.pitch = Random.Range(0.97f, 1f);
                audioScript.hitSound.Play();
                hitSoundActivated = true;
                //Debug.Log("hit sound volume = " + audioScript.hitSound.volume);
            }
        }
    }
    #endregion
}