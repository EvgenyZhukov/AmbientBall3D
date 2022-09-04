using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("�������")]
    public GameScript GameScript;
    public ScriptUI scriptUI;
    [Header("���������� �������")]
    public Transform target;         // ������ �� ������� �������������� ����������
    public Vector3 offset;           // ������� ���������� ����� ������� � �������� ����������
    private float sensitivity = 100f;   // ���������������� ���������
    private float limitTop = 90f;     // ����������� �������� �� Y
    private float limitBottom = 40f;
    private float zoomCam = 13f;       // �������� ���� ������
    public float X, Y;              // ���������� ���� �������� ������
    public float tempX, tempY;
    public bool loading = false;
    public LayerMask obstacles;
    private RaycastHit hit;

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
        CamViewRay();
        CamControl();
        CamLoad();
    }

    /// <summary>
    /// ���������� �������
    /// </summary>
    private void CamControl()            
    {
        camVertical = -scriptUI.JoystickCam.Vertical;
        camHorizontal = -scriptUI.JoystickCam.Horizontal;

        // �������� ������ ����������
        if (hit.collider != null)
        {
            X = transform.localEulerAngles.y;
            Y -= 100f * Time.deltaTime;
        }
        else
        {
            X = transform.localEulerAngles.y + camHorizontal * sensitivity * Time.deltaTime;
            Y += camVertical * sensitivity * Time.deltaTime;
        }
        Y = Mathf.Clamp(Y, -limitTop, -limitBottom);     // ����������� ���� ������

        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;
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
    /// ���������� ��������� ������� ������ � ������ ��������� ������� ���������� � ����
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
}
