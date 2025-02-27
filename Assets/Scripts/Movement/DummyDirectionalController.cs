using UnityEngine;

namespace SecondProject.Movement
{
    public class DummyDirectionalController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection { get; private set; }

        protected void Awake()
        {
            MovementDirection = Vector3.zero;
        }
    }
}