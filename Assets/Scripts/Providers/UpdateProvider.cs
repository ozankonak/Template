using System;
using UnityEngine;
using VContainer.Unity;

namespace Providers
{
    public class UpdateProvider : ITickable
    {
        public Action CheckInputUpdate;
        public Action StartCounterUpdate;
		
        public void Tick()
        {
            if(!Application.isPlaying) return;
            CheckInputUpdate?.Invoke();
            StartCounterUpdate?.Invoke();
        }
		
        private void Reset()
        {
            CheckInputUpdate = null;
            StartCounterUpdate = null;
        }
    }
}