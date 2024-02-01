﻿using System;

namespace Observer {    

    [Serializable]
    public class OrderedElement<T> {
        
        public int order;
        public T element;
        public OrderedElement(int order, T element){
            this.order   = order;
            this.element = element;
        }
    }

}