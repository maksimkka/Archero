using System;
using System.Collections.Generic;
using Code.Bullet;
using UnityEngine;

namespace Code.Enemy
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