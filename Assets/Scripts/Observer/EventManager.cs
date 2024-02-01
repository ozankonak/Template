using System;
using System.Collections.Generic;
using System.Reflection;

namespace Observer {
    public static class EventManager {
        
        private static List<Type> _subscribedEventTypes = new List<Type>();
        
        public static void Subscribe<TEvent>(Action<TEvent> callback, object subscriber,
                                             int            priority = 100) where TEvent : BaseEvent<TEvent> {
            BaseEvent<TEvent>.Subscribe(callback, subscriber, priority);
            
            var genericClassType = typeof(BaseEvent<>).MakeGenericType(typeof(TEvent));
            
            if (!_subscribedEventTypes.Contains(genericClassType))
                _subscribedEventTypes.Add(genericClassType);
        }
        public static void Subscribe<TEvent>(Action<TEvent> callback, object subscriber, object sender,
                                             int            priority = 100) where TEvent : BaseEvent<TEvent> {
            BaseEvent<TEvent>.Subscribe(callback, subscriber, sender, priority);
            
            var genericClassType = typeof(BaseEvent<>).MakeGenericType(typeof(TEvent));
            
            if (!_subscribedEventTypes.Contains(genericClassType))
                _subscribedEventTypes.Add(genericClassType);
        }
        
        public static void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : BaseEvent<TEvent> {
            BaseEvent<TEvent>.Unsubscribe(callback);
        }
        
        public static void Unsubscribe(object subscriber){
            foreach (var eventType in _subscribedEventTypes) {
                var unsubscribeMethod = eventType.GetMethod("Unsubscribe", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(object) }, null);

                unsubscribeMethod?.Invoke(null, new[]{ subscriber });
            }
        }


        public static void Subscribe(Type eventType, Action callback, object subscriber, int priority = 100)
        {
            Type genericClassType = typeof(BaseEvent<>).MakeGenericType(eventType);
            
            var subscribeMethod = genericClassType.GetMethod("Subscribe", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(Action), typeof(object), typeof(int) }, null);
            
            subscribeMethod?.Invoke(null, new[] { callback, subscriber, priority });

            if (!_subscribedEventTypes.Contains(genericClassType))
                _subscribedEventTypes.Add(genericClassType);
        }

    }

}
