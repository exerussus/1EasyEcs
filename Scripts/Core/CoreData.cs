
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public struct TransformData : IEcsComponent
    {
        public Transform Value;
        public void InitializeValues(Transform value)
        {
            Value = value;
        }
    }

    public struct RigidBody2DData : IEcsComponent
    {
        public Rigidbody2D Value;
    }

    public struct RigidBody3DData : IEcsComponent
    {
        public Rigidbody Value;
    }
    
    public struct OnDestroyData : IEcsData<GameObject, float>
    {
        public float TimeRemaining;
        public GameObject ObjectToDelete;
        public void InitializeValues(GameObject objectToDelete, float value)
        {
            TimeRemaining = value;
            ObjectToDelete = objectToDelete;
        }
    }
}