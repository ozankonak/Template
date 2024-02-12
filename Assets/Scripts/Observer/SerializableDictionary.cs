using System;
using System.Collections.Generic;
using UnityEngine;

namespace Observer
{
	[Serializable]
	public class SerializableDictionary<TypeKey,TypeValue> : Dictionary<TypeKey, TypeValue>, ISerializationCallbackReceiver
	{
		private TypeValue defaultValue;
		[SerializeField]private Pair<TypeKey,TypeValue>[] array = Array.Empty<Pair<TypeKey, TypeValue>>();

		public TypeValue GetValue(TypeKey key)
		{
			return GetValue(key,defaultValue);
		}
		
		public TypeValue GetValue(TypeKey key,TypeValue defaultCustom)
		{
			if(key==null) return defaultCustom;
			return ContainsKey(key) ? base[key] : defaultCustom;
		}
		
		async void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Clear();
			if(array == null) return;
			if(array.Length == 0) return;
			
			foreach (var dic in array)
			{
#if UNITY_EDITOR && CanDebug
				if(base.ContainsKey(array[i].Key))
				{
					string stringKey = array[i].Key as string;
					string stringValue = array[i].Value as string;
					Debug.LogWarning("SerializedDict Overrides:"+stringKey+":"+stringValue);
				}
#endif
				base[dic.Key] = dic.Value;
			}
			
			await System.Threading.Tasks.Task.Yield();
			//#if !UNITY_EDITOR
			//if(Application.isPlaying) array = null;
			//#endif
		}
		
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			//#if ODIN_INSPECTOR
			if(Count==0) return;
			array = new Pair<TypeKey,TypeValue>[Count];
			int index = 0;
			foreach (KeyValuePair<TypeKey,TypeValue> item in this)
			{
				array[index] = new Pair<TypeKey,TypeValue>(item.Key,item.Value);
				index++;
			}
			//#endif
		}
	}
	
	[Serializable]
	public class Pair<TypeKey,TypeValue>
	{
		public Pair(){}
		public Pair(TypeKey key,TypeValue value)
		{
			this.key = key;
			this.value = value;
		}

		[SerializeField] private TypeKey key;
		public TypeKey Key
		{
			get => key;
			set => key = value;
		}

		[SerializeField] private TypeValue value;
		public TypeValue Value
		{
			get => value;
			set => this.value = value;
		}
	}
}