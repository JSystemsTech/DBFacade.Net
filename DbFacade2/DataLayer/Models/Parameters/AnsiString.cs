namespace DbFacade.DataLayer.Models.Parameters
{
    public sealed class AnsiString
    {
        public AnsiString(string value)
        {
            Value = value;
        }

        public readonly string Value;
        public override string ToString() => Value;
    }
}
