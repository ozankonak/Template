using System.Collections.Generic;
using System.Linq;
using Containers;
using Managers;
using Observer;
using Sheeps;
using Systems;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [Inject] private List<BoxCollider> houseTriggerPoints;
        [Inject] private SerializableDictionary<int, Sheep> SheepDictionary;
        [Inject] private VFXSystem vfxSystem;
        [Inject] private AudioManager audioManager;
        [Inject] private GameAudioContainer gameAudioContainer;

        private void OnTriggerEnter(Collider other)
        {
            CheckHouses(other);
            CheckSheep(other);
        }
        
        private void CheckHouses(Collider other)
        {
            for (var i = 0; i < houseTriggerPoints.Count; i++)
            {
                var houseObject = houseTriggerPoints[i];

                if (!houseObject.Equals(other)) continue;
                
                Destroy(houseObject.transform.parent.gameObject);
                houseTriggerPoints.Remove(houseObject);
            }
        }
        
        private void CheckSheep(Collider other)
        {
            Sheep sheep = other.GetComponent<Sheep>();

            if (sheep == null) return;

            if (!SheepDictionary.ContainsValue(sheep)) return;
            
            var keyToRemove = SheepDictionary.FirstOrDefault(pair => pair.Value == sheep).Key;

            if (keyToRemove < 0) return;
            
            SheepDictionary.Remove(keyToRemove);
            sheep.gameObject.SetActive(false);
            
            vfxSystem.CreateSheepCollectParticle(sheep.transform.position + Vector3.up);
            
            audioManager.Play2DSound(gameAudioContainer.CollectShipClip,true);
        }

    }
}
