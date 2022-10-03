using CommandLine;

namespace BlockchainTestProject.Application;

public class BlockchainApplicationOptions
{
    // TODO: implement option
    [Option('j', "read-inline", Required = false, HelpText = "Reads either a single json element, or an array of json elements representing transactions as an argument.")]
    public string FileJson{ get; init; }
    
    [Option('r', "read-file", Required = false, HelpText = "Reads either a single json element, or an array of json elements representing transactions from the file in the specified location.")]
    public string FilePath { get; init; }
    
    [Option('w', "wallet", Required = false, HelpText = "Lists all NFTs currently owned by the wallet of the given address.")]
    public string WalletAddressId { get; init; }
    
    [Option('t', "nft", Required = false, HelpText = "Returns ownership information for the nft with the given id.")]
    public string TokenId { get; init; }
    
    // TODO: implement option
    [Option('d', "reset", Required = false, HelpText = "Returns ownership information for the nft with the given id.")]
    public string Reset { get; init; }
}