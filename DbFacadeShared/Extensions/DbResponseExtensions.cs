using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DbFacadeShared.Extensions
{
    public static class DbResponseExtensions
    {
        //private static bool TryGetDataSet<TDbDataModel, T>(this IDbResponse<TDbDataModel> response, int index, out IEnumerable<T> value)
        //    where TDbDataModel : DbDataModel
        //    where T : DbDataModel
        //{
        //    if (response.HasError || response.DataSets.Count()-1 < index || index < 0)
        //    {
        //        value = Array.Empty<T>();
        //        return false;
        //    }
        //    try
        //    {
        //        var dataSet = response.DataSets.ElementAt(index);
        //        value = dataSet.ToDbDataModelList<T>();
        //        return true;
        //    }
        //    catch
        //    {
        //        value = Array.Empty<T>();
        //        return false;
        //    }
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1>(this IDbResponse<TDbDataModel> response, out IEnumerable<T1> value)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //=> response.TryGetDataSet(1, out value);

        //public static bool TryGetDataSet<TDbDataModel, T1,T2>(
        //    this IDbResponse<TDbDataModel> response, 
        //    out IEnumerable<T1> value, 
        //    out IEnumerable<T2> value2)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //{
        //        bool hasPrevSets = response.TryGetDataSet(out value);
        //        bool hasNextSet = response.TryGetDataSet(2, out value2);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2,T3>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2);
        //    bool hasNextSet = response.TryGetDataSet(3, out value3);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2, T3,T4>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3,
        //    out IEnumerable<T4> value4)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //    where T4 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2, out value3);
        //    bool hasNextSet = response.TryGetDataSet(4, out value4);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2, T3, T4, T5>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3,
        //    out IEnumerable<T4> value4,
        //    out IEnumerable<T5> value5)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //    where T4 : DbDataModel
        //    where T5 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2, out value3, out value4);
        //    bool hasNextSet = response.TryGetDataSet(5, out value5);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2, T3, T4, T5, T6>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3,
        //    out IEnumerable<T4> value4,
        //    out IEnumerable<T5> value5,
        //    out IEnumerable<T6> value6)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //    where T4 : DbDataModel
        //    where T5 : DbDataModel
        //    where T6 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2, out value3, out value4, out value5);
        //    bool hasNextSet = response.TryGetDataSet(6, out value6);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2, T3, T4, T5, T6, T7>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3,
        //    out IEnumerable<T4> value4,
        //    out IEnumerable<T5> value5,
        //    out IEnumerable<T6> value6,
        //    out IEnumerable<T7> value7)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //    where T4 : DbDataModel
        //    where T5 : DbDataModel
        //    where T6 : DbDataModel
        //    where T7 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2, out value3, out value4, out value5, out value6);
        //    bool hasNextSet = response.TryGetDataSet(7, out value7);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2, T3, T4, T5, T6, T7, T8>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3,
        //    out IEnumerable<T4> value4,
        //    out IEnumerable<T5> value5,
        //    out IEnumerable<T6> value6,
        //    out IEnumerable<T7> value7,
        //    out IEnumerable<T8> value8)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //    where T4 : DbDataModel
        //    where T5 : DbDataModel
        //    where T6 : DbDataModel
        //    where T7 : DbDataModel
        //    where T8 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2, out value3, out value4, out value5, out value6, out value7);
        //    bool hasNextSet = response.TryGetDataSet(8, out value8);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3,
        //    out IEnumerable<T4> value4,
        //    out IEnumerable<T5> value5,
        //    out IEnumerable<T6> value6,
        //    out IEnumerable<T7> value7,
        //    out IEnumerable<T8> value8,
        //    out IEnumerable<T9> value9)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //    where T4 : DbDataModel
        //    where T5 : DbDataModel
        //    where T6 : DbDataModel
        //    where T7 : DbDataModel
        //    where T8 : DbDataModel
        //    where T9 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2, out value3, out value4, out value5, out value6, out value7, out value8);
        //    bool hasNextSet = response.TryGetDataSet(9, out value9);
        //    return hasPrevSets && hasNextSet;
        //}
        //public static bool TryGetDataSet<TDbDataModel, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        //    this IDbResponse<TDbDataModel> response,
        //    out IEnumerable<T1> value,
        //    out IEnumerable<T2> value2,
        //    out IEnumerable<T3> value3,
        //    out IEnumerable<T4> value4,
        //    out IEnumerable<T5> value5,
        //    out IEnumerable<T6> value6,
        //    out IEnumerable<T7> value7,
        //    out IEnumerable<T8> value8,
        //    out IEnumerable<T9> value9,
        //    out IEnumerable<T10> value10)
        //    where TDbDataModel : DbDataModel
        //    where T1 : DbDataModel
        //    where T2 : DbDataModel
        //    where T3 : DbDataModel
        //    where T4 : DbDataModel
        //    where T5 : DbDataModel
        //    where T6 : DbDataModel
        //    where T7 : DbDataModel
        //    where T8 : DbDataModel
        //    where T9 : DbDataModel
        //    where T10 : DbDataModel
        //{
        //    bool hasPrevSets = response.TryGetDataSet(out value, out value2, out value3, out value4, out value5, out value6, out value7, out value8, out value9);
        //    bool hasNextSet = response.TryGetDataSet(10, out value10);
        //    return hasPrevSets && hasNextSet;
        //}
    }
}
