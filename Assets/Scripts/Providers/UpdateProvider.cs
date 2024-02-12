using System;
using UnityEngine;
using VContainer.Unity;

namespace Providers
{
    public class UpdateProvider : ITickable
    {
        public Action CheckInputUpdate;
        public Action CameraFollowUpdate;
        public Action StartCounterUpdate;
        public Action SheepMovement;
        
        public void Tick()
        {
            if(!Application.isPlaying) return;
            
            CheckInputUpdate?.Invoke();
            StartCounterUpdate?.Invoke();
            CameraFollowUpdate?.Invoke();
            SheepMovement?.Invoke();
        }
        
        public void Reset()
        {
            CheckInputUpdate = null;
            StartCounterUpdate = null;
            CameraFollowUpdate = null;
            SheepMovement = null;
        }
    }
}