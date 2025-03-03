using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace IT4080C
{
    public struct SpawnPointData : IBufferElementData
    {
        public Vector3 spawnPoints;
    }
    public struct SpawnPtsComponent : IComponentData
    {
        public Entity spPt;
    }
    [DisallowMultipleComponent]
    public class CreateSpawnPointBuffer : MonoBehaviour
    {
        /// <summary>
        /// The cube prefab to spawn.
        /// </summary>
        public GameObject[] spawnPts;

        class SpawnerBaker : Baker<CreateSpawnPointBuffer>
        {
            public override void Bake(CreateSpawnPointBuffer authoring)
            {

                EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                Entity ent = entityManager.CreateEntity();



                DynamicBuffer<SpawnPointData> dynamicBuff = entityManager.AddBuffer<SpawnPointData>(ent);
                for (int i = 0; i < authoring.spawnPts.Length; i++)
                {
                    Vector3 localPosition = authoring.spawnPts[0].transform.localPosition;
                    dynamicBuff.Add(new SpawnPointData { spawnPoints = localPosition });
                }

            }
        }
    }
}

