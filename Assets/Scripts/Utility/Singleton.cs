using UnityEngine;

namespace Utility
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _ins = null;
        public static T ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = FindObjectOfType<T>();
                    if (_ins == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        _ins = singleton.AddComponent<T>();
						
                    }
                }
                return _ins;
            }
        }
		
        [SerializeField] private SingletonType type = SingletonType.Infinite;
        private enum SingletonType
        {
            OneTime,
            Infinite
        }
		
        public virtual void Awake()
        {
            if (_ins == null)
            {
                _ins = this as T;
                if (type == SingletonType.Infinite)
                {
                    transform.parent = null;
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}