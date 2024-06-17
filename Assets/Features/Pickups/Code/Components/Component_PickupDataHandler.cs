using UnityEngine;

public class Component_PickupDataHandler : MonoBehaviour
{
    public SO_PickupData dataWrapper;
    public PickupData workingData { get; private set; } = new();

    private void Awake()
    {
        workingData = dataWrapper.data.CloneData(dataWrapper.data);
    }
}
