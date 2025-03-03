using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;
namespace IT4080C
{
    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    partial struct ShootManager : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BulletSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
           var prefab = SystemAPI.GetSingleton<BulletSpawner>().Bullet;

            EntityCommandBuffer ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

            foreach ((
                var playerInput,
                var localTransform,
                var ghostOwner)
                in SystemAPI.Query<
                RefRO<CubeInput>,
                RefRO<LocalTransform>,
                RefRO<GhostOwner>>().WithAll<Simulate>())
            {
                    if (playerInput.ValueRO.shoot.IsSet)
                    {
                        Debug.LogWarning("Shoot Input");
                        Entity bulletEntity = ecb.Instantiate(prefab);

                        int bulletOffset = 3;
                        var forwardDir = math.mul(localTransform.ValueRO.Rotation, Vector3.forward) * bulletOffset;



                        ecb.SetComponent(bulletEntity, LocalTransform.FromPositionRotation(localTransform.ValueRO.Position + forwardDir, localTransform.ValueRO.Rotation));
                        ecb.SetComponent(bulletEntity, new GhostOwner { NetworkId = ghostOwner.ValueRO.NetworkId });
                    }
            }
            ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}
