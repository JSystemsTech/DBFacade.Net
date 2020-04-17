using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace DBFacade.DataLayer.Models
{
    /// <summary>
    /// </summary>
    public interface IDbResponse
    {
        /// <summary>
        ///     Returns the value.
        /// </summary>
        /// <returns></returns>
        object ReturnValue { get; }

        bool IsNull { get; }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IXMLSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        ///     Serializes the specified text writer.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        void Serialize(TextWriter textWriter);

        Task SerializeAsync(TextWriter textWriter);

        /// <summary>
        ///     Serializes the specified XML writer.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        void Serialize(XmlWriter xmlWriter);

        Task SerializeAsync(XmlWriter xmlWriter);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IJsonSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        ///     Converts to json.
        /// </summary>
        /// <returns></returns>
        string ToJson();

        Task<string> ToJsonAsync();
    }

    public interface IReadOnlyDbCollection<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        ///     Determines whether this instance contains the object.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(TDbDataModel item);

        /// <summary>
        ///     Counts this instance.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        ///     Firsts this instance.
        /// </summary>
        /// <returns></returns>
        TDbDataModel First();

        /// <summary>
        ///     Exists the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        bool Exists(Predicate<TDbDataModel> match);

        /// <summary>
        ///     Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        TDbDataModel Find(Predicate<TDbDataModel> match);

        /// <summary>
        ///     Finds the index.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindIndex(Predicate<TDbDataModel> match);

        /// <summary>
        ///     Finds the index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindIndex(int startIndex, Predicate<TDbDataModel> match);

        /// <summary>
        ///     Finds the index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindIndex(int startIndex, int count, Predicate<TDbDataModel> match);

        /// <summary>
        ///     Finds the last index.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindLastIndex(Predicate<TDbDataModel> match);

        /// <summary>
        ///     Finds the last index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int FindLastIndex(int startIndex, int count, Predicate<TDbDataModel> match);

        /// <summary>
        ///     For each.
        /// </summary>
        /// <param name="action">The action.</param>
        void ForEach(Action<TDbDataModel> action);

        /// <summary>
        ///     Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        int IndexOf(TDbDataModel item, int index, int count);

        /// <summary>
        ///     Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        int IndexOf(TDbDataModel item, int index);

        /// <summary>
        ///     Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        int IndexOf(TDbDataModel item);

        /// <summary>
        ///     Lasts the index of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        int LastIndexOf(TDbDataModel item);

        /// <summary>
        ///     Lasts the index of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        int LastIndexOf(TDbDataModel item, int index);

        /// <summary>
        ///     Lasts the index of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        int LastIndexOf(TDbDataModel item, int index, int count);

        /// <summary>
        ///     Reverses the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        void Reverse(int index, int count);

        /// <summary>
        ///     Reverses this instance.
        /// </summary>
        void Reverse();

        /// <summary>
        ///     Sorts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <param name="comparer">The comparer.</param>
        void Sort(int index, int count, IComparer<TDbDataModel> comparer);

        /// <summary>
        ///     Sorts the specified comparison.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        void Sort(Comparison<TDbDataModel> comparison);

        /// <summary>
        ///     Sorts this instance.
        /// </summary>
        void Sort();

        /// <summary>
        ///     Sorts the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        void Sort(IComparer<TDbDataModel> comparer);

        /// <summary>
        ///     Converts to array.
        /// </summary>
        /// <returns></returns>
        TDbDataModel[] ToArray();

        /// <summary>
        ///     Converts to list.
        /// </summary>
        /// <returns></returns>
        List<TDbDataModel> ToList();

        Task<List<TDbDataModel>> ToListAsync();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IDbResponse<TDbDataModel> : IDbResponse, IEnumerable<TDbDataModel>,
        IReadOnlyDbCollection<TDbDataModel>, IXMLSerializable<TDbDataModel>, IJsonSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        bool HasDataBindingErrors { get; }
    }
}