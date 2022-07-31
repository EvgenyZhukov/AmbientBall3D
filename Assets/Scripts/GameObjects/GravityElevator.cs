using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityElevator : MonoBehaviour
{
    public GameObject gravityForce;
    public ButtonColumn button;
    [SerializeField] private bool activated = false;

    void FixedUpdate()
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