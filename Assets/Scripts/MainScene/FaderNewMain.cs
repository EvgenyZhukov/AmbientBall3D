using UnityEngine;

public class FaderNewMain : MonoBehaviour
{
    public bool fading = false;
    public bool brighten = false;
    public bool fadingHalf = false;
    public bool fadingAlmost = false;
    private float r;
    private float g;
    private float b;
    private float a;
    public Material panelMaterial;
    public GameObject panelObj;
    private float changeStep = 0.02f;

    public Renderer objectRenderer;


    void Start()
    {
        r = panelMaterial.color.r;
        g = panelMaterial.color.g;
        b = panelMaterial.color.b;
        a = panelMaterial.color.a;
    }
    void FixedUpdate()
    {
        Fade();
    }
    /// <summary>
    /// Меняет прозрачность панели
    /// </summary>
    private void Fade()
    {
        switch ((fading, brighten, fadingHalf, fadingAlmost))
        {
            case (true, false, false, false):
                if (a < 1)
                {
                    a += changeStep;
                    ChangePanelColor();
                }
                else if (a >= 1)
                {
                    fading = false;
                }
                break;
            case (false, true, false, false):
                if (a > 0)
                {
                    a -= changeStep;
                    ChangePanelColor();
                }
                else if (a <= 0)
                {
                    brighten = false;
                    panelObj.SetActive(false);
                }
                break;
            case (false, false, true, false):
                if (a < 0.5f)
                {
                    a += changeStep * 2;
                    ChangePanelColor();
                }
                else if (a >= 0.5f)
                {
                    fadingHalf = false;
                }
                break;
            case (false, false, false, true):
                if (a < 0.7f)
                {
                    a += changeStep * 1/0.7f;
                    ChangePanelColor();
                }
                else if (a >= 0.7f)
                {
                    fadingAlmost = false;
                }
                break;
            default:
                break;
        }
    }
    private void ChangePanelColor()
    {
        objectRenderer.material.color = new Color(r, g, b, a);
    }

    public void SetBluFader()
    {
        r = panelMaterial.color.r;
        g = panelMaterial.color.g;
        b = panelMaterial.color.b;
        a = panelMaterial.color.a;
    }
}
