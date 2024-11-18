using UnityEngine;

public class GravityElevatorManyButtons : MonoBehaviour
{
    public GameObject gravityForce;
    public ButtonColumn button;
    public ButtonColumn button2;
    public ButtonColumn button3;
    public bool activated = false;

    void Update()
    {
        if (button.activated && button2.activated && button3.activated)
        {
            activated = true;
        }
        if (activated)
        {
            gravityForce.SetActive(true);
        }
    }
}