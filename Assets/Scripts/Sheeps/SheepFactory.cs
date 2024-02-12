using DG.Tweening;
using ObjectPool;
using Observer;
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
            SpawnTween().OnComplete(AutoSpawn);
        }

        private Tween SpawnTween()
        {
            return DOVirtual.DelayedCall(Random.Range(0.25f, 1.5f), ()=> CreateSheep());
        }

        private void CreateSheep()
        {
            var sheepInstance = sheepPrefab.Spawn();
            sheepInstance.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), 0.2f, 488f);
            sheepInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
            var sheep = sheepInstance.GetComponent<Sheep>();
            resolver.Inject(sheep);
            SheepDictionary.Add(sheepCount,sheep);
            sheepCount++;
        }
    }
}