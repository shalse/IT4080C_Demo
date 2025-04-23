
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;
namespace IT4080C
{
    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    partial struct BulletController : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

            foreach((
                RefRW<LocalTransform> localTransform,
                RefRW<Bullet> bullet,
                Entity entity)
                in SystemAPI.Query<
                    RefRW<LocalTransform>,
                    RefRW<Bullet>>().WithEntityAccess().WithAll<Simulate>())
            {
                float bulletSpeed = 5f;

                //change movement to the direction of player
           
                var forwardDir = math.mul(localTransform.ValueRO.Rotation, Vector3.forward);

                localTransform.ValueRW.Position += forwardDir * bulletSpeed *  SystemAPI.Time.DeltaTime;

                if(state.World.IsServer())
                {
                    //add kills
                    if (bullet.ValueRO.hitPlayerNetworkID <= 5)
                    {
                 
                        
                        foreach ((
                             RefRW<HealthComponent> healthComponent,
                             Entity playerEntity)
                                in SystemAPI.Query<
                            RefRW<HealthComponent>>()
                            .WithEntityAccess().WithAll<Simulate>())
                        {
                            if(bullet.ValueRO.killed)
                            {
                                Debug.Log("Bullet will kill");
                            }
                            if (healthComponent.ValueRO.ownerNetworkID == bullet.ValueRO.ownerNetworkID && bullet.ValueRO.killed)
                            {
                                healthComponent.ValueRW.kills += 1;
                                Debug.Log("Adding a kill");

                            }
                            if (healthComponent.ValueRO.ownerNetworkID == bullet.ValueRO.hitPlayerNetworkID && healthComponent.ValueRO.CurrentHealth <= 0)
                            {
                                Debug.Log("Player " + bullet.ValueRO.ownerNetworkID + " killed " + bullet.ValueRO.hitPlayerNetworkID);
                                healthComponent.ValueRW.deaths += 1;
                            }
                           
                            
                        }
                        
                    }
                    bullet.ValueRW.timer -= SystemAPI.Time.DeltaTime;
                    if (bullet.ValueRW.timer <= 0f)
                    {
                        ecb.DestroyEntity(entity);
                    }
                    else if (bullet.ValueRW.timer <= 4f)
                    {
                        bullet.ValueRW.hittable = true;
                    }
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