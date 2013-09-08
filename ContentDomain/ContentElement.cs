﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDomain
{
    public sealed class ContentElement<T>
        where T : class, IContentValue
    {
        private T _value;

        public void SetValue(T value)
        {
            if (value == null) throw new ArgumentNullException("value");
            _value = value;
        }

        public T GetValue()
        {
            return _value;
        }
    }
}
