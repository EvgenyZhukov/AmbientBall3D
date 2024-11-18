using PlayerPrefsSavingMethods;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Скрипты")]
    public GameScript GameScript;
    public ScriptUI scriptUI;
    [Header("Управление камерой")]
    public Transform target;         // Объект за которым осуществляется следование
    public Vector3 offset;           // Разница расстояний между камерой и объектом наблюдения
    private float sensitivity = 100f;   // Чувствительность джойстика
    private float limitTop = 90f;     // Ограничение вращения по Y
    private float limitBottom = 45f;
    private float zoomCam = 13f;       // Значение зума камеры
    public float X, Y;              // Переменные осей вращения камеры
    public float tempX, tempY;
    public bool loading = false;
    public LayerMask obstacles;
    private RaycastHit hit;
    bool camMoved;

    float standartRangeCulling = 25f;
    public Camera cameraFirst;

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
        ClippingDistance();
    }
    void Update()
    {
        CamViewRay();
        CamControl();
        CamLoad();
        //Debug.Log("layerCullDistances: " + string.Join(", ", cameraFirst.layerCullDistances));
    }
    /// <summary>
    /// Управление камерой
    /// </summary>
    private void CamControl()            
    {
            camVertical = -scriptUI.JoystickCam.Vertical;
            camHorizontal = -scriptUI.JoystickCam.Horizontal;

            // Вращение камеры джойстиком
            if (hit.collider != null)
            {
                X = transform.localEulerAngles.y;
                Y -= 50f * Time.deltaTime;
            }
            else
            {
                X = transform.localEulerAngles.y + camHorizontal * sensitivity * Time.deltaTime;
                Y += camVertical * sensitivity * Time.deltaTime;
            }
            Y = Mathf.Clamp(Y, -limitTop, -limitBottom);     // Ограничение угла обзора

            transform.localEulerAngles = new Vector3(-Y, X, 0);
            transform.position = transform.localRotation * offset + target.position;

            if (camVertical != 0 || camHorizontal != 0)
            {
                camMoved = true;
            }

            if (camMoved)
            {
                SaveLoadData.SaveCamAxisTemp(X, Y);
                camMoved = false;
            }
        
    }
    private void CamLoad()
    {
        if (loading)
        {
            SaveLoadData.LoadCamAxisTemp(out tempX, out tempY);
            //Debug.Log(tempX);
            //Debug.Log(tempY);

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
    private void CamViewRay()
    {
        Ray ray = new Ray(transform.position, target.position - transform.position);
        Physics.Raycast(ray, out hit, zoomCam, obstacles);

        //Debug.DrawLine(ray.origin, hit.point, Color.red);
        //Debug.Log(hit.collider);
    }
    private void ClippingDistance()
    {
        float[] distances = new float[32];
        distances[0] = standartRangeCulling;
        distances[1] = standartRangeCulling;
        distances[2] = standartRangeCulling;
        distances[3] = standartRangeCulling;
        distances[4] = standartRangeCulling;
        distances[5] = standartRangeCulling;
        distances[6] = standartRangeCulling;
        distances[7] = 100f;
        cameraFirst.layerCullDistances = distances;
        //Debug.Log("layerCullDistances: " + string.Join(", ", distances));
    }
}
