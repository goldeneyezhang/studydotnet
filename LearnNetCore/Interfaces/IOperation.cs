using System;
using System.Collections.Generic;
using System.Text;

namespace LearnNetCore.Interfaces
{
	public interface IOperation
	{
		Guid OperationId { get; }
	}
	public interface IOperationTransient : IOperation
	{

	}
	public interface IOperationScoped : IOperation
	{

	}
	public interface IOperationSingleton : IOperation
	{

	}
	public interface IOperationSingletonInstance : IOperation
	{

	}
	public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton, IOperationSingletonInstance
	{
		private Guid _guid;
		public Operation(Guid guid )
		{
			this._guid = guid;
		}
		public Operation()
		{
			_guid = Guid.NewGuid();
		}
		Guid IOperation.OperationId => this._guid;
	}
}
