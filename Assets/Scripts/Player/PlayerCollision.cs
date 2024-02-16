using System;
using System.Collections.Generic;
using Observer;
using Sheeps;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [Inject] private List<BoxCollider> houseTriggerPoints;
        [Inject] private SerializableDictionary<int, Sheep> SheepDictionary;

        private void OnTriggerEnter(Collider other)
        {
            CheckHouses(other);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            CheckSheep(hit);
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
        
        private void CheckSheep(ControllerColliderHit hit)
        {
            Sheep sheep = hit.gameObject.GetComponent<Sheep>();

            if (sheep == null) return;

            if (SheepDictionary.ContainsValue(sheep))
            {
                Destroy(gameObject);
            }
        }

    }
}
