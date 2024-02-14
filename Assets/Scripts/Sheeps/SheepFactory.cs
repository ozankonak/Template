using System.Linq;
using DG.Tweening;
using ObjectPool;
using Observer;
using Providers;
using UnityEngine;
using VContainer;
using UnityEngine.Jobs;
using Unity.Jobs;
using Random = UnityEngine.Random;

namespace Sheeps
{
    public class SheepFactory : ISheepFactory
    {
        [Inject] private SerializableDictionary<int, Sheep> SheepDictionary;
        [Inject] private UpdateProvider updateProvider;
        [Inject] private LateUpdateProvider lateUpdateProvider;
        
        private readonly IObjectResolver resolver;
        private readonly GameObject sheepPrefab;

        private int sheepCount;

        private readonly float minSpawnTime = 0.1f;
        private readonly float maxSpawnTime = 0.2f;

        private readonly float sheepSpeed = 0.2f;
        private readonly float sheepBorder = 4.5f;
        
        struct MoveJob : IJobParallelForTransform
        {
            public void Execute(int index, TransformAccess transform)
            {
                transform.position += 0.2f * (transform.rotation * new Vector3(0, 0, 1));
            }
        }

        private MoveJob moveJob;
        private JobHandle moveHandle;
        private TransformAccessArray transforms;

        public SheepFactory(IObjectResolver resolver, GameObject sheepPrefab)
        {
            this.resolver = resolver;
            this.sheepPrefab = sheepPrefab;
            transforms = new TransformAccessArray(0);
        }

        public void Init()
        {
            updateProvider.SheepMovement += ManualUpdate;
            lateUpdateProvider.SheepMovementCompleteUpdate += ManualLateUpdate;
            AutoSpawn();
        }
        
        private void ManualUpdate()
        {
            if (SheepDictionary.Count <= 0) return;
            
            moveJob = new MoveJob();
            moveHandle = moveJob.Schedule(transforms);
        }

        private void ManualLateUpdate()
        {
            moveHandle.Complete();
        }
        
        public void AutoSpawn()
        {
            SpawnTween().OnComplete(AutoSpawn);
        }

        private Tween SpawnTween()
        {
            return DOVirtual.DelayedCall(Random.Range(minSpawnTime, maxSpawnTime), CreateSheep);
        }

        private void CreateSheep()
        {
            var sheepInstance = sheepPrefab.Spawn();
            sheepInstance.transform.position = RandomSheepPos();
            sheepInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
            var sheep = sheepInstance.GetComponent<Sheep>();
            resolver.Inject(sheep);
            SheepDictionary.Add(sheepCount,sheep);
            UpdateTransformAccessArray();
            sheepCount++;
        }
        
        private void UpdateTransformAccessArray()
        {
            var allSheepTransforms = SheepDictionary.Values.Select(sheep => sheep.transform).ToArray();
            transforms = new TransformAccessArray(allSheepTransforms);
        }

        private Vector3 RandomSheepPos()
        {
            return new Vector3(Random.Range(-sheepBorder, sheepBorder), 0.2f, 488f);
        }

        public void OnRemoveFactory()
        {
            transforms.Dispose();
        }
    }
}