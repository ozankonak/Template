using System;
using UnityEngine;
using VContainer.Unity;

namespace Providers
{
    public class UpdateProvider : ITickable
    {
        public Action CheckInputUpdate;
		
        public void Tick()
        {
            if(!Application.isPlaying) return;
            CheckInputUpdate?.Invoke();
        }
		
        private void Reset()
        {
            CheckInputUpdate = null;
        }
    }
}