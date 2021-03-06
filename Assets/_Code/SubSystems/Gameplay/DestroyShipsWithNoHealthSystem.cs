﻿using Latios;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Lsss
{
    public class DestroyShipsWithNoHealthSystem : SubSystem
    {
        protected override void OnUpdate()
        {
            var   icb = latiosWorld.syncPoint.CreateInstantiateCommandBuffer<Translation>().AsParallelWriter();
            var   dcb = latiosWorld.syncPoint.CreateDestroyCommandBuffer().AsParallelWriter();
            float dt  = Time.DeltaTime;

            Entities.WithChangeFilter<ShipHealth>().ForEach((Entity entity,
                                                             int entityInQueryIndex,
                                                             in ShipHealth health,
                                                             in ShipExplosionPrefab explosionPrefab,
                                                             in LocalToWorld ltw) =>
            {
                if (health.health <= 0f)
                {
                    dcb.Add(entity, entityInQueryIndex);
                    if (explosionPrefab.explosionPrefab != Entity.Null)
                        icb.Add(explosionPrefab.explosionPrefab, new Translation { Value = ltw.Position }, entityInQueryIndex);
                }
            }).ScheduleParallel();
        }
    }
}

