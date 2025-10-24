namespace DbFacade.DataLayer.Models.Parameters
{
    public sealed class Currency
    {
        public Currency(decimal value)
        {
            Value = value;
        }

        public readonly decimal Value;
        public override string ToString() => Value.ToString();
    }
}
