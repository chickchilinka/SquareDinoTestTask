using UnityEngine;

namespace Base.Movement
{
    public abstract class Moveable : MonoBehaviour
    {
        public delegate void OnReachDestination();
        public abstract void MoveTo(Vector3 destination, OnReachDestination onReach);

        public abstract void RotateTo(Quaternion worldEuler, OnReachDestination onReach);
    }
}
