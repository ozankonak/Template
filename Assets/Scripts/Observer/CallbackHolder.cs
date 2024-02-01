using System;

namespace Observer {
    public class CallbackHolder<T> {

        public Action<T> callback;

        public Action noParamCallback;
        
        public object subscriber;
        
        public object sender;
        public bool IsAlive() {
            if (subscriber is Object unityObject) {
                return unityObject.ToString() != "null";
            }

            return subscriber != null;
        }
        
        public override bool Equals(object obj) {
            if (obj is Action<T> callback) {
                return Equals(callback);
            }

            if (obj is Action noParamCallback) {
                return Equals(noParamCallback);
            }

            if (obj is CallbackHolder<T> holder) {
                if (holder.callback != null)
                    return Equals(holder.callback);
                if (holder.subscriber != null)
                    return subscriber == holder.subscriber;
            }

            return base.Equals(obj);
        }
        
        public bool Equals(Action<T> callback) {
            return callback == this.callback;
        }
        
        public bool Equals(Action callback) {
            return callback == this.noParamCallback;
        }
    }

}