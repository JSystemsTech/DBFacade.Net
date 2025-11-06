using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DbFacade.Extensions
{
    public static class DbCommandMethodExtensions
    {
        /// <summary>Executes the and fetch first.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbResponse ExecuteAndFetchFirst<T>(this IDbCommandMethod endpoint, out IEnumerable<T> data)
            where T : class, IDbDataModel
        {
            var response = endpoint.Execute();
            data = response.ToDbDataModelList<T>();
            return response;
        }


        /// <summary>Executes the and fetch first.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="initialize">The initialize.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbResponse ExecuteAndFetchFirst<T>(this IDbCommandMethod endpoint, Action<T, IDataCollection> initialize, out IEnumerable<T> data)
            where T : class
        {
            var response = endpoint.Execute();
            data = response.ToDbDataModelList(initialize);
            return response;
        }


        /// <summary>Executes the and fetch first.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbResponse ExecuteAndFetchFirst<T, TParameters>(this IDbCommandMethod endpoint, TParameters parameters, out IEnumerable<T> data)
            where T : class, IDbDataModel
        {
            var response = endpoint.Execute(parameters);
            data = response.ToDbDataModelList<T>();
            return response;
        }


        /// <summary>Executes the and fetch first.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="initialize">The initialize.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbResponse ExecuteAndFetchFirst<T,TParameters>(this IDbCommandMethod endpoint, TParameters parameters, Action<T, IDataCollection> initialize, out IEnumerable<T> data)
            where T : class
        {
            var response = endpoint.Execute(parameters);
            data = response.ToDbDataModelList(initialize);
            return response;
        }

        /// <summary>Executes the group.</summary>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="methods">The methods.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IEnumerable<IDbResponse> ExecuteGroup<TParameters>(this IEnumerable<IDbCommandMethod> methods, TParameters parameters)
        {
            var methodsToRun = methods.Select(m => (DbCommandMethod)m);
            return DbConnectionManager.ExecuteDbActions(methodsToRun, parameters);
        }
        /// <summary>Executes the group.</summary>
        /// <param name="methods">The methods.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IEnumerable<IDbResponse> ExecuteGroup(this IEnumerable<IDbCommandMethod> methods)
            => methods.ExecuteGroup<object>(null);



        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T>(this IDbCommandMethod endpoint)
            where T : class, IDbDataModel
        => await endpoint.ExecuteAndFetchFirstAsync<T>(CancellationToken.None);

        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T>(this IDbCommandMethod endpoint, Action<T, IDataCollection> initialize)
            where T : class
        => await endpoint.ExecuteAndFetchFirstAsync(CancellationToken.None, initialize);


        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T, TParameters>(this IDbCommandMethod endpoint, TParameters parameters)
            where T : class, IDbDataModel
        => await endpoint.ExecuteAndFetchFirstAsync<T, TParameters>(CancellationToken.None, parameters);

        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T, TParameters>(this IDbCommandMethod endpoint, TParameters parameters, Action<T, IDataCollection> initialize)
            where T : class
            => await endpoint.ExecuteAndFetchFirstAsync(CancellationToken.None, parameters, initialize);        

        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T>(this IDbCommandMethod endpoint, CancellationToken cancellationToken)
            where T : class, IDbDataModel
        {
            var response = await endpoint.ExecuteAsync(cancellationToken);
            var data = response.ToDbDataModelList<T>();
            return new Tuple<IDbResponse, IEnumerable<T>>(response, data);
        }

        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T>(this IDbCommandMethod endpoint, CancellationToken cancellationToken, Action<T, IDataCollection> initialize)
            where T : class
        {
            var response = await endpoint.ExecuteAsync(cancellationToken);
            var data = response.ToDbDataModelList(initialize);
            return new Tuple<IDbResponse, IEnumerable<T>>(response, data);
        }

        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T, TParameters>(this IDbCommandMethod endpoint, CancellationToken cancellationToken, TParameters parameters)
            where T : class, IDbDataModel
        {
            var response = await endpoint.ExecuteAsync(cancellationToken, parameters);
            var data = response.ToDbDataModelList<T>();
            return new Tuple<IDbResponse, IEnumerable<T>>(response, data);
        }

        /// <summary>Executes the and fetch first asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<Tuple<IDbResponse, IEnumerable<T>>> ExecuteAndFetchFirstAsync<T, TParameters>(this IDbCommandMethod endpoint, CancellationToken cancellationToken, TParameters parameters, Action<T, IDataCollection> initialize)
            where T : class
        {
            var response = await endpoint.ExecuteAsync(cancellationToken, parameters);
            var data = response.ToDbDataModelList(initialize);
            return new Tuple<IDbResponse, IEnumerable<T>>(response, data);
        }


        /// <summary>Executes the group asynchronous.</summary>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="methods">The methods.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<IEnumerable<IDbResponse>> ExecuteGroupAsync<TParameters>(this IEnumerable<IDbCommandMethod> methods, TParameters parameters)
        => await methods.ExecuteGroupAsync(CancellationToken.None, parameters);

        /// <summary>Executes the group asynchronous.</summary>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="methods">The methods.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<IEnumerable<IDbResponse>> ExecuteGroupAsync<TParameters>(this IEnumerable<IDbCommandMethod> methods, CancellationToken cancellationToken, TParameters parameters)
        {
            var methodsToRun = methods.Select(m => (DbCommandMethod)m);
            return await DbConnectionManager.ExecuteDbActionsAsync(methodsToRun, parameters, cancellationToken);
        }
        /// <summary>Executes the group asynchronous.</summary>
        /// <param name="methods">The methods.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<IEnumerable<IDbResponse>> ExecuteGroupAsync(this IEnumerable<IDbCommandMethod> methods)
        => await methods.ExecuteGroupAsync(CancellationToken.None);

        /// <summary>Executes the group asynchronous.</summary>
        /// <param name="methods">The methods.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static async Task<IEnumerable<IDbResponse>> ExecuteGroupAsync(this IEnumerable<IDbCommandMethod> methods, CancellationToken cancellationToken)
        {
            var methodsToRun = methods.Select(m => (DbCommandMethod)m);
            return await DbConnectionManager.ExecuteDbActionsAsync<object>(methodsToRun, null,cancellationToken);
        }
    }
}
