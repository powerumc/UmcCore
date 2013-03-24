using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy
{
    public abstract class DynamicContext
    {
        private IDictionary<object, DynamicContext> contexts = new Dictionary<object, DynamicContext>();
        
        public IDictionary<object, DynamicContext> Contexts
        {
            get { return this.contexts; }
        }

        protected DynamicContext Get(object key)
        {
            if (this.contexts.ContainsKey(key))
            {
                return this.Contexts[key];
            }

            throw new KeyNotFoundException(key.ToString());
        }

        protected IEnumerable<DynamicContext> Gets()
        {
            return this.contexts.Values.AsEnumerable();
        }
    }
}
