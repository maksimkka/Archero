using System;
using UnityEngine;

namespace Code.Collision
{
    public static class CollisionDetector
    {
        public static event Action<Collider, Collider> CollisionDetected;
        public static event Action<Collider, Collider> TriggerDetected;

        public static void HandleCollisionStay(Collider selfCollider, Collider otherCollider)
        {
            CollisionDetected?.Invoke(selfCollider, otherCollider);
        }

        public static void HandleTriggerEnter(Collider selfCollider, Collider otherCollider)
        {
            TriggerDetected?.Invoke(selfCollider, otherCollider);
        }
    }
}