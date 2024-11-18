using UnityEngine;

public class GravityElevator : MonoBehaviour
{
    public GameObject gravityForce;
    public ButtonColumn button;
    public bool activated = false;

    void Update()
    {
        if (button.activated)
        {
            activated = true;
        }
        if (activated)
        {
            gravityForce.SetActive(true);
        }
    }
}