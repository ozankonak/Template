using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Observer {
    
    public abstract class BaseEvent<T> where T : BaseEvent<T>{
        
        private static OrderedList<CallbackHolder<T>> _callbacks = new OrderedList<CallbackHolder<T>>();
        
        public object sender;
        public void Trigger(object sender) {
            this.sender = sender;

            #region Remove destroyed subscribers

            var toBeRemoved = new List<CallbackHolder<T>>();

            foreach (var callback in _callbacks.Select(n => n.element).Where(n => !n.IsAlive())) {
                Debug.LogWarning($"[EventManager] Trying to trigger event '{typeof(T)}', but a subscriber has been destroyed");
                toBeRemoved.Add(callback);
            }

            
            foreach (var callback in toBeRemoved) {
                _callbacks.Remove(callback);
            }
            
            #endregion

            #region Trigger callbacks

            foreach (var callbackHolder in _callbacks.Select(n => n.element)) {

                if (callbackHolder.sender != null && callbackHolder.sender != sender)
                    continue;
                
                callbackHolder.callback?.Invoke(this as T);
                callbackHolder.noParamCallback?.Invoke();
            }

            #endregion
        }
        

        #if KD_UNIVERSAL_TIME
        public void Trigger(object sender, float delay, bool pausable = true) {
            UniversalTime.Instance.StartTimer(delay, () => Trigger(sender), pausable);
        }
        #endif
        
        public static void Subscribe(Action<T> callback, object subscriber, int priority = 100) {
            _callbacks.Add(priority, new CallbackHolder<T>{ callback = callback, subscriber = subscriber });
        }
        
        public static void Subscribe(Action<T> callback, object subscriber, object sender, int priority) {
            _callbacks.Add(priority, new CallbackHolder<T>{ callback = callback, subscriber = subscriber, sender = sender});
        }
        
        public static void Subscribe(Action noParamCallback, object subscriber, int priority) {
            _callbacks.Add(priority, new CallbackHolder<T>{ noParamCallback = noParamCallback, subscriber = subscriber });
        }
        
        public static void Unsubscribe(Action<T> callback) {
            _callbacks.RemoveAll(new CallbackHolder<T>{ callback = callback });
        }
        
        public static void Unsubscribe(Action callback) {
            _callbacks.RemoveAll(new CallbackHolder<T>{ noParamCallback = callback });
        }
        
        public static void Unsubscribe(object subscriber) {
            _callbacks.RemoveAll(new CallbackHolder<T>{ subscriber = subscriber });
        }
        
        public bool TryGetSenderGameObject(out GameObject senderGameObject) {
            senderGameObject = null;
            
            if (sender == null) return false;

            if (sender is GameObject go) {
                senderGameObject = go;
                return true;
            }
            
            if (sender is Component component) {
                senderGameObject = component.gameObject;
                return true;
            }

            return false;
        }

    }
}