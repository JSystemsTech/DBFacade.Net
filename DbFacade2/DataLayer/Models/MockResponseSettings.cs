using System.Xml;

namespace DbFacade.DataLayer.Models
{
    public class MockResponseSettings
    {
        /// <summary>Gets or sets the connection creation error.</summary>
        /// <value>The connection creation error.</value>
        public string ConnectionCreationError { get; set; }
        /// <summary>Gets or sets the connection open error.</summary>
        /// <value>The connection open error.</value>
        public string ConnectionOpenError { get; set; }
        /// <summary>Gets or sets the execute XML error.</summary>
        /// <value>The execute XML error.</value>
        public string ExecuteXmlError { get; set; }
        /// <summary>Gets or sets the execute scalar error.</summary>
        /// <value>The execute scalar error.</value>
        public string ExecuteScalarError { get; set; }
        /// <summary>Gets or sets the execute query error.</summary>
        /// <value>The execute query error.</value>
        public string ExecuteQueryError { get; set; }
        /// <summary>Gets or sets the execute non query error.</summary>
        /// <value>The execute non query error.</value>
        public string ExecuteNonQueryError { get; set; }
        /// <summary>Gets or sets a value indicating whether [use null connection].</summary>
        /// <value>
        ///   <c>true</c> if [use null connection]; otherwise, <c>false</c>.</value>
        public bool UseNullConnection { get; set; }
        /// <summary>Gets or sets the begin transaction error.</summary>
        /// <value>The begin transaction error.</value>
        public string BeginTransactionError { get; set; }
        /// <summary>Gets or sets a value indicating whether [use null transaction].</summary>
        /// <value>
        ///   <c>true</c> if [use null transaction]; otherwise, <c>false</c>.</value>
        public bool UseNullTransaction { get; set; }
        /// <summary>Gets or sets the transaction rollback error.</summary>
        /// <value>The transaction rollback error.</value>
        public string TransactionRollbackError { get; set; }
        /// <summary>Gets the XML reader settings.</summary>
        /// <value>The XML reader settings.</value>
        public XmlReaderSettings XmlReaderSettings { get; private set; }
        internal MockResponseSettings()
        {
            XmlReaderSettings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment };
        }
        internal void Clear()
        {
            XmlReaderSettings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment };
            ConnectionCreationError = null;
            ConnectionOpenError = null;
            ExecuteXmlError = null;
            ExecuteScalarError = null;
            ExecuteQueryError = null;
            ExecuteNonQueryError = null;
            UseNullConnection = false;
            BeginTransactionError = null;
            UseNullTransaction = false;
            TransactionRollbackError = null;
        }
    }
}
