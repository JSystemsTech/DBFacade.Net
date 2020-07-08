using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDbParameterCollection : DbParameterCollection
    {
        private List<MockDbParameter> Parameters { get; set; }
        public MockDbParameterCollection() {
            Parameters = new List<MockDbParameter>();
        }
        public override int Count => Parameters.Count;

        public override object SyncRoot => throw new NotImplementedException();

        public override int Add(object value)
        {
            if (value is MockDbParameter parameter && !Contains(parameter))
            {
                Parameters.Add(parameter);
                return 1;
            }
            return 0;
        }

        public override void AddRange(Array values)
        {
            if(values is IEnumerable<MockDbParameter> range)
            {
                Parameters.AddRange(range);
            }
        }

        public override void Clear() => Parameters.Clear();

        public override bool Contains(object value)
        {
            if (value is MockDbParameter parameter)
            {
                return Contains(parameter.ParameterName);
            }
            return false;
        }

        public override bool Contains(string value) => Parameters.Any(p=>p.ParameterName == value);

        public override void CopyTo(Array array, int index)
        {
            if (array is MockDbParameter[] values)
            {
                Parameters.CopyTo(values, index);
            }
        }

        public override IEnumerator GetEnumerator() => Parameters.GetEnumerator();

        public override int IndexOf(object value)
        {
            if (value is MockDbParameter parameter)
            {
                return Parameters.IndexOf(parameter);
            }
            return -1;
        }
        public override int IndexOf(string parameterName) => IndexOf(Parameters.FirstOrDefault(p=>p.ParameterName == parameterName));


        public override void Insert(int index, object value)
        {
            if (value is MockDbParameter parameter)
            {
                Parameters.Insert(index, parameter);
            }
        }

        public override void Remove(object value)
        {
            if (value is MockDbParameter parameter)
            {
                Parameters.Remove(parameter);
            }
        }

        public override void RemoveAt(int index) => Parameters.RemoveAt(index);

        public override void RemoveAt(string parameterName) => RemoveAt(IndexOf(parameterName));

        protected override DbParameter GetParameter(int index) => Parameters.ElementAtOrDefault(index);
        protected override DbParameter GetParameter(string parameterName) => GetParameter(IndexOf(parameterName));

        protected override void SetParameter(int index, DbParameter value)
        {
            if (value is MockDbParameter parameter && index  >= 0 && index < Count)
            {
                bool hasExistingRecord = Contains(parameter);
                if(!hasExistingRecord || (hasExistingRecord && Parameters.ElementAtOrDefault(index).ParameterName == parameter.ParameterName))
                {
                    Parameters[index] = parameter;
                }                
            }
        }

        protected override void SetParameter(string parameterName, DbParameter value) => SetParameter(IndexOf(parameterName), value);
        public MockDbParameter GetReturnValueParam()
        =>Parameters.Where(entry => entry.Direction == System.Data.ParameterDirection.ReturnValue).FirstOrDefault();
        
        public IEnumerable<MockDbParameter> GetOutputParams()
        => Parameters.Where(entry => entry.Direction == System.Data.ParameterDirection.Output);
    }
}
