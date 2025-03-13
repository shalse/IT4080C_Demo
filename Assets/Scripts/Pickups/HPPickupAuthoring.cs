using Unity.Entities;
using UnityEngine;
    
namespace IT4080C
{
    /// <summary>
    /// Flag component to mark an entity as a Bullet.
    /// </summary>
    public struct HPPickupComponent : IComponentData
    {
        public float timer;
        public byte hasBeenPickedup;
        public bool touchable;
    }

    /// <summary>
    /// The authoring component for the Bullet.
    /// </summary>
    [DisallowMultipleComponent]
    public class HPPickupAuthoring : MonoBehaviour
    {
        class Baker : Baker<HPPickupAuthoring>
        {
            public override void Bake(HPPickupAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<HPPickupComponent>(entity, new HPPickupComponent
                {
                    timer = 5f,
                    hasBeenPickedup = 0,
                    touchable = true
                });
            }
        }
    }
}
