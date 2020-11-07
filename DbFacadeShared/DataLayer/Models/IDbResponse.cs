using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.DataLayer.Models
{    
    public interface IXMLSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        void Serialize(TextWriter textWriter);

        Task SerializeAsync(TextWriter textWriter);

        void Serialize(XmlWriter xmlWriter);

        Task SerializeAsync(XmlWriter xmlWriter);
    }

    public interface IJsonSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        string ToJson();

        Task<string> ToJsonAsync();
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
        Task<List<TDbDataModel>> ToListAsync();
    }

    public interface IDbResponse<TDbDataModel> : IEnumerable<TDbDataModel>,
        IReadOnlyDbCollection<TDbDataModel>, IXMLSerializable<TDbDataModel>, IJsonSerializable<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        int ReturnValue { get; }
        IDictionary<string, object> OutputValues { get; }
        object GetOutputValue(string key);
        T GetOutputValue<T>(string key);
        bool IsNull { get; }
        bool HasDataBindingErrors { get; }
    }
    public interface IDbResponse : IDbResponse<DbDataModel> { }
}