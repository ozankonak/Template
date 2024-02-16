using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(menuName = "Container/GameAudioContainer", fileName = "GameAudioContainer", order = 4)]
    public class GameAudioContainer : ScriptableObject
    {
        public AudioClip CollectShipClip;

        public List<AudioClip> GetAllClips()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return fields
                .Where(field => typeof(AudioClip).IsAssignableFrom(field.FieldType))
                .Select(field => (AudioClip)field.GetValue(this))
                .ToList();
        }
    }
}