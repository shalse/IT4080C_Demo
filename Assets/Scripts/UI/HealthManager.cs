using IT4080C;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

partial struct HealthManager : ISystem
{
    

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<HPBar>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach (
            var playerRef
            in SystemAPI.Query<
            RefRO<HPBar>>().WithAll<Simulate>()) 
        {

           // playerData.currentHealth = playerRef.ValueRO.currentHP;
           // Debug.Log("Player HP: "+playerData.currentHealth);
          
        }
        ecb.Playback(state.EntityManager);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
