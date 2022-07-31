using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("�������")]
    public GameScript GameScript;
    //public PlayerController playerController;
    public ScriptUI scriptUI;
    [Header("���������� �������")]
    public Transform target;         // ������ �� ������� �������������� ����������
    public Vector3 offset;           // ������� ���������� ����� ������� � �������� ����������
    private float sensitivity = 1f;   // ���������������� ���������
    private float limitTop = 70f;     // ����������� �������� �� Y
    private float limitBottom = 40f;
    private float zoomCam = 13f;       // �������� ���� ������
    public float X, Y;              // ���������� ���� �������� ������
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
    /// ���������� �������
    /// </summary>
    private void CamControl()            
    {
        camVertical = -scriptUI.JoystickCam.Vertical;          
        camHorizontal = -scriptUI.JoystickCam.Horizontal;          
        
        // �������� ������ ����������
        X = transform.localEulerAngles.y + camHorizontal * sensitivity;
        Y += camVertical * sensitivity;
        

        Y = Mathf.Clamp(Y, -limitTop, -limitBottom);     // ����������� ���� ������
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
    /// ���������� ��������� ������� ������ � ������ ��������� ������� ���������� � ����
    /// </summary>
    private void CamStart()
    {
        offset = new Vector3(offset.x, offset.y, -zoomCam);
        transform.position = target.position + offset;
    }
}
