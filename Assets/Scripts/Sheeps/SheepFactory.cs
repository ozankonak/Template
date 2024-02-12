using DG.Tweening;
using Observer;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Sheeps
{
    public class SheepFactory : ISheepFactory
    {
        [Inject] private SerializableDictionary<int, Sheep> SheepDictionary;
        
        private readonly IObjectResolver resolver;
        private readonly GameObject sheepPrefab;

        private int sheepCount = 0;

        public SheepFactory(IObjectResolver resolver, GameObject sheepPrefab)
        {
            this.resolver = resolver;
            this.sheepPrefab = sheepPrefab;
        }
        
        public void AutoSpawn()
        {
            DOVirtual.DelayedCall(Random.Range(0.5f, 3f),
                () =>
                {
                    CreateSheep(new Vector3(Random.value * 100f, 0f, Random.value * 100f),
                        quaternion.identity);
                  
                }).onComplete += () =>
            {
                DOVirtual.DelayedCall(Random.Range(0.5f, 3f), AutoSpawn);
            };
        }

        public Sheep CreateSheep(Vector3 position, Quaternion rotation)
        {
            var sheepInstance = Object.Instantiate(sheepPrefab, position, rotation);
            var sheep = sheepInstance.GetComponent<Sheep>();
            resolver.Inject(sheep);
            SheepDictionary.Add(sheepCount,sheep);
            sheepCount++;
            return sheep;
        }
    }
}