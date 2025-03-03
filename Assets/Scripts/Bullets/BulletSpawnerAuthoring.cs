using Unity.Entities;
using UnityEngine;

namespace IT4080C
{
    public struct BulletSpawner : IComponentData
    {
        public Entity Bullet;
    }

    [DisallowMultipleComponent]
    public class BulletSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Bullet;

        class SpawnerBaker : Baker<BulletSpawnerAuthoring>
        {
            public override void Bake(BulletSpawnerAuthoring authoring)
            {
                BulletSpawner component = default(BulletSpawner);
                component.Bullet = GetEntity(authoring.Bullet, TransformUsageFlags.Dynamic);
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, component);
            }
        }
    }
}
