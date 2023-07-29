using System;
using Code.Logger;
using UnityEngine;

namespace Code.Enemy
{
    public class CollisionEnterDetector : MonoBehaviour
    {
        [SerializeField] public float DelayBetweenCollisions;
        private Collider selfCollider;
        private float lastCollisionTime;

        private void Awake()
        {
            selfCollider = gameObject.GetComponent<Collider>();
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            if (Time.time - lastCollisionTime >= DelayBetweenCollisions)
            {
                CollisionDetector.HandleCollisionStay(selfCollider, collisionInfo.collider);
                lastCollisionTime = Time.time;
            }
        }
    }
}