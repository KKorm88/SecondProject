using UnityEngine;

namespace SecondProject.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }
    }
}
