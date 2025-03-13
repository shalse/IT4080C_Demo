using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Unity.NetCode;

namespace IT4080C
{
    /// <summary>
    /// Flag component to mark an entity as a Bullet.
    /// </summary>
    [GhostComponent]
    public struct Health : IComponentData
    {
        [GhostField] public float currentHealth;
    }

    /// <summary>
    /// The authoring component for the Bullet.
    /// </summary>
    [DisallowMultipleComponent]
    public class HealthAuthoring : MonoBehaviour
    {

        static class UIElementNames
        {
            public const string HpBarName = "TestText";
        }
        class Baker : Baker<HealthAuthoring>
        {
            public override void Bake(HealthAuthoring authoring)
            {

                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<Health>(entity, new Health
                {
                    currentHealth = 100f
                    
                });

              
            }

        }


    }
}
