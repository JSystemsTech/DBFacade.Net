namespace DbFacade.DataLayer.Models.Parameters
{
    public sealed class AnsiStringFixedLength
    {
        public AnsiStringFixedLength(string value)
        {
            Value = value;
        }

        public readonly string Value;
        public override string ToString() => Value;
    }
}
