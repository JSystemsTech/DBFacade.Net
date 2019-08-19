using DBFacade.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml;

namespace DBFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbResponse
    {
        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <returns></returns>
        object ReturnValue();
        /// <summary>
        /// Determines whether this instance has error.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </returns>
        bool HasError();
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <returns></returns>
        FacadeException GetException();
    }

    public interface IXMLSerializable<TDbDataModel>
         where TDbDataModel : DbDataModel
    {
        void Serialize(TextWriter textWriter);
        void Serialize(XmlWriter xmlWriter);
    }
    public interface IJsonSerializable<TDbDataModel>
         where TDbDataModel : DbDataModel
    {
        string ToJson();
        JsonResult ToJsonResult();
    }
    public interface IReadOnlyDbCollection<TDbDataModel>
         where TDbDataModel : DbDataModel
    {
        bool Contains(TDbDataModel item);
        int Count();
        TDbDataModel First();
        bool Exists(Predicate<TDbDataModel> match);
        TDbDataModel Find(Predicate<TDbDataModel> match);
        int FindIndex(Predicate<TDbDataModel> match);
        int FindIndex(int startIndex, Predicate<TDbDataModel> match);
        int FindIndex(int startIndex, int count, Predicate<TDbDataModel> match);
        int FindLastIndex(Predicate<TDbDataModel> match);
        int FindLastIndex(int startIndex, int count, Predicate<TDbDataModel> match);
        void ForEach(Action<TDbDataModel> action);
        int IndexOf(TDbDataModel item, int index, int count);
        int IndexOf(TDbDataModel item, int index);
        int IndexOf(TDbDataModel item);
        int LastIndexOf(TDbDataModel item);
        int LastIndexOf(TDbDataModel item, int index);
        int LastIndexOf(TDbDataModel item, int index, int count);
        void Reverse(int index, int count);
        void Reverse();
        void Sort(int index, int count, IComparer<TDbDataModel> comparer);
        void Sort(Comparison<TDbDataModel> comparison);
        void Sort();
        void Sort(IComparer<TDbDataModel> comparer);
        TDbDataModel[] ToArray();
        List<TDbDataModel> ToList();
    }
    public interface IDbResponse<TDbDataModel> : IDbResponse, IReadOnlyDbCollection<TDbDataModel>, IXMLSerializable<TDbDataModel>, IJsonSerializable<TDbDataModel>
         where TDbDataModel : DbDataModel
    {}

}
