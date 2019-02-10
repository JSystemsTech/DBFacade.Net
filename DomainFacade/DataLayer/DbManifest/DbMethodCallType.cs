namespace DomainFacade.DataLayer.DbManifest
{
    public abstract class DbMethodCallType
    {
        public sealed class Transaction : DbMethodCallType { }
        public sealed class FetchRecord : DbMethodCallType { }
        public sealed class FetchRecords : DbMethodCallType { }
        public sealed class TransactionWithReturn : DbMethodCallType { }
        public sealed class FetchRecordWithReturn : DbMethodCallType { }
        public sealed class FetchRecordsWithReturn : DbMethodCallType { }
    }
}
