using UnityEngine;

/// <summary>
/// Раскачивает колонны
/// </summary>
public class DriftingColumn : MonoBehaviour
{
    public GameObject target;

    float linearPendulum;

    public bool activeX;
    public float amplitudoX;
    public float periodX;
    [Range(-1f, 1f)]
    public float offsetX;
    public float delayX;

    public bool activeY;
    public float amplitudoY;
    public float periodY;
    [Range(-1f, 1f)]
    public float offsetY;
    public float delayY;

    public bool activeZ;
    public float amplitudoZ;
    public float periodZ;
    [Range(-1f, 1f)]
    public float offsetZ;
    public float delayZ;

    public bool activeXR;
    public float amplitudoXR;
    public float periodXR;
    [Range(-1f, 1f)]
    public float offsetXR;
    public float delayXR;

    public bool activeZR;
    public float amplitudoZR;
    public float periodZR;
    [Range(-1f, 1f)]
    public float offsetZR;
    public float delayZR;

    private void Start()
    {
        PendulumOffset();
    }
    void FixedUpdate()
    {
            TransformPositionXYZ();
            TransformRotationXZ();
    }
    void TransformPositionXYZ()
    {
        transform.position = transform.TransformPoint(activeX ? Pendulum(periodX, amplitudoX, delayX) : 0.0f,
                                                        activeY ? Pendulum(periodY, amplitudoY, delayY) : 0.0f, 
                                                        activeZ ? Pendulum(periodZ, amplitudoZ, delayZ) : 0.0f);
    }
    void PendulumOffset()
    {
        transform.position = transform.TransformPoint(activeX ? -offsetX * amplitudoX : 0.0f,
                                                    activeY ? -offsetY * amplitudoY : 0.0f,
                                                    activeZ ? -offsetZ * amplitudoZ : 0.0f);

        transform.RotateAround(target.transform.position, new Vector3(1, 0, 0), activeXR ? -offsetXR * amplitudoXR : 0.0f);
        transform.RotateAround(target.transform.position, new Vector3(0, 0, 1), activeZR ? -offsetZR * amplitudoZR : 0.0f);
    }
    void TransformRotationXZ()
    {
        transform.RotateAround(target.transform.position, new Vector3(1, 0, 0), activeXR ? Pendulum(periodXR, amplitudoXR, delayXR) : 0.0f);
        transform.RotateAround(target.transform.position, new Vector3(0, 0, 1), activeZR ? Pendulum(periodZR, amplitudoZR, delayZR) : 0.0f);
    }
    float Pendulum(float period, float amplitudo, float delay)
    {
        linearPendulum = Mathf.Sin((Time.timeSinceLevelLoad - delay) * period) * amplitudo * period * Time.deltaTime;
        return linearPendulum;
    }
}
