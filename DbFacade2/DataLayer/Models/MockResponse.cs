using DbFacade.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMockResponse
    {
        /// <summary>Gets the output values.</summary>
        /// <value>The output values.</value>        
        DataCollection OutputValues { get; }
        /// <summary>Gets the settings.</summary>
        /// <value>The settings.</value>
        MockResponseSettings Settings { get; }
        /// <summary>Gets or sets the return value.</summary>
        /// <value>The return value.</value>
        int ReturnValue { get; set; }
        /// <summary>Gets or sets the scalar value.</summary>
        /// <value>The scalar value.</value>
        object ScalarReturnValue { get; set; }
        /// <summary>
        /// Adds the response data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        void Add<T>(IEnumerable<T> responseData) where T : class;

        /// <summary>Adds the response data.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        void Add<T>(params T[] responseData) where T : class;

        /// <summary>Clears this instance.</summary>
        void Clear();
    }

    
    internal class MockResponse : IMockResponse
    {

        private readonly EndpointSettings EndpointSettings;
        public int ReturnValue { get; set; }
        public object ScalarReturnValue { get; set; }
        public DataCollection OutputValues { get; private set; }
        private readonly DataSet TestDataSet;
        private string XmlString { get; set; }

        public MockResponseSettings Settings { get; private set; }
        internal MockResponse(EndpointSettings endpointSettings)
        {
            EndpointSettings = endpointSettings;
            TestDataSet = CreateDataSet();
            ReturnValue = default(int);
            OutputValues = new DataCollection();
            Settings = new MockResponseSettings();
        }
        internal static DataSet CreateDataSet()
        => new DataSet($"MockDataSetUID{Guid.NewGuid()}");
        internal static DataSet ResolveDataSet(DataSet dataSet)
        {
            if (dataSet.Tables.Count == 0)
            {
                dataSet.Tables.Add(new DataTable("EmptyMockDbTable"));
            }
            return dataSet;
        }

        public void Add<T>(IEnumerable<T> responseData)
            where T : class
        {
            if (EndpointSettings.ExecutionMethod == ExecutionMethod.Xml)
            {
                SetXml(responseData);
            }
            else
            {
                TestDataSet.Tables.Add(responseData.ToDataTable());
            }
        }
        public void Add<T>(params T[] responseData)
            where T : class
            => Add((IEnumerable<T>)responseData);

        private void SetXml<T>(IEnumerable<T> responseData)
            where T : class
        {
            var accessor = Accessor<T>.GetInstance();
            var refs = accessor.VariableReferences.Values;
            List<string> attributeTypeList = new List<string>();
            List<string> attributeList = new List<string>();
            List<VariableReference> validRefs = new List<VariableReference>();
            foreach (var variableRef in refs)
            {
                string dtType = GetDtType(variableRef.VariableType);
                if (dtType != null)
                {
                    attributeTypeList.Add($"<AttributeType name=\"{variableRef.Name}\" dt:type=\"{dtType}\" />");
                    attributeList.Add($"<attribute type=\"{variableRef.Name}\" />");
                    validRefs.Add(variableRef);
                }
            }
            var attributeTypes = string.Join("", attributeTypeList);
            var attributes = string.Join("", attributeList);

            List<string> dataList = new List<string>();
            foreach (var data in responseData)
            {
                List<string> propList = new List<string>();
                foreach (var vr in validRefs)
                {
                    var val = vr.Get(data);
                    if (val != null)
                    {
                        propList.Add($"{vr.Name}=\"{FormatToDBString(val)}\"");
                    }
                }
                var props = string.Join(" ", propList);
                dataList.Add($"<{accessor.Name} xmlns=\"x-schema:#Schema1\" {props} />");
            }
            var dataStr = string.Join("", dataList);
            string elementType = $"<ElementType name=\"{accessor.Name}\" content=\"empty\" model=\"closed\">{attributeTypes}{attributes}</ElementType>";
            XmlString = $"<Schema name=\"Schema1\" xmlns=\"urn:schemas-microsoft-com:xml-data\" xmlns:dt=\"urn:schemas-microsoft-com:datatypes\">{elementType}</Schema>{dataStr}";

        }


        private static string GetDtType(Type type)
        {
            if (type == typeof(byte))
                return "ui1";
            else if (type == typeof(sbyte))
                return "ui1";
            else if (type == typeof(short))
                return "i2";
            else if (type == typeof(int))
                return "i4";
            else if (type == typeof(long))
                return "i8";
            else if (type == typeof(ushort))
                return "ui2";
            else if (type == typeof(uint))
                return "ui4";
            else if (type == typeof(ulong))
                return "ui8";
            else if (type == typeof(float))
                return "r4";
            else if (type == typeof(double))
                return "r8";
            else if (type == typeof(decimal))
                return "number";
            else if (type == typeof(bool))
                return "boolean";
            else if (type == typeof(char))
                return "string";
            else if (type == typeof(Guid))
                return "uuid";
            else if (type == typeof(DateTime))
                return "dateTime";
            else if (type == typeof(string))
                return "string";
            else
                return null;
        }

        private static string FormatToDBString(object value)
        {
            if (value is byte b)
                return XmlConvert.ToString(b);
            else if (value is sbyte sb)
                return XmlConvert.ToString(sb);
            else if (value is short s)
                return XmlConvert.ToString(s);
            else if (value is int i)
                return XmlConvert.ToString(i);
            else if (value is long l)
                return XmlConvert.ToString(l);
            else if (value is ushort us)
                return XmlConvert.ToString(us);
            else if (value is uint ui)
                return XmlConvert.ToString(ui);
            else if (value is ulong ul)
                return XmlConvert.ToString(ul);
            else if (value is float f)
                return XmlConvert.ToString(f);
            else if (value is double d)
                return XmlConvert.ToString(d);
            else if (value is decimal dc)
                return XmlConvert.ToString(dc);
            else if (value is bool bl)
                return XmlConvert.ToString(bl);
            else if (value is char c)
                return XmlConvert.ToString(c);
            else if (value is Guid guid)
                return XmlConvert.ToString(guid);
            else if (value is DateTime dt)
                return XmlConvert.ToString(dt, XmlDateTimeSerializationMode.Unspecified);
            else
                return value.ToString();
        }


        public void Clear()
        {
            Settings.Clear();
            OutputValues.Clear();
            TestDataSet.Tables.Clear();
            ReturnValue = default(int);
            XmlString = null;
        }

        internal DataTableReader GetResponseData()
        => ResolveDataSet(TestDataSet).CreateDataReader();
        internal XmlReader GetResponseDataXml()
        => XmlReader.Create(new StringReader(XmlString), Settings.XmlReaderSettings);
    }

}