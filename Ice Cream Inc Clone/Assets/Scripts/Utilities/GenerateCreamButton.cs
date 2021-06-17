using UnityEngine;

public class GenerateCreamButton : MonoBehaviour
{
    [SerializeField]
    IceCreamMachine machine;

    [SerializeField]
    GameObject iceCreamPrefab;

    public void ButtonDown()
    {
        machine.StartIceCream(iceCreamPrefab);
    }

    public void ButtonUp()
    {
        machine.StopIceCream();
    }
}
