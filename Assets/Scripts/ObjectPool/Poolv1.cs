using System.Collections.Generic;
using ObjectPool;
using UnityEngine;
using Utility;

namespace Gameguru
{
	internal class Poolv1 : Singleton<Poolv1>
	{
		#region Static
		private readonly Dictionary<GameObject, PoolGroup> pools = new Dictionary<GameObject, PoolGroup>();
		internal static PoolGroup GetPool(GameObject original)
		{
			if(!original.TryGetComponent<PoolObject>(out var poolObject))
			{
				poolObject = original.AddComponent<PoolObject>();
				poolObject.original = original;
				Debug.Log($"PoolSetup_{original.name}",original);
			}
			if(poolObject.original==null) poolObject.original = original;
			
			original = poolObject.original;
			if(ins.pools.ContainsKey(original)) return ins.pools[original];
			PoolGroup newPool = new GameObject($"Pool_{original.name}").AddComponent<PoolGroup>();
			ins.pools.Add(original, newPool);
			newPool.transform.SetParent(ins.transform);
			newPool.gameObject.SetActive(true);
			newPool.original = poolObject;
			newPool.GrowInstant(1);
			return newPool;
		}
		
		internal static GameObject Spawn(GameObject original)
		{
			return GetPool(original).Spawn();
		}
		
		internal static void Pool(GameObject clone)
		{
			if(clone==null) return;
			if(!clone.TryGetComponent<PoolObject>(out var poolObject))
			{
				Destroy(clone);
				return;
			}
			
			if(poolObject.inPool) return;
			if(poolObject.original==null) return;
			GetPool(poolObject.original).AddToDisabled(poolObject);
		}
		
		internal static void BringPools()
		{
			Debug.Log("BringPools");
			foreach (var pair in ins.pools)
			{
				pair.Value.BringPool();
			}
		}
		
		internal static void ClearPools()
		{
			Debug.Log("ClearPools");
			foreach (var pair in ins.pools)
			{
				pair.Value.ClearPool();
			}
		}
		
		internal static void ExpirePools()
		{
			Debug.Log("ExpirePools");
			foreach (var pair in ins.pools)
			{
				pair.Value.ExpirePool();
			}
		}
		#endregion
	}
}