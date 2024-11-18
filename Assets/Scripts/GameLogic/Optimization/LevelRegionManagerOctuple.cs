using System.Collections.Generic;
using UnityEngine;

public class LevelRegionManagerOctuple : MonoBehaviour
{
    [Header("Regions")]
    public List<GameObject> regionActiveOff;
    public List<GameObject> regionStaticOff;
    public GameObject regionActiveOn;
    public GameObject regionStaticOn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Включаем последний регион
            regionActiveOn.SetActive(true);
            regionStaticOn.SetActive(true);

            // Выключаем все регионы, кроме последнего
            for (int i = 0; i < regionActiveOff.Count - 1; i++)
            {
                regionActiveOff[i].SetActive(false);
                regionStaticOff[i].SetActive(false);
            }
        }
    }
}