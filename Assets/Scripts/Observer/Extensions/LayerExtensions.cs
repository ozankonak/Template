using UnityEngine;

namespace Observer.Extensions {
    public static class LayerExtensions {
        
        public static bool IncludesLayer(this LayerMask layerMask, int layer) {
            return ((1 << layer) & layerMask.value) != 0;
        }
        
    }
}