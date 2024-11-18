using UnityEngine;

public class LevelRegionManagerTriple : MonoBehaviour
{
    public GameObject regionActiveOff;
    public GameObject regionStaticOff;

    public GameObject regionActiveOff_2;
    public GameObject regionStaticOff_2;

    public GameObject regionActiveOn;
    public GameObject regionStaticOn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            regionActiveOff.SetActive(false);
            regionStaticOff.SetActive(false);

            regionActiveOff_2.SetActive(false);
            regionStaticOff_2.SetActive(false);

            regionActiveOn.SetActive(true);
            regionStaticOn.SetActive(true);
        }
    }
}
