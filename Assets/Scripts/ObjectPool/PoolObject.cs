using UnityEngine;

namespace ObjectPool
{
	public class PoolObject : MonoBehaviour
	{
		internal GameObject original = null;
		[SerializeField]internal byte countInitial = 1;
		[SerializeField]internal byte minuteClear = 5;
		[SerializeField]internal bool poolOnDisable = true;
		
		internal bool inPool = false;
		private void OnDisable()
		{
			if(poolOnDisable) Pool(gameObject);
		}
		
		public static GameObject Spawn(GameObject original,Transform parent=null,bool enable=true)
		{
			GameObject clone = Poolv1.Spawn(original);
			clone.transform.SetParent(parent);
			clone.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			clone.SetActive(enable);
			return clone;
		}
		
		public static GameObject Spawn(GameObject original,Vector3 pos,Vector3 rot,bool enable=true)
		{
			GameObject clone = Poolv1.Spawn(original);
			clone.transform.position = pos;
			clone.transform.rotation = Quaternion.Euler(rot);
			clone.SetActive(enable);
			return clone;
		}
		
		public static T Spawn<T>(T original,Transform parent=null,bool enable = true) where T : Component
		{
			return Spawn(original.gameObject,parent,enable).GetComponent<T>();
		}
		
		public static T Spawn<T>(T original,Vector3 pos, Vector3 rot,bool enable = true) where T : Component
		{
			return Spawn(original.gameObject,pos,rot,enable).GetComponent<T>();
		}
		
		public static void Pool(GameObject clone)
		{
			Poolv1.Pool(clone);
		}
		
		public static void Warm(GameObject original)
		{
			Poolv1.GetPool(original);
		}
		
		public static void BringPools()
		{
			Poolv1.BringPools();
		}
		
		public static void ExpirePools()
		{
			Poolv1.ExpirePools();
		}
	}
	
	public static class ExtPool
	{
		public static GameObject Spawn(this GameObject original,Transform parent=null,bool enable = true)
		{
			return PoolObject.Spawn(original,parent,enable);
		}
		
		public static GameObject Spawn(this GameObject original,Vector3 pos,Vector3 rot,bool enable = true)
		{
			return PoolObject.Spawn(original,pos,rot,enable);
		}
		
		public static T Spawn<T>(this T original,Transform parent=null,bool enable = true) where T : Component
		{
			return PoolObject.Spawn(original,parent,enable);
		}
		
		public static void Pool(this GameObject clone)
		{
			PoolObject.Pool(clone);
		}
		
		public static void Pool<T>(this T clone) where T : Component
		{
			PoolObject.Pool(clone.gameObject);
		}
	}
}