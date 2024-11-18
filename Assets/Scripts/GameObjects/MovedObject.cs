using UnityEngine;

public class MovedObject : MonoBehaviour
{
    public float rangeX, rangeY, rangeZ;
    [SerializeField] private float timer = 0f;
    public bool activeX, activeY, activeZ;
    public int switcher;
    public bool inMove = false;
    public Vector3 posFirst;
    public Vector3 posSecond;

    void Update()
    {
        if (inMove)
        {
            float deltaTime = 1.0f * Time.deltaTime;
            Vector3 movement = Vector3.zero;

            switch (switcher)
            {
                case 1:
                    if (activeX) movement.x = rangeX * deltaTime;
                    else if (activeY) movement.y = rangeY * deltaTime;
                    else if (activeZ) movement.z = rangeZ * deltaTime;
                    break;
                case 2:
                    if (activeX) movement.x = -rangeX * deltaTime;
                    else if (activeY) movement.y = -rangeY * deltaTime;
                    else if (activeZ) movement.z = -rangeZ * deltaTime;
                    break;
                default:
                    break;
            }

            transform.position += movement;
            timer += deltaTime;

            if (timer >= 1)
            {
                inMove = false;
                timer = 0;

                switch (switcher)
                {
                    case 1:
                        transform.position = posSecond;
                        break;
                    case 2:
                        transform.position = posFirst;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}