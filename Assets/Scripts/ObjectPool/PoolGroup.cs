using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace ObjectPool
{
	internal class PoolGroup : MonoBehaviour
	{
		internal PoolObject original = null;
		private readonly Queue<PoolObject> stackPool = new Queue<PoolObject>();
		private readonly Queue<PoolObject> stackDisabled = new Queue<PoolObject>();
		private readonly List<PoolObject> listAll = new List<PoolObject>();
		
		private float lastGetTime = 0f;
		internal GameObject Spawn()
		{
			pointStart:
			if(stackPool.Count==0) GrowInstant(original.countInitial);
			while (stackPool.Count>0)
			{
				PoolObject temp = stackPool.Dequeue();
				if(temp==null)
				{
					Debug.LogException(new UnityException($"stackPool:pop:{original.name}"),original);
					continue;
				}
				lastGetTime = Time.time;
				temp.inPool = false;
				return temp.gameObject;
			}
			goto pointStart;
		}
		
		private static readonly System.Random random = new System.Random();
		private bool growAsync = false;
		private async Task GrowAsync()
		{
			if(growAsync) return;
			growAsync = true;
			//Debug.Log($"GrowAsync:Start:{original.name}:{original.countInitial}",original);
			while(growAsync)
			{
				await ExtTask.WaitFrames(random.Next(5,15));
				if(!growAsync) break;
				if(!Application.isPlaying) break;
				if(listAll.Count>=original.countInitial) break;
				PoolObject clone = Instantiate(original,this.transform);
				clone.original = original.gameObject;
				listAll.Add(clone);
				clone.gameObject.name = $"{original.name}_{listAll.Count}";
				Pool(clone);
				//Debug.Log($"GrowAsync:{clone.name}:{listAll.Count}",clone);
			}
			growAsync = false;
		}
		
		internal void GrowInstant(byte countInitial)
		{
			byte growCount = countInitial;
			if(growCount>4) growCount = 4;
			for (byte i = 0; i < growCount; i++)
			{
				PoolObject clone = Instantiate(original,this.transform);
				clone.original = original.gameObject;
				listAll.Add(clone);
				clone.gameObject.name = $"{original.name}_{listAll.Count}";
				Pool(clone);
			}
			
			if(original.countInitial<listAll.Count)
			{
				original.countInitial = (byte)(listAll.Count);
				#if UNITY_EDITOR
				UnityEditor.EditorUtility.SetDirty(original.gameObject);
				#endif
				Debug.LogWarning($"Pool:countInitial:{original.name}:{listAll.Count}",original);
			}
			
			if(hasExpire)
			{
				hasExpire = false;
				original.minuteClear += 5;
				#if UNITY_EDITOR
				UnityEditor.EditorUtility.SetDirty(original.gameObject);
				#endif
				Debug.LogWarning($"Pool:minuteClear:{original.name}:{original.minuteClear}",original);
			}
			
			//Debug.Log($"Pool:GrowInstant:{original.name}:{listAll.Count}",original);
			if(growCount>1) GrowAsync().NoWait();
		}
		
		internal void Pool(PoolObject clone)
		{
			if(clone==null) return;
			if(clone.inPool) return;
			clone.inPool = true;
			clone.transform.SetParent(transform);
			clone.transform.localPosition = Vector3.zero;
			clone.gameObject.SetActive(false);
			
			if(stackPool.Contains(clone))
			{
				Debug.LogException(new UnityException($"stackPool:has:{clone.name}"),clone);
				return;
			}
			stackPool.Enqueue(clone);
		}
		
		internal void BringPool()
		{
			if(listAll.Count==0) return;
			listAll.RemoveAll((x)=>x==null);
			foreach (var item in listAll)
			{
				if(stackPool.Contains(item)) continue;
				Pool(item);
			}
		}
		
		internal void ClearPool()
		{
			//if(listAll.Count<8) return;
			Debug.Log($"ClearPool!:{this.gameObject.name}",original);
			growAsync = false;
			foreach (var item in listAll)
			{
				if(item==null) continue;
				if(item.gameObject==null) continue;
				Destroy(item.gameObject);
			}
			listAll.Clear();
			stackPool.Clear();
			stackDisabled.Clear();
		}
		
		private bool hasExpire = false;
		internal void ExpirePool()
		{
			if(listAll.Count==0) return;
			if(original.minuteClear>0 && (Time.time-lastGetTime)>(original.minuteClear*60f))
			{
				hasExpire = true;
				ClearPool();
				return;
			}
		}
		
		internal void AddToDisabled(PoolObject pooledObject)
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying) return;
			if(Application.isPlaying && !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) return; //ExitingPlayMode
			#endif
			
			if(stackDisabled.Contains(pooledObject))
			{
				Debug.LogWarning(new UnityException($"stackDisabled:has:{pooledObject.name}"),pooledObject);
				return;
			}
			stackDisabled.Enqueue(pooledObject);
			this.enabled = true;
		}
		
		private void LateUpdate()
		{
			while (stackDisabled.Count>0)
			{
				Pool(stackDisabled.Dequeue());
			}
			stackDisabled.Clear();
			this.enabled = false;
		}
	}
}