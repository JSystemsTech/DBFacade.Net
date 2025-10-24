namespace DbFacade.DataLayer.Models.Parameters
{
    public sealed class StringFixedLength
    {
        public StringFixedLength(string value)
        {
            Value = value;
        }

        public readonly string Value;
        public override string ToString() => Value;
    }
}
