using Unity.Entities;
using UnityEngine;

namespace IT4080C
{
    /// <summary>
    /// Flag component to mark an entity as a Bullet.
    /// </summary>
    public struct HPBar : IComponentData
    {
        public float currentHP;
    }

    /// <summary>
    /// The authoring component for the Bullet.
    /// </summary>
    [DisallowMultipleComponent]
    public class HPBarAuthouring : MonoBehaviour
    {
        class Baker : Baker<HPBarAuthouring>
        {
            public override void Bake(HPBarAuthouring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<HPBar>(entity, new HPBar
                {
                    currentHP = 50f
                });
            }
        }
    }
}
