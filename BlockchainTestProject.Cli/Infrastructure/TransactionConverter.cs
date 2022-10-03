using BlockchainTestProject.InputTransactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlockchainTestProject.Infrastructure;

public class TransactionConverter : JsonConverter
{
    public override void WriteJson(
        JsonWriter writer,
        object? value,
        JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        
        var type = (string)jo["Type"]!;

        if (type.Equals(TransactionType.Mint.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return jo.ToObject<MintTransaction>();
        }
        
        if (type.Equals(TransactionType.Burn.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return jo.ToObject<BurnTransaction>();
        }
        
        if (type.Equals(TransactionType.Transfer.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return jo.ToObject<TransferTransaction>();
        }
        
        throw new NotImplementedException();
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(BaseTransaction).IsAssignableFrom(objectType);
    }

}