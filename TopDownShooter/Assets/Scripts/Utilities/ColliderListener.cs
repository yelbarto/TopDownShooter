using System;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(Collider))]
    public class ColliderListener : MonoBehaviour
    {
        public Action<Collider> OnColliderEnter;
        
        private void OnTriggerEnter(Collider other)
        {
            OnColliderEnter?.Invoke(other);
        }
    }
}