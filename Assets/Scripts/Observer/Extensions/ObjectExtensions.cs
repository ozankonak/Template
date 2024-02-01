namespace Observer.Extensions {
    public static class ObjectExtensions {
        
        public static void Subscribe<T>(this object obj, System.Action<T> action, object subscriber, int priority = 100) where T : BaseEvent<T> {
            EventManager.Subscribe(action, subscriber, obj, priority);
        }
    }
}