using System;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(Collider))]
    public class ColliderListener : MonoBehaviour
    {
        public Action<Collider> OnColliderEnter;
        public Action<Collider> OnColliderExit;
        
        private void OnTriggerEnter(Collider other)
        {
            OnColliderEnter?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnColliderExit?.Invoke(other);
        }
    }
}