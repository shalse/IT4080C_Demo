
using Unity.Entities;
using UnityEngine;

namespace IT4080C
{
    public struct HPPickUPPoint : IComponentData
    {
    }

    public class HPPickUPPointAuthoring : MonoBehaviour
    {
        public class Baker : Baker<HPPickUPPointAuthoring>
        {
            public override void Bake(HPPickUPPointAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new HPPickUPPoint());
            }
        }
    }
}
