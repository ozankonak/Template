using System.Linq;
using Observer;
using UnityEngine;
using VContainer;

namespace Sheeps
{
    public class SheepDestroyer : MonoBehaviour
    {
        [Inject] private SerializableDictionary<int, Sheep> SheepDictionary;

        private void OnTriggerEnter(Collider other)
        {
            Sheep sheep = other.GetComponent<Sheep>();

            if (sheep == null) return;
            
            var keyToRemove = SheepDictionary.FirstOrDefault(pair => pair.Value == sheep).Key;

            if (keyToRemove < 0) return;
            
            SheepDictionary.Remove(keyToRemove);
            sheep.gameObject.SetActive(false);
        }
    }
}