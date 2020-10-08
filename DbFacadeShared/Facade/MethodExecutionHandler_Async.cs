using DbFacade.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacade.Facade
{
    public interface IAsyncTransaction
    {
        Task<IDbResponse> ExecuteAsync();
        Task<IDbResponse> MockAsync(int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncTransaction<TDbParams>
        where TDbParams : IDbParamsModel
    {
        Task<IDbResponse> ExecuteAsync(TDbParams parameters);
        Task<IDbResponse> MockAsync(TDbParams parameters, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericTransaction<T1> : IAsyncTransaction<DbParamsModel<T1>>
    {
        Task<IDbResponse> ExecuteAsync(T1 param1);
        Task<IDbResponse> MockAsync(T1 param1, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericTransaction<T1, T2> : IAsyncTransaction<DbParamsModel<T1, T2>>
    {
        Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2);
        Task<IDbResponse> MockAsync(T1 param1, T2 param2, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericTransaction<T1, T2, T3> : IAsyncTransaction<DbParamsModel<T1, T2, T3>>

    {
        Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2, T3 param3);
        Task<IDbResponse> MockAsync(T1 param1, T2 param2, T3 param3, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericTransaction<T1, T2, T3, T4> : IAsyncTransaction<DbParamsModel<T1, T2, T3, T4>>

    {
        Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4);
        Task<IDbResponse> MockAsync(T1 param1, T2 param2, T3 param3, T4 param4, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericTransaction<T1, T2, T3, T4, T5> : IAsyncTransaction<DbParamsModel<T1, T2, T3, T4, T5>>

    {
        Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
        Task<IDbResponse> MockAsync(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    

    public interface IAsyncFetch<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> ExecuteAsync();
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncFetch<TDbDataModel, TDbParams>
        where TDbDataModel : DbDataModel
        where TDbParams : IDbParamsModel
    {
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1> : IAsyncFetch<TDbDataModel, DbParamsModel<T1>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2, T3> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2, T3 param3);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }
    public interface IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4, T5> : IAsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>>
        where TDbDataModel : DbDataModel
    {
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null);
    }

    internal class AsyncFetch<TDbDataModel> : IAsyncFetch<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        internal delegate Task<IDbResponse<TDbDataModel>> ExecutionDelegate();
        internal delegate Task<IDbResponse<TDbDataModel>> MockExecutionDelegate(DbParamsModel parameters);
        private ExecutionDelegate Handler { get; set; }
        private MockExecutionDelegate MockHandler { get; set; }
        
        public static async Task<AsyncFetch<TDbDataModel>> CreateAsync(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            AsyncFetch<TDbDataModel> asyncFetch = new AsyncFetch<TDbDataModel>();
            await asyncFetch.InitializeAsync(handler, mockHandler);
            return asyncFetch;
        }
        private async Task InitializeAsync(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            Handler = handler;
            MockHandler = mockHandler;
            await Task.CompletedTask;
        }
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync() => await Handler();
        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            var parameters = new DbParamsModel();
            await parameters.RunAsTestAsync(responseData, returnValue, outputValues);
            return await MockHandler(parameters);
        }
        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            var parameters = new DbParamsModel();
            await parameters.RunAsTestAsync(responseData, returnValue, outputValues);
            return await MockHandler(parameters);
        }
    }
    internal class AsyncFetch<TDbDataModel, TDbParams> : IAsyncFetch<TDbDataModel, TDbParams>
        where TDbDataModel : DbDataModel
        where TDbParams : IDbParamsModel
    {
        internal delegate Task<IDbResponse<TDbDataModel>> ExecutionDelegate(TDbParams parameters);
        private ExecutionDelegate Handler { get; set; }

        public static async Task<AsyncFetch<TDbDataModel, TDbParams>> CreateAsync(ExecutionDelegate handler)
        {
            AsyncFetch<TDbDataModel, TDbParams> asyncFetch = new AsyncFetch<TDbDataModel, TDbParams>();
            await asyncFetch.InitializeAsync(handler);
            return asyncFetch;
        }
        protected async Task InitializeAsync(ExecutionDelegate handler)
        {            
            Handler = handler;
            await Task.CompletedTask;
        }
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters) => await Handler(parameters);
        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            await parameters.RunAsTestAsync(responseData, returnValue, outputValues);
            return await ExecuteAsync(parameters);
        }
        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            await parameters.RunAsTestAsync(responseData,returnValue, outputValues);
            return await ExecuteAsync(parameters);
        }
    }
    internal class GenericAsyncFetch<TDbDataModel, T1> : AsyncFetch<TDbDataModel, DbParamsModel<T1>>, IAsyncGenericFetch<TDbDataModel, T1>
        where TDbDataModel : DbDataModel
    {
        public static new async Task<GenericAsyncFetch<TDbDataModel, T1>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncFetch<TDbDataModel, T1> asyncFetch = new GenericAsyncFetch<TDbDataModel, T1>();
            await asyncFetch.InitializeAsync(handler);
            return asyncFetch;
        }
        
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1)
            => await ExecuteAsync(new DbParamsModel<T1>(param1));

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1>(param1), responseData, returnValue, outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1>(param1), responseData, returnValue, outputValues);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2>>, IAsyncGenericFetch<TDbDataModel, T1, T2>
        where TDbDataModel : DbDataModel
    {
        public static new async Task<GenericAsyncFetch<TDbDataModel, T1,T2>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncFetch<TDbDataModel, T1, T2> asyncFetch = new GenericAsyncFetch<TDbDataModel, T1, T2>();
            await asyncFetch.InitializeAsync(handler);
            return asyncFetch;
        }
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2)
            => await ExecuteAsync(new DbParamsModel<T1, T2>(param1, param2));

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2>(param1, param2), responseData, returnValue, outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2>(param1, param2), responseData, returnValue, outputValues);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2, T3> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3>>, IAsyncGenericFetch<TDbDataModel, T1, T2, T3>
        where TDbDataModel : DbDataModel
    {
        public static new async Task<GenericAsyncFetch<TDbDataModel, T1, T2, T3>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncFetch<TDbDataModel, T1, T2, T3> asyncFetch = new GenericAsyncFetch<TDbDataModel, T1, T2, T3>();
            await asyncFetch.InitializeAsync(handler);
            return asyncFetch;
        }
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2, T3 param3)
            => await ExecuteAsync(new DbParamsModel<T1, T2, T3>(param1, param2, param3));

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3>(param1, param2, param3), responseData, returnValue, outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3>(param1, param2, param3), responseData, returnValue, outputValues);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4>>, IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4>
        where TDbDataModel : DbDataModel
    {
        public static new async Task<GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4> asyncFetch = new GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4>();
            await asyncFetch.InitializeAsync(handler);
            return asyncFetch;
        }
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4)
            => await ExecuteAsync(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4));

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), responseData, returnValue, outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), responseData, returnValue, outputValues);
    }
    internal class GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4, T5> : AsyncFetch<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>>, IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4, T5>
        where TDbDataModel : DbDataModel
    {
        public static new async Task<GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4, T5>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4, T5> asyncFetch = new GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4, T5>();
            await asyncFetch.InitializeAsync(handler);
            return asyncFetch;
        }
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            => await ExecuteAsync(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, IEnumerable<T> responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), responseData, returnValue, outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T responseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
            =>await MockAsync(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), responseData, returnValue, outputValues);
    }



    internal class AsyncTransaction : IAsyncTransaction
    {
        internal delegate Task<IDbResponse> ExecutionDelegate();
        internal delegate Task<IDbResponse> MockExecutionDelegate(DbParamsModel parameters);
        private ExecutionDelegate Handler { get; set; }
        private MockExecutionDelegate MockHandler { get; set; }
        public static async Task<AsyncTransaction> CreateAsync(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            AsyncTransaction asyncTransaction = new AsyncTransaction();
            await asyncTransaction.InitializeAsync(handler, mockHandler);
            return asyncTransaction;
        }
        private async Task InitializeAsync(ExecutionDelegate handler, MockExecutionDelegate mockHandler)
        {
            Handler = handler;
            MockHandler = mockHandler;
            await Task.CompletedTask;
        }
        public async Task<IDbResponse> ExecuteAsync() => await Handler();
        public async Task<IDbResponse> MockAsync(int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            var parameters = new DbParamsModel();
            await parameters.RunAsTestAsync(returnValue, outputValues);
            return await MockHandler(parameters);
        }
    }
    internal class AsyncTransaction<TDbParams> : IAsyncTransaction<TDbParams>
        where TDbParams : IDbParamsModel
    {
        internal delegate Task<IDbResponse> ExecutionDelegate(TDbParams parameters);
        private ExecutionDelegate Handler { get; set; }
        public static async Task<AsyncTransaction<TDbParams>> CreateAsync(ExecutionDelegate handler)
        {
            AsyncTransaction<TDbParams> asyncTransaction = new AsyncTransaction<TDbParams>();
            await asyncTransaction.InitializeAsync(handler);
            return asyncTransaction;
        }
        protected async Task InitializeAsync(ExecutionDelegate handler)
        {
            Handler = handler;
            await Task.CompletedTask;
        }
        public async Task<IDbResponse> ExecuteAsync(TDbParams parameters) => await Handler(parameters);
        public async Task<IDbResponse> MockAsync(TDbParams parameters, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            await parameters.RunAsTestAsync(returnValue, outputValues);
            return await ExecuteAsync(parameters);
        }
    }
    internal class GenericAsyncTransaction<T1> : AsyncTransaction<DbParamsModel<T1>>, IAsyncGenericTransaction<T1>
    {
        public static new async Task<GenericAsyncTransaction<T1>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncTransaction<T1> asyncTransaction = new GenericAsyncTransaction<T1>();
            await asyncTransaction.InitializeAsync(handler);
            return asyncTransaction;
        }
        public async Task<IDbResponse> ExecuteAsync(T1 param1)
            => await ExecuteAsync(new DbParamsModel<T1>(param1));

        public async Task<IDbResponse> MockAsync(T1 param1, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1>(param1), returnValue,outputValues);
    }
    internal class GenericAsyncTransaction<T1, T2> : AsyncTransaction<DbParamsModel<T1, T2>>, IAsyncGenericTransaction<T1, T2>
    {
        public static new async Task<GenericAsyncTransaction<T1,T2>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncTransaction<T1,T2> asyncTransaction = new GenericAsyncTransaction<T1,T2>();
            await asyncTransaction.InitializeAsync(handler);
            return asyncTransaction;
        }
        public async Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2)
            => await ExecuteAsync(new DbParamsModel<T1, T2>(param1, param2));

        public async Task<IDbResponse> MockAsync(T1 param1, T2 param2, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2>(param1, param2), returnValue, outputValues);
    }
    internal class GenericAsyncTransaction<T1, T2, T3> : AsyncTransaction<DbParamsModel<T1, T2, T3>>, IAsyncGenericTransaction<T1, T2, T3>
    {
        public static new async Task<GenericAsyncTransaction<T1, T2, T3>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncTransaction<T1, T2, T3> asyncTransaction = new GenericAsyncTransaction<T1, T2, T3>();
            await asyncTransaction.InitializeAsync(handler);
            return asyncTransaction;
        }
        public async Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2, T3 param3)
            => await ExecuteAsync(new DbParamsModel<T1, T2, T3>(param1, param2, param3));

        public async Task<IDbResponse> MockAsync(T1 param1, T2 param2, T3 param3, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3>(param1, param2, param3), returnValue,outputValues);
    }
    internal class GenericAsyncTransaction<T1, T2, T3, T4> : AsyncTransaction<DbParamsModel<T1, T2, T3, T4>>, IAsyncGenericTransaction<T1, T2, T3, T4>
    {
        public static new async Task<GenericAsyncTransaction<T1, T2, T3, T4>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncTransaction<T1, T2, T3, T4> asyncTransaction = new GenericAsyncTransaction<T1, T2, T3, T4>();
            await asyncTransaction.InitializeAsync(handler);
            return asyncTransaction;
        }
        public async Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4)
            => await ExecuteAsync(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4));

        public async Task<IDbResponse> MockAsync(T1 param1, T2 param2, T3 param3, T4 param4, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3, T4>(param1, param2, param3, param4), returnValue,outputValues);
    }
    internal class GenericAsyncTransaction<T1, T2, T3, T4, T5> : AsyncTransaction<DbParamsModel<T1, T2, T3, T4, T5>>, IAsyncGenericTransaction<T1, T2, T3, T4, T5>
    {
        public static new async Task<GenericAsyncTransaction<T1, T2, T3, T4, T5>> CreateAsync(ExecutionDelegate handler)
        {
            GenericAsyncTransaction<T1, T2, T3, T4, T5> asyncTransaction = new GenericAsyncTransaction<T1, T2, T3, T4, T5>();
            await asyncTransaction.InitializeAsync(handler);
            return asyncTransaction;
        }
        public async Task<IDbResponse> ExecuteAsync(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            => await ExecuteAsync(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5));

        public async Task<IDbResponse> MockAsync(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => await MockAsync(new DbParamsModel<T1, T2, T3, T4, T5>(param1, param2, param3, param4, param5), returnValue,outputValues);
    }
    
}
