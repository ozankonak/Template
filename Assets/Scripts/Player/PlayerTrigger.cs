using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerTrigger : MonoBehaviour
    {
        [Inject] private List<BoxCollider> houseTriggerPoints;

        private void OnTriggerEnter(Collider other)
        {
            CheckHouses(other);
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
    }
}
