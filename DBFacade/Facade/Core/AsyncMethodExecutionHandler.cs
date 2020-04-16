using DBFacade.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacade.Facade.Core
{
    public interface IAsyncTransaction
    {
        Task<IDbResponse> Execute();
        Task<IDbResponse> Mock(object returnValue = null);
    }
    public interface IAsyncTransaction<TDbParams>
        where TDbParams : IDbParamsModel
    {
        Task<IDbResponse> Execute(TDbParams parameters);
        Task<IDbResponse> Mock(TDbParams parameters, object returnValue = null);
    }
    public interface IAsyncGenericTransaction<T1> : IAsyncTransaction<DbParamsModel<T1>>
    {
        Task<IDbResponse> Execute(T1 param1);
        Task<IDbResponse> Mock(T1 param1, object returnValue = null);
    }
    public interface IAsyncGenericTransaction<T1, T2> : IAsyncTransaction<DbParamsModel<T1, T2>>
    {
        Task<IDbResponse> Execute(T1 param1, T2 param2);
        Task<IDbResponse> Mock(T1 param1, T2 param2, object returnValue = null);
    }
    public interface IAsyncGenericTransaction<T1, T2, T3> : IAsyncTransaction<DbParamsModel<T1, T2, T3>>

    {
        Task<IDbResponse> Execute(T1 param1, T2 param2, T3 param3);
        Task<IDbResponse> Mock(T1 param1, T2 param2, T3 param3, object returnValue = null);
    }
    public interface IAsyncGenericTransaction<T1, T2, T3, T4> : IAsyncTransaction<DbParamsModel<T1, T2, T3, T4>>

    {
        Task<IDbResponse> Execute(T1 param1, T2 param2, T3 param3, T4 param4);
        Task<IDbResponse> Mock(T1 param1, T2 param2, T3 param3, T4 param4, object returnValue = null);
    }
    public interface IAsyncGenericTransaction<T1, T2, T3, T4, T5> : IAsyncTransaction<DbParamsModel<T1, T2, T3, T4, T5>>

    {
        Task<IDbResponse> Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
        Task<IDbResponse> Mock(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, object returnValue = null);
    }
    

    public interface IAsyncFetch<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> Execute();
        Task<IDbResponse<TDbDataModel>> Mock<T>(IEnumerable<T> responseData, object returnValue = null);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T responseData, object returnValue = null);
    }
    public interface IAsyncFetch<TDbDataModel, TDbParams>
        where TDbDataModel : DbDataModel
        where TDbParams : IDbParamsModel
    {
        Task<IDbResponse<TDbDataModel>> Execute(TDbParams parameters);
        Task<IDbResponse<TDbDataModel>> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, object returnValue = null);
        Task<IDbResponse<TDbDataModel>> Mock<T>(TDbParams parameters, T responseData, object returnValue = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1> : IAsyncFetch<TDbDataModel, DbParamsModel<T1>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> Execute(T1 param1);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, IEnumerable<T> responseData, object returnValue = null);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T responseData, object returnValue = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, IEnumerable<T> responseData, object returnValue = null);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T responseData, object returnValue = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2, T3> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2, T3 param3);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, IEnumerable<T> responseData, object returnValue = null);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T responseData, object returnValue = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2, T3 param3, T4 param4);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, IEnumerable<T> responseData, object returnValue = null);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T responseData, object returnValue = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4, T5> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, IEnumerable<T> responseData, object returnValue = null);
        Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T responseData, object returnValue = null);
    }

    internal class AsyncFetch<TDbDataModel> : IAsyncFetch<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        internal delegate Task<IDbResponse<TDbDataModel>> ExecutionDelegate();
        internal delegate Task<IDbResponse<TDbDataModel>> MockExecutionDelegate(DbParamsModel parameters);
        private ExecutionDelegate Handler { get; }
        private MockExecutionDelegate MockHandler { get; }
        public AsyncFetch(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            Handler = handler;
            MockHandler = mockHandler;
        }
        public async Task<IDbResponse<TDbDataModel>> Execute() => await Handler();
        public async Task<IDbResponse<TDbDataModel>> Mock<T>(IEnumerable<T> responseData, object returnValue = null)
        {
            var parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await MockHandler(parameters);
        }
        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T responseData, object returnValue = null)
        {
            var parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await MockHandler(parameters);
        }
    }
    internal class AsyncFetch<TDbDataModel, TDbParams> : IAsyncFetch<TDbDataModel, TDbParams>
        where TDbDataModel : DbDataModel
        where TDbParams : IDbParamsModel
    {
        internal delegate Task<IDbResponse<TDbDataModel>> ExecutionDelegate(TDbParams parameters);
        private ExecutionDelegate Handler { get;  }
        public AsyncFetch(ExecutionDelegate handler)
        {
            Handler = handler;
        }
        public async Task<IDbResponse<TDbDataModel>> Execute(TDbParams parameters) => await Handler(parameters);
        public async Task<IDbResponse<TDbDataModel>> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, object returnValue = null)
        {
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await Execute(parameters);
        }
        public async Task<IDbResponse<TDbDataModel>> Mock<T>(TDbParams parameters, T responseData, object returnValue = null)
        {
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await Execute(parameters);
        }
    }
    internal class GenericAsyncFetch<TDbDataModel, T1> : AsyncFetch<TDbDataModel, DbParamsModel<T1>>, IAsyncGenericFetch<TDbDataModel, T1>
        where TDbDataModel : DbDataModel
    {
        public GenericAsyncFetch(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse<TDbDataModel>> Execute(T1 param1)
            => await Execute(new DbParamsModel<T1>(param1));

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, IEnumerable<T> responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1>(param1), responseData, returnValue);

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1>(param1), responseData, returnValue);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2>>, IAsyncGenericFetch<TDbDataModel, T1, T2>
        where TDbDataModel : DbDataModel
    {
        public GenericAsyncFetch(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2)
            => await Execute(new DbParamsModel<T1, T2>(param1, param2));

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, IEnumerable<T> responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2>(param1, param2), responseData, returnValue);

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2>(param1, param2), responseData, returnValue);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2, T3> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3>>, IAsyncGenericFetch<TDbDataModel, T1, T2, T3>
        where TDbDataModel : DbDataModel
    {
        public GenericAsyncFetch(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2, T3 param3)
            => await Execute(new DbParamsModel<T1, T2, T3>(param1, param2, param3));

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, IEnumerable<T> responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3>(param1, param2, param3), responseData, returnValue);

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3>(param1, param2, param3), responseData, returnValue);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4>>, IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4>
        where TDbDataModel : DbDataModel
    {
        public GenericAsyncFetch(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2, T3 param3, T4 param4)
            => await Execute(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4));

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, IEnumerable<T> responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), responseData, returnValue);

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), responseData, returnValue);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4, T5> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>>, IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4, T5>
        where TDbDataModel : DbDataModel
    {
        public GenericAsyncFetch(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse<TDbDataModel>> Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            => await Execute(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, IEnumerable<T> responseData, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), responseData, returnValue);

        public async Task<IDbResponse<TDbDataModel>> Mock<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T responseData, object returnValue = null)
            =>await
 Mock(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), responseData, returnValue);
    }



    internal class AsyncTransaction : IAsyncTransaction
    {
        internal delegate Task<IDbResponse> ExecutionDelegate();
        internal delegate Task<IDbResponse> MockExecutionDelegate(DbParamsModel parameters);
        private ExecutionDelegate Handler { get; }
        private MockExecutionDelegate MockHandler { get; }
        public AsyncTransaction(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            Handler = handler;
            MockHandler = mockHandler;
        }
        public async Task<IDbResponse> Execute() => await Handler();
        public async Task<IDbResponse> Mock(object returnValue = null)
        {
            var parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(returnValue));
            return await MockHandler(parameters);
        }
    }
    internal class AsyncTransaction<TDbParams> : IAsyncTransaction<TDbParams>
        where TDbParams : IDbParamsModel
    {
        internal delegate Task<IDbResponse> ExecutionDelegate(TDbParams parameters);
        private ExecutionDelegate Handler { get; }
        public AsyncTransaction(ExecutionDelegate handler)
        {
            Handler = handler;
        }

        public async Task<IDbResponse> Execute(TDbParams parameters) => await Handler(parameters);
        public async Task<IDbResponse> Mock(TDbParams parameters, object returnValue = null)
        {
            await Task.Run(() => parameters.RunAsTest(returnValue));
            return await Execute(parameters);
        }
    }
    internal class GenericAsyncTransaction<T1> : AsyncTransaction<DbParamsModel<T1>>, IAsyncGenericTransaction<T1>
    {
        public GenericAsyncTransaction(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse> Execute(T1 param1)
            => await Execute(new DbParamsModel<T1>(param1));

        public async Task<IDbResponse> Mock(T1 param1, object returnValue = null)
            => await Mock(new DbParamsModel<T1>(param1), returnValue);
    }
    internal class GenericAsyncTransaction<T1, T2> : AsyncTransaction<DbParamsModel<T1, T2>>, IAsyncGenericTransaction<T1, T2>
    {
        public GenericAsyncTransaction(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse> Execute(T1 param1, T2 param2)
            => await Execute(new DbParamsModel<T1, T2>(param1, param2));

        public async Task<IDbResponse> Mock(T1 param1, T2 param2, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2>(param1, param2), returnValue);
    }
    internal class GenericAsyncTransaction<T1, T2, T3> : AsyncTransaction<DbParamsModel<T1, T2, T3>>, IAsyncGenericTransaction<T1, T2, T3>
    {
        public GenericAsyncTransaction(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse> Execute(T1 param1, T2 param2, T3 param3)
            => await Execute(new DbParamsModel<T1, T2, T3>(param1, param2, param3));

        public async Task<IDbResponse> Mock(T1 param1, T2 param2, T3 param3, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3>(param1, param2, param3), returnValue);
    }
    internal class GenericAsyncTransaction<T1, T2, T3, T4> : AsyncTransaction<DbParamsModel<T1, T2, T3, T4>>, IAsyncGenericTransaction<T1, T2, T3, T4>
    {
        public GenericAsyncTransaction(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse> Execute(T1 param1, T2 param2, T3 param3, T4 param4)
            => await Execute(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4));

        public async Task<IDbResponse> Mock(T1 param1, T2 param2, T3 param3, T4 param4, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), returnValue);
    }
    internal class GenericAsyncTransaction<T1, T2, T3, T4, T5> : AsyncTransaction<DbParamsModel<T1, T2, T3, T4, T5>>, IAsyncGenericTransaction<T1, T2, T3, T4, T5>
    {
        public GenericAsyncTransaction(ExecutionDelegate handler) : base(handler) { }
        public async Task<IDbResponse> Execute(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            => await Execute(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));

        public async Task<IDbResponse> Mock(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, object returnValue = null)
            => await Mock(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), returnValue);
    }
    
}
