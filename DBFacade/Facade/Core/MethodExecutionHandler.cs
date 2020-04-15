using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFacade.Facade.Core
{
    public interface ITransaction
    {
        IDbResponse Execute();
        IDbResponse Mock(object returnValue = null);
    }
    public interface ITransaction<TDbParams>
        where TDbParams : IDbParamsModel
    {
        IDbResponse Execute(TDbParams parameters);
        IDbResponse Mock(TDbParams parameters, object returnValue = null);
    }
    public interface IFetch<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        IDbResponse<TDbDataModel> Execute();
        IDbResponse<TDbDataModel> Mock<T>(IEnumerable<T> responseData, object returnValue = null);
        IDbResponse<TDbDataModel> Mock<T>(T responseData, object returnValue = null);
    }
    public interface IFetch<TDbDataModel, TDbParams>
        where TDbDataModel : DbDataModel
        where TDbParams : IDbParamsModel
    {
        IDbResponse<TDbDataModel> Execute(TDbParams parameters);
        IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, object returnValue = null);
        IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, T responseData, object returnValue = null);
    }
    public interface IGenericFetch<TDbDataModel, T1>: IFetch<TDbDataModel, DbParamsModel<T1>>
        where TDbDataModel : DbDataModel
    {
        IDbResponse<TDbDataModel> Execute(T1 param1);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, IEnumerable<T> responseData, object returnValue = null);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T responseData, object returnValue = null);
    }
    public interface IGenericFetch<TDbDataModel, T1, T2> : IFetch<TDbDataModel, DbParamsModel<T1, T2>>
        where TDbDataModel : DbDataModel
    {
        IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, IEnumerable<T> responseData, object returnValue = null);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T responseData, object returnValue = null);
    }
    public interface IGenericFetch<TDbDataModel, T1, T2,T3> : IFetch<TDbDataModel, DbParamsModel<T1, T2,T3>>
        where TDbDataModel : DbDataModel
    {
        IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2, T3 param3);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, IEnumerable<T> responseData, object returnValue = null);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T responseData, object returnValue = null);
    }
    public interface IGenericFetch<TDbDataModel, T1, T2, T3, T4> : IFetch<TDbDataModel, DbParamsModel<T1, T2, T3,T4>>
        where TDbDataModel : DbDataModel
    {
        IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2, T3 param3, T4 param4);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, IEnumerable<T> responseData, object returnValue = null);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T responseData, object returnValue = null);
    }
    public interface IGenericFetch<TDbDataModel, T1, T2, T3, T4,T5> : IFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4,T5>>
        where TDbDataModel : DbDataModel
    {
        IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, IEnumerable<T> responseData, object returnValue = null);
        IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T responseData, object returnValue = null);
    }

    public interface IGenericTransaction<T1> : ITransaction<DbParamsModel<T1>>
    {
        IDbResponse Execute(T1 param1);
        IDbResponse Mock(T1 param1, object returnValue = null);
    }
    public interface IGenericTransaction<T1, T2> : ITransaction<DbParamsModel<T1, T2>>
    {
        IDbResponse Execute(T1 param1, T2 param2);
        IDbResponse Mock(T1 param1, T2 param2, object returnValue = null);
    }
    public interface IGenericTransaction<T1, T2, T3> : ITransaction<DbParamsModel<T1, T2, T3>>
        
    {
        IDbResponse Execute(T1 param1, T2 param2, T3 param3);
        IDbResponse Mock(T1 param1, T2 param2, T3 param3, object returnValue = null);
    }
    public interface IGenericTransaction<T1, T2, T3, T4> : ITransaction<DbParamsModel<T1, T2, T3, T4>>
        
    {
        IDbResponse Execute(T1 param1, T2 param2, T3 param3, T4 param4);
        IDbResponse Mock(T1 param1, T2 param2, T3 param3, T4 param4, object returnValue = null);
    }
    public interface IGenericTransaction<T1, T2, T3, T4, T5> : ITransaction<DbParamsModel<T1, T2, T3, T4, T5>>
        
    {
        IDbResponse Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
        IDbResponse Mock(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, object returnValue = null);
    }

    internal class Fetch<TDbDataModel, TDbParams>: IFetch<TDbDataModel, TDbParams>
        where TDbDataModel : DbDataModel
        where TDbParams : IDbParamsModel
    {
        internal delegate IDbResponse<TDbDataModel> ExecutionDelegate(TDbParams parameters);
        private ExecutionDelegate Handler { get; set; }
        public Fetch(ExecutionDelegate handler)
        {
            Handler = handler;
        }

        public IDbResponse<TDbDataModel> Execute(TDbParams parameters) => Handler(parameters);
        public IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, object returnValue=null)
        {
            parameters.RunAsTest(responseData, returnValue);
            return Execute(parameters);
        }
        public IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, T responseData, object returnValue = null)
        {
            parameters.RunAsTest(responseData, returnValue);
            return Execute(parameters);
        }
    }
    internal class GenericFetch<TDbDataModel, T1> : Fetch<TDbDataModel, DbParamsModel<T1>>, IGenericFetch<TDbDataModel, T1>
        where TDbDataModel : DbDataModel
    {
        public GenericFetch(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse<TDbDataModel> Execute(T1 param1) 
            => Execute(new DbParamsModel<T1>(param1));

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, IEnumerable<T> responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1>(param1), responseData, returnValue);

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1>(param1), responseData, returnValue);
    }
    internal class GenericFetch<TDbDataModel, T1,T2> : Fetch<TDbDataModel, DbParamsModel<T1,T2>>, IGenericFetch<TDbDataModel, T1,T2>
        where TDbDataModel : DbDataModel
    {
        public GenericFetch(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2)
            => Execute(new DbParamsModel<T1,T2>(param1, param2));

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, IEnumerable<T> responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1,T2>(param1, param2), responseData, returnValue);

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1,T2>(param1, param2), responseData, returnValue);
    }
    internal class GenericFetch<TDbDataModel, T1, T2, T3> : Fetch<TDbDataModel, DbParamsModel<T1, T2, T3>>, IGenericFetch<TDbDataModel, T1, T2, T3>
        where TDbDataModel : DbDataModel
    {
        public GenericFetch(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2, T3 param3)
            => Execute(new DbParamsModel<T1, T2, T3>(param1, param2, param3));

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, IEnumerable<T> responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3>(param1, param2, param3), responseData, returnValue);

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3>(param1, param2, param3), responseData, returnValue);
    }
    internal class GenericFetch<TDbDataModel, T1, T2, T3, T4> : Fetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4>>, IGenericFetch<TDbDataModel, T1, T2, T3, T4>
        where TDbDataModel : DbDataModel
    {
        public GenericFetch(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2, T3 param3, T4 param4)
            => Execute(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4));

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, IEnumerable<T> responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), responseData, returnValue);

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), responseData, returnValue);
    }
    internal class GenericFetch<TDbDataModel, T1, T2, T3,T4,T5> : Fetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>>, IGenericFetch<TDbDataModel, T1, T2, T3, T4, T5>
        where TDbDataModel : DbDataModel
    {
        public GenericFetch(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse<TDbDataModel> Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            => Execute(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, IEnumerable<T> responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), responseData, returnValue);

        public IDbResponse<TDbDataModel> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T responseData, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), responseData, returnValue);
    }

    internal class Fetch<TDbDataModel>: IFetch<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        internal delegate IDbResponse<TDbDataModel> ExecutionDelegate();
        internal delegate IDbResponse<TDbDataModel> MockExecutionDelegate(DbParamsModel parameters);
        private ExecutionDelegate Handler { get; set; }
        private MockExecutionDelegate MockHandler { get; set; }
        public Fetch(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            Handler = handler;
            MockHandler = mockHandler;
        }
        public IDbResponse<TDbDataModel> Execute() => Handler();
        public IDbResponse<TDbDataModel> Mock<T>(IEnumerable<T> responseData, object returnValue = null)
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return MockHandler(parameters);
        }
        public IDbResponse<TDbDataModel> Mock<T>(T responseData, object returnValue = null)
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return MockHandler(parameters);
        }
    }

    internal class Transaction<TDbParams> : ITransaction<TDbParams>
        where TDbParams : IDbParamsModel
    {
        internal delegate IDbResponse ExecutionDelegate(TDbParams parameters);
        private ExecutionDelegate Handler { get; set; }
        public Transaction(ExecutionDelegate handler)
        {
            Handler = handler;
        }

        public IDbResponse Execute(TDbParams parameters) => Handler(parameters);
        public IDbResponse Mock(TDbParams parameters, object returnValue = null)
        {
            parameters.RunAsTest(returnValue);
            return Execute(parameters);
        }
    }
    internal class GenericTransaction<T1> : Transaction<DbParamsModel<T1>>, IGenericTransaction<T1>
    {
        public GenericTransaction(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse Execute(T1 param1)
            => Execute(new DbParamsModel<T1>(param1));

        public IDbResponse Mock(T1 param1, object returnValue = null)
            => Mock(new DbParamsModel<T1>(param1), returnValue);
    }
    internal class GenericTransaction<T1, T2> : Transaction<DbParamsModel<T1, T2>>, IGenericTransaction<T1, T2>
    {
        public GenericTransaction(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse Execute(T1 param1, T2 param2)
            => Execute(new DbParamsModel<T1, T2>(param1, param2));

        public IDbResponse Mock(T1 param1, T2 param2, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2>(param1, param2), returnValue);
    }
    internal class GenericTransaction<T1, T2, T3> : Transaction<DbParamsModel<T1, T2, T3>>, IGenericTransaction<T1, T2, T3>
    {
        public GenericTransaction(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse Execute(T1 param1, T2 param2, T3 param3)
            => Execute(new DbParamsModel<T1, T2, T3>(param1, param2, param3));

        public IDbResponse Mock(T1 param1, T2 param2, T3 param3, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3>(param1, param2, param3), returnValue);
    }
    internal class GenericTransaction<T1, T2, T3, T4> : Transaction<DbParamsModel<T1, T2, T3, T4>>, IGenericTransaction<T1, T2, T3, T4>
    {
        public GenericTransaction(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse Execute(T1 param1, T2 param2, T3 param3, T4 param4)
            => Execute(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4));

        public IDbResponse Mock(T1 param1, T2 param2, T3 param3, T4 param4, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), returnValue);
    }
    internal class GenericTransaction<T1, T2, T3, T4, T5> : Transaction<DbParamsModel<T1, T2, T3, T4, T5>>, IGenericTransaction<T1, T2, T3, T4, T5>
    {
        public GenericTransaction(ExecutionDelegate handler) : base(handler) { }
        public IDbResponse Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            => Execute(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));

        public IDbResponse Mock(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, object returnValue = null)
            => Mock(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), returnValue);
    }

    internal class Transaction : ITransaction
    {
        internal delegate IDbResponse ExecutionDelegate();
        internal delegate IDbResponse MockExecutionDelegate(DbParamsModel parameters);
        private ExecutionDelegate Handler { get; set; }
        private MockExecutionDelegate MockHandler { get; set; }
        public Transaction(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            Handler = handler;
            MockHandler = mockHandler;
        }
        public IDbResponse Execute() => Handler();
        public IDbResponse Mock(object returnValue = null)
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(returnValue);
            return MockHandler(parameters);
        }
    }
}
