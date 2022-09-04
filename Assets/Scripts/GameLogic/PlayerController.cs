using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Осуществляет управление игроком (перемещение и изменение свойств игрока)
/// </summary>
public class PlayerController : MonoBehaviour 
{
    [Header("Скрипты")]
    public CameraController cameraController;
    public ScriptUI scriptUI;
    [Header("Внешние объекты")]
    public Transform mediator;       // Трансформ объекта посредника
    public GameObject cam;
    [Header("Управление игроком")]
    public Rigidbody playerRb;       // Переменная объекта (игрока) которым управляем
    [SerializeField] private float moveVertical;
    [SerializeField] private float moveHorizontal;
    private Vector3 alignment;       // Rotation объекта посредника
    private const float DIAGONAL_COEF = 0.7071f;
    public float correct;            // Величина калибровки акселерометра при смене положения
    [SerializeField] private bool grounded = true;     // Определяет, стоит ли игрок на земле
    [Header("Изменение свойств игрока")]
    [SerializeField] private bool propertiesChangeIteration = false;
    [SerializeField] private float jumpForce;          // Сила прыжка, текущая
    [SerializeField] private float weight;             // Масса игрока, текущая
    [SerializeField] private float moveForce;          // Сила движения, текущая
    private float standartMoveForce = 500f;      // Сила движения, стандарт
    private float doubleMoveForce = 900f;        // Увеличенная сила перемещения игрока
    private float bigWeightMoveForce = 1000f;    // Сила движения, при высоком весе
    private float standartWeight = 1f;           // Масса игрока, стандарт
    private float bigWeight = 5f;                // Увеличенная масса игрока
    private float standartJumpForce = 450f;      // Сила прыжка, стандарт
    private float doubleJumpForce = 750f;        // Сила прыжка, увеличеная
    private float bigWeightJumpForce = 850f;     // Сила прыжка, при высоком весе
    private float moveForceInJump = 100f;        // Сила перемещения игрока в прыжке
    /// <summary>
    /// 0 - стандартный/белый, 1 - скоростной/красный, 2 - прыгучий/желтый, 3 - тяжелый/зеленый, 4 - магнитный/синий
    /// </summary>
    [SerializeField] private int playerForm;
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
    public bool magniteForm = false;
    public int sceneNumber;

    void Start ()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PropertiesStart();
    }
    void Update()
    {
        BallTransformator();
        ColorChange();
        PropertiesChange();
    }
    void FixedUpdate ()
	{
        MoveControl();
    }

    #region Изменение свойств и цвета линий шара
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
                    moveForce = standartMoveForce;
                    jumpForce = standartJumpForce;
                    ChangeMass(standartWeight);
                    magniteForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 1:
                    moveForce = doubleMoveForce;
                    jumpForce = standartJumpForce;
                    ChangeMass(standartWeight);
                    magniteForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 2:
                    moveForce = standartMoveForce;
                    jumpForce = doubleJumpForce;
                    ChangeMass(standartWeight);
                    magniteForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 3:
                    moveForce = bigWeightMoveForce;
                    jumpForce = bigWeightJumpForce;
                    ChangeMass(bigWeight);
                    magniteForm = false;
                    propertiesChangeIteration = false;
                    break;
                case 4:
                    moveForce = standartMoveForce;
                    jumpForce = standartJumpForce;
                    ChangeMass(standartWeight);
                    magniteForm = true;
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
        switch (sceneNumber)
        {
            case 3:
                line1.GetComponent<MeshRenderer>().material = ballMaterialRed;
                line2.GetComponent<MeshRenderer>().material = ballMaterialRed;
                line3.GetComponent<MeshRenderer>().material = ballMaterialRed;
                playerForm = 1;
                jumpForce = standartJumpForce;
                moveForce = doubleMoveForce;
                ChangeMass(standartWeight);
                break;
            default:
                line1.GetComponent<MeshRenderer>().material = ballMaterialWhite;
                line2.GetComponent<MeshRenderer>().material = ballMaterialWhite;
                line3.GetComponent<MeshRenderer>().material = ballMaterialWhite;
                playerForm = 0;
                jumpForce = standartJumpForce;
                moveForce = standartMoveForce;
                ChangeMass(standartWeight);
                break;
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
            
            
            playerRb.AddForce(mediator.transform.right * moveHorizontal * moveForce * Time.fixedDeltaTime);
            playerRb.AddForce(mediator.transform.forward * moveVertical * moveForce * Time.fixedDeltaTime);
            
        }

        if (scriptUI.accelerometerActive) 
        {

            moveVertical = (Input.acceleration.y - correct) * 2;
            moveHorizontal = Input.acceleration.x * 2;

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
    }
    /// <summary>
    /// Кнопка прыжка
    /// </summary>
    public void JumpButton()
    {
        if (grounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce);
            grounded = false;
        }
    }
    /// <summary>
    /// Кнопка изменения свойств игрока
    /// </summary>
    public void ChangePropertiesButton()
    {
        if (sceneNumber >= 7)
        {
            if (playerForm < 4)
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
        else if (sceneNumber == 6 || sceneNumber == 5)
        {
            if (playerForm < 3)
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
        else if (sceneNumber == 4)
        {
            if (playerForm < 2)
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
            grounded = false;
            moveForce = moveForceInJump;
        }
    }
    #endregion
}