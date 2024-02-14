using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(menuName = "Container/GameObjectContainer", fileName = "GameObjectContainer", order = 1)]
    public class GameObjectContainer : ScriptableObject
    {
        public GameObject Sheep;

        public List<GameObject> GetAllGameObjects()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return fields
                .Where(field => typeof(GameObject).IsAssignableFrom(field.FieldType))
                .Select(field => (GameObject)field.GetValue(this))
                .ToList();
        }
    }
}