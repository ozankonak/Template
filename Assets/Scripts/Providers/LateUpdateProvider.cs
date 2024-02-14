using System;
using UnityEngine;
using VContainer.Unity;

namespace Providers
{
    public class LateUpdateProvider : ILateTickable
    {
        public Action SheepMovementCompleteUpdate;
        
        public void LateTick()
        {
            if(!Application.isPlaying) return;
            
            SheepMovementCompleteUpdate?.Invoke();
        }
        
        public void Reset()
        {
            SheepMovementCompleteUpdate = null;
        }
    }
}