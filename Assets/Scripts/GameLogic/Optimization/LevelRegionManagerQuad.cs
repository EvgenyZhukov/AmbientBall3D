using UnityEngine;

public class LevelRegionManagerQuad : MonoBehaviour
{
    public GameObject regionActiveOff;
    public GameObject regionStaticOff;

    public GameObject regionActiveOff_2;
    public GameObject regionStaticOff_2;

    public GameObject regionActiveOff_3;
    public GameObject regionStaticOff_3;

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

            regionActiveOff_3.SetActive(false);
            regionStaticOff_3.SetActive(false);

            regionActiveOn.SetActive(true);
            regionStaticOn.SetActive(true);
        }
    }
}
