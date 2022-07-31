using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// јнимирует шар в меню
/// </summary>
public class BallAnimator : MonoBehaviour
{
    public GameObject ball;
    public GameObject line1;
    public GameObject line2;
    public GameObject line3;
    [SerializeField] private float timer = 0f;
    [SerializeField] private int changer;
    [SerializeField] private float x;
    [SerializeField] private bool increaseForm = false;
    [SerializeField] private bool decreaseForm = false;
    [SerializeField] private bool materialChanging = false;
    private float formSpeed = 3f;
    private float rotationSpeed = 30f;

    public Material ballMaterialRed;
    public Material ballMaterialBlue;
    public Material ballMaterialYellow;
    public Material ballMaterialGreen;

    void Update()
    {
        Timer();
        BallRotator();
        BallTransformator();
    }
    /// <summary>
    /// Ёффект изменени€ цвета шара
    /// </summary>
    private void BallTransformator()
    {
        x = line1.transform.localScale.x;
        if (increaseForm)
        {
            line1.transform.localScale = new Vector3(x + formSpeed * Time.deltaTime, 1.02f, 1.02f);
        }
        if (x >= 0.9f)
        {
            increaseForm = false;
            decreaseForm = true;
            materialChanging = true;
        }
        if (decreaseForm)
        {
            line1.transform.localScale = new Vector3(x - formSpeed * Time.deltaTime, 1.02f, 1.02f);
        }
        if (decreaseForm && x <= 0.3f)
        {
            decreaseForm = false;
            line1.transform.localScale = new Vector3(0.3f, 1.02f, 1.02f);
            line2.transform.localScale = new Vector3(0.3f, 1.02f, 1.02f);
            line3.transform.localScale = new Vector3(0.3f, 1.02f, 1.02f);
        }
        if (decreaseForm || increaseForm)
        {
            line2.transform.localScale = line1.transform.localScale;
            line3.transform.localScale = line1.transform.localScale;
        }
    }
    /// <summary>
    /// ¬ращает шар, мен€ет направление вращени€ и цвет
    /// </summary>
    private void BallRotator()
    {
        ball.transform.position = new Vector3(26.3f + 0.25f * Mathf.Sin(Time.time), 52, -47);
        switch (changer)
        {
            case 1:
                ball.transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0));
                if (materialChanging)
                {
                    line1.GetComponent<MeshRenderer>().material = ballMaterialRed;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialRed;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialRed;
                    materialChanging = false;
                }
                break;
            case 2:
                ball.transform.Rotate(new Vector3(-rotationSpeed * Time.deltaTime, 0, rotationSpeed * Time.deltaTime));
                if (materialChanging)
                {
                    line1.GetComponent<MeshRenderer>().material = ballMaterialBlue;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialBlue;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialBlue;
                    materialChanging = false;
                }
                break;
            case 3:
                ball.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime));
                if (materialChanging)
                {
                    line1.GetComponent<MeshRenderer>().material = ballMaterialYellow;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialYellow;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialYellow;
                    materialChanging = false;
                }
                break;
            case 4:
                ball.transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, -rotationSpeed * Time.deltaTime));
                if (materialChanging)
                {
                    line1.GetComponent<MeshRenderer>().material = ballMaterialGreen;
                    line2.GetComponent<MeshRenderer>().material = ballMaterialGreen;
                    line3.GetComponent<MeshRenderer>().material = ballMaterialGreen;
                    materialChanging = false;
                }
                break;
            default:
                changer = 1;
                break;
        }
    }

    private void Timer()
    {
        timer += 1f * Time.deltaTime;
        if (timer > 5f)
        {
            timer = 0;
            changer++;
            increaseForm = true;
        }
    }
}
