using UnityEngine;
using static Constants;

public class ShipResources : MonoBehaviour {
    public EResourceType resourceType;
    public int quantity;

    public ShipResources(EResourceType rT, int number) {
        resourceType = rT;
        quantity = number;
    }

    public bool Consume(int consumedResources) {
        if (quantity - consumedResources > 0) {
            quantity -= consumedResources;
            return true;
        }
        return false;
        
    }

    public void Obtain(int obtainedResources) {
        quantity += obtainedResources;
    }
}