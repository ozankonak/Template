using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
	public static class ExtTask
	{
		#if UNITY_EDITOR
		static ExtTask()
		{
			UnityEditor.EditorApplication.playModeStateChanged += playModeStateChanged;
			ResetGlobalToken();
		}
		
		private static void playModeStateChanged(UnityEditor.PlayModeStateChange state)
		{
			if(state!=UnityEditor.PlayModeStateChange.EnteredEditMode) return;
			ResetGlobalToken();
		}
		#endif
		
		public static CancellationTokenSource globalToken = new CancellationTokenSource();
		private static void ResetGlobalToken()
		{
			if(!Application.isBatchMode && globalToken!=null && !globalToken.IsCancellationRequested)
			{
				//Debug.Log($"ResetGlobalToken");
				globalToken.Cancel(false);
			}
			globalToken = new CancellationTokenSource();
		}
		
		[Obsolete("use NoWait()")]
		public static void Skip(this Task task){}
		public static void NoWait(this Task task){}
		public static async Task CaptureErrors(this Task task)
		{
			try
			{
				await task;
			}
			catch(Exception)
			{
				foreach (var item in task.Exception.InnerExceptions)
				{
					Debug.LogException(item);
				}
			}
		}
		
		public static async Task WaitUntil(Func<bool> predicate, int sleep = 100,UnityAction call=null)
		{
			globalToken.Token.ThrowIfCancellationRequested();
			while (!predicate())
			{
				await Task.Delay(sleep,globalToken.Token);
			}
			globalToken.Token.ThrowIfCancellationRequested();
			call?.Invoke();
		}
		
		public static async Task WaitFrames(int delayFrames=1,int sleep=1,UnityAction call=null)
		{
			float endFrame = Time.frameCount+delayFrames-1;
			await ExtTask.WaitUntil(()=>(Time.frameCount>endFrame),sleep:sleep);
			call?.Invoke();
		}
		
		public static async Task WaitSeconds(float delaySeconds=0,bool unscaled=false,UnityAction call=null)
		{
			if(unscaled)
			{
				float endTime = Time.unscaledTime+delaySeconds;
				await ExtTask.WaitUntil(()=>(Time.unscaledTime>endTime),sleep:1);
			}
			else
			{
				float endTime = Time.time+delaySeconds;
				await ExtTask.WaitUntil(()=>(Time.time>endTime),sleep:1);
			}
			call?.Invoke();
		}
		
		public static Task WaitInstant()
		{
			return Task.CompletedTask;
		}
	}
}