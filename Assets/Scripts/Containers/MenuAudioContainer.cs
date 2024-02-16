using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(menuName = "Container/MenuAudioContainer", fileName = "MenuAudioContainer", order = 3)]
    public class MenuAudioContainer : ScriptableObject
    {
        public AudioClip StartButtonAudio;

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