using System.Collections.Generic;
using ObjectPool;
using UnityEngine;
using Utility;

namespace Gameguru
{
	internal class Poolv2 : Singleton<Poolv2>
	{
		#region Collection
		private readonly Dictionary<GameObject,Stack<PoolObject>> dictionary = new Dictionary<GameObject,Stack<PoolObject>>();
		private Stack<PoolObject> GetStack(GameObject key)
		{
			if(dictionary.ContainsKey(key)) return dictionary[key];
			Stack<PoolObject> stack = new Stack<PoolObject>();
			dictionary[key] = stack;
			return stack;
		}
		#endregion
		
		
		#region Main
		internal static GameObject Spawn(GameObject original,bool usePool = true)
		{
			Debug.LogFormat(original,$"MnPool:Spawn:original:{original.name}");
			GameObject clone = null;
			
			if(ins && ins.GetStack(original).Count>0) clone = ins.GetStack(original).Pop().gameObject;
			if(!clone)
			{
				Debug.LogFormat(original,$"MnPool:Instantiate:{original.name}");
				bool originalState = original.activeSelf;
				original.SetActive(false);
				clone = Instantiate(original);
				original.SetActive(originalState);
			}
			
			if(usePool) clone.AddComponent<PoolObject>().original = original;
			Debug.LogFormat(clone,$"MnPool:Spawn:clone:{clone.name}");
			return clone;
		}
		
		internal static void Pool(GameObject clone)
		{
			Debug.LogFormat(clone,$"MnPool:Pool:{clone.name}");
			
			if(clone == null) return;
			clone.SetActive(false);
			
			PoolObject poolObject = clone.GetComponent<PoolObject>();
			if(!poolObject || !ins)
			{
				Destroy(clone);
				return;
			}
			
			clone.transform.SetParent(ins.transform);
			ins.GetStack(poolObject.original).Push(poolObject);
		}
		#endregion
	}
}