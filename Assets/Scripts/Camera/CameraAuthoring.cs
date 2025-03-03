using Unity.Entities;
using UnityEngine;
namespace IT4080C
{
    public class CameraAuthoring : MonoBehaviour
    {
    }

    public struct CameraData : IComponentData
    {
    }

    public class CameraBaker : Baker<CameraAuthoring>
    {
        public override void Bake(CameraAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<CameraData>(entity);
        }
    }
}