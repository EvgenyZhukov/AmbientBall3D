using UnityEngine;

public class LevelRegionManager : MonoBehaviour
{
    public GameObject regionActiveOff;
    public GameObject regionStaticOff;

    public GameObject regionActiveOn;
    public GameObject regionStaticOn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            regionActiveOff.SetActive(false);
            regionStaticOff.SetActive(false);

            regionActiveOn.SetActive(true);
            regionStaticOn.SetActive(true);
        }
    }
}
