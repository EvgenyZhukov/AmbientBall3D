using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Скрипты")]
    public GameScript GameScript;
    //public PlayerController playerController;
    public ScriptUI scriptUI;
    [Header("Управление камерой")]
    public Transform target;         // Объект за которым осуществляется следование
    public Vector3 offset;           // Разница расстояний между камерой и объектом наблюдения
    private float sensitivity = 1f;   // Чувствительность джойстика
    private float limitTop = 70f;     // Ограничение вращения по Y
    private float limitBottom = 40f;
    private float zoomCam = 13f;       // Значение зума камеры
    public float X, Y;              // Переменные осей вращения камеры
    public float tempX, tempY;
    public bool loading = false;

    [SerializeField] private float camVertical;
    [SerializeField] private float camHorizontal;

    void Awake()
    {
        if (!SaveLoadData.GetFirstLevelLaunch())
        {
            loading = true;
        }
    }

    void Start()
    {
        CamStart();
    }

    void Update()
    {
        CamControl();
        CamLoad();
    }

    /// <summary>
    /// Управление камерой
    /// </summary>
    private void CamControl()            
    {
        camVertical = -scriptUI.JoystickCam.Vertical;          
        camHorizontal = -scriptUI.JoystickCam.Horizontal;          
        
        // Вращение камеры джойстиком
        X = transform.localEulerAngles.y + camHorizontal * sensitivity;
        Y += camVertical * sensitivity;
        

        Y = Mathf.Clamp(Y, -limitTop, -limitBottom);     // Ограничение угла обзора
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;
    }
    private void CamLoad()
    {
        if (loading)
        {
            SaveLoadData.LoadCamAxisTemp(out tempX, out tempY);

            X = transform.localEulerAngles.y + tempX;
            Y = tempY;
            
            transform.localEulerAngles = new Vector3(-Y, X, 0);
            transform.position = transform.localRotation * offset + target.position;

            loading = false;
        }
    }
    /// <summary>
    /// Выставляет стартовую позицию камеры с учетом положения объекта наблюдения и зума
    /// </summary>
    private void CamStart()
    {
        offset = new Vector3(offset.x, offset.y, -zoomCam);
        transform.position = target.position + offset;
    }
}
