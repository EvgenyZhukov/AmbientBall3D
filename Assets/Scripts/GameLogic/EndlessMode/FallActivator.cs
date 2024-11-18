using UnityEngine;

public class FallActivator : MonoBehaviour
{
    public ActiveEndlessColumn ActiveEndlessColumn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveEndlessColumn.activated = true;
            Debug.Log("activated");
        }
    }
}
