using System;
using System.Collections.Generic;
using System.Text;

namespace LearnNetCore.Interfaces
{
    public class OperationService
    {
		public IOperationTransient TransientOperation { get; }
		public IOperationScoped ScopedOperation { get; }
		public IOperationSingleton SingletonOperation { get; }
		public IOperationSingletonInstance SingletonInstanceOperation { get; }
		
		public OperationService(IOperationTransient transientOperation,IOperationScoped scopedOperation,IOperationSingleton singletonOperation,IOperationSingletonInstance instanceOperation)
		{
			this.TransientOperation = transientOperation;
			this.ScopedOperation = scopedOperation;
			this.SingletonOperation = singletonOperation;
			this.SingletonInstanceOperation = instanceOperation;

		}
    }
}
