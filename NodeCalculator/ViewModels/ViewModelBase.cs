using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NodeCalculator.ViewModels
{
    class ViewModelBase : Models.BindableBase, IDisposable
    {
        protected List<IDisposable> container = new List<IDisposable>();


        public virtual void Start()
        {
            
        }

        public virtual void End()
        {
            
        }

        public virtual void Dispose()
        {
            foreach (var disposable in container)
            {
                disposable.Dispose();
            }

            container.Clear();
        }
    }
}
