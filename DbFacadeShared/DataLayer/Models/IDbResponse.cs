using DbFacade.DataLayer.ConnectionService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IXMLSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Serializes the specified text writer.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        void Serialize(TextWriter textWriter);

        /// <summary>
        /// Serializes the asynchronous.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        /// <returns></returns>
        Task SerializeAsync(TextWriter textWriter);

        /// <summary>
        /// Serializes the specified XML writer.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        void Serialize(XmlWriter xmlWriter);

        /// <summary>
        /// Serializes the asynchronous.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        /// <returns></returns>
        Task SerializeAsync(XmlWriter xmlWriter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IJsonSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns></returns>
        string ToJson();

        /// <summary>
        /// Converts to jsonasync.
        /// </summary>
        /// <returns></returns>
        Task<string> ToJsonAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IReadOnlyDbCollection<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(TDbDataModel item);
        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// Firsts this instance.
        /// </summary>
        /// <returns></returns>
        TDbDataModel First();
        /// <summary>
        /// Existses the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        bool Exists(Predicate<TDbDataModel> match);
        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        TDbDataModel Find(Predicate<TDbDataModel> match);
        /// <summary>
        /// Finds the index.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindIndex(Predicate<TDbDataModel> match);

        /// <summary>
        /// Finds the index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindIndex(int startIndex, Predicate<TDbDataModel> match);
        /// <summary>
        /// Finds the index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindIndex(int startIndex, int count, Predicate<TDbDataModel> match);
        /// <summary>
        /// Finds the last index.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindLastIndex(Predicate<TDbDataModel> match);
        /// <summary>
        /// Finds the last index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindLastIndex(int startIndex, int count, Predicate<TDbDataModel> match);
        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <param name="action">The action.</param>
        void ForEach(Action<TDbDataModel> action);
        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        int IndexOf(TDbDataModel item, int index, int count);
        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        int IndexOf(TDbDataModel item, int index);
        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        int IndexOf(TDbDataModel item);
        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        int LastIndexOf(TDbDataModel item);
        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        int LastIndexOf(TDbDataModel item, int index);
        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        int LastIndexOf(TDbDataModel item, int index, int count);
        /// <summary>
        /// Reverses the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        void Reverse(int index, int count);
        /// <summary>
        /// Reverses this instance.
        /// </summary>
        void Reverse();
        /// <summary>
        /// Sorts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <param name="comparer">The comparer.</param>
        void Sort(int index, int count, IComparer<TDbDataModel> comparer);
        /// <summary>
        /// Sorts the specified comparison.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        void Sort(Comparison<TDbDataModel> comparison);
        /// <summary>
        /// Sorts this instance.
        /// </summary>
        void Sort();
        /// <summary>
        /// Sorts the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        void Sort(IComparer<TDbDataModel> comparer);
        /// <summary>
        /// Converts to array.
        /// </summary>
        /// <returns></returns>
        TDbDataModel[] ToArray();
        /// <summary>
        /// Converts to list.
        /// </summary>
        /// <returns></returns>
        List<TDbDataModel> ToList();
        /// <summary>
        /// Converts to listasync.
        /// </summary>
        /// <returns></returns>
        Task<List<TDbDataModel>> ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IDbResponse<TDbDataModel> : IEnumerable<TDbDataModel>,
        IReadOnlyDbCollection<TDbDataModel>, IXMLSerializable<TDbDataModel>, IJsonSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        int ReturnValue { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is null; otherwise, <c>false</c>.
        /// </value>
        bool IsNull { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has data binding errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has data binding errors; otherwise, <c>false</c>.
        /// </value>
        bool HasDataBindingErrors { get; }
        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        /// <value>
        /// The command identifier.
        /// </value>
        Guid CommandId { get; }

        /// <summary>Gets the database command settings.</summary>
        /// <value>The database command settings.</value>
        IDbCommandSettings DbCommandSettings { get; }
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        Exception Error { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        bool HasError { get; }
        /// <summary>
        /// Gets the output value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object GetOutputValue(string key);
        /// <summary>
        /// Gets the output value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetOutputValue<T>(string key);
        /// <summary>
        /// Gets the output model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOutputModel<T>() where T : DbDataModel;

        /// <summary>
        /// Gets the output value asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<object> GetOutputValueAsync(string key);
        /// <summary>
        /// Gets the output value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<T> GetOutputValueAsync<T>(string key);
        /// <summary>
        /// Gets the output model asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOutputModelAsync<T>() where T : DbDataModel;
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDbResponse : IDbResponse<DbDataModel> { }
}