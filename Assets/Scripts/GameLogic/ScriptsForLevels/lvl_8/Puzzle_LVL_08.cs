using UnityEngine;

public class Puzzle_LVL_08 : MonoBehaviour
{
    public MovedObject movedObjectScriptPart1;
    public MovedObject movedObjectScriptPart2;
    bool lockerPart1 = false;
    bool lockerPart2 = false;

    void Update()
    {
        if (movedObjectScriptPart1.inMove)
        {
            lockerPart1 = false;
        }
        if (movedObjectScriptPart2.inMove)
        {
            lockerPart2 = false;
        }

        if (!movedObjectScriptPart1.inMove && !lockerPart1)
        {
            if (movedObjectScriptPart1.transform.position.y == 22)
            {
                movedObjectScriptPart1.rangeY = -22;
                movedObjectScriptPart1.posSecond = new Vector3(0, 0, 0);
            }
            if (movedObjectScriptPart1.transform.position.y == 0)
            {
                movedObjectScriptPart1.rangeY = 22;
                movedObjectScriptPart1.posSecond = new Vector3(0, 22, 0);
            }
            lockerPart1 = true;
        }
        if (!movedObjectScriptPart2.inMove && !lockerPart2)
        {
            if (movedObjectScriptPart2.transform.position.y == 12)
            {
                movedObjectScriptPart2.rangeY = 12;
                movedObjectScriptPart2.posFirst = new Vector3(0, 0, 0);
            }
            if (movedObjectScriptPart2.transform.position.y == 0)
            {
                movedObjectScriptPart2.rangeY = -12;
                movedObjectScriptPart2.posFirst = new Vector3(0, 12, 0);
            }
            lockerPart2 = true;
        }
    }
}
