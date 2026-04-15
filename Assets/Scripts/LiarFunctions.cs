using UnityEngine;

public class LiarFunctions : MonoBehaviour
{
    public void DoubleDown()
    {
        GameManager.instance.MyEvents.LieDoubleDown();
    }

    public void Retract()
    {
        GameManager.instance.MyEvents.RetractLie();
    }
}
