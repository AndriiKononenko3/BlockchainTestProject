using BlockchainTestProject.Application.Commands;
using BlockchainTestProject.Application.Models;
using BlockchainTestProject.Application.Queries;
using BlockchainTestProject.Infrastructure;
using BlockchainTestProject.InputTransactions;
using BlockchainTestProject.Output;
using CommandLine;
using MediatR;
using Newtonsoft.Json;
using OneOf;

namespace BlockchainTestProject.Application;

public class BlockchainApplication
{
    private readonly IConsoleWriter _consoleWriter;
    private readonly IMediator _mediator;


    public BlockchainApplication(IConsoleWriter consoleWriter, IMediator mediator)
    {
        _consoleWriter = consoleWriter;
        _mediator = mediator;
    }
    
    public async Task RunAsync(string[] args)
    {
        await Parser.Default
            .ParseArguments<BlockchainApplicationOptions>(args)
            .WithParsedAsync(async option =>
            {
                if (!string.IsNullOrWhiteSpace(option.FilePath))
                {
                    if (!File.Exists(option.FilePath))
                    {
                        _consoleWriter.WriteLine("File not found");
                        return;
                    }
                    
                    var jsonString = await File.ReadAllTextAsync(option.FilePath);
                    var transactionsList = JsonConvert.DeserializeObject<List<BaseTransaction>>(jsonString, new TransactionConverter());

                    foreach (var transaction in transactionsList)
                    {
                        switch (transaction)
                        {
                            case MintTransaction mintTransaction:
                            {
                                var result = await _mediator.Send(
                                    new MintNftCommand.Request(
                                        new MintCommandModel.Request(
                                            mintTransaction.Address,
                                            mintTransaction.TokenId)));

                                HandleCommandResult(result, "Mint transaction processed.");
                                break;
                            }
                            case BurnTransaction burnTransaction:
                            {
                                var result = await _mediator.Send(
                                    new BurnNftCommand.Request(
                                        new BurnNftCommandModel.Request(
                                            burnTransaction.TokenId)));

                                HandleCommandResult(result, "Burn transaction processed.");
                                break;
                            }
                            case TransferTransaction transferTransaction:
                            {
                                var result = await _mediator.Send(
                                    new TransferNftCommand.Request(
                                        new TransferNftCommandModel.Request(
                                            transferTransaction.From,
                                            transferTransaction.To,
                                            transferTransaction.TokenId)));

                                HandleCommandResult(result, "Transfer transaction processed.");
                                break;
                            }
                        }
                    }
                }
                
                if (!string.IsNullOrWhiteSpace(option.WalletAddressId))
                {
                    var result = await _mediator.Send(
                        new GetWalletDetailsQuery.Request(
                            new GetWalletDetailsQueryModel.Request(
                                option.WalletAddressId)));
                    
                    result.Switch(searchResult =>
                        {
                            _consoleWriter.WriteLine($"Wallet {searchResult.AddressId} {searchResult.Tokens.Count()} Tokens: {string.Join(" ", searchResult.Tokens)}");
                        },
                        none =>
                        {
                            _consoleWriter.WriteLine("no results");
                        });
                }
                
                if (!string.IsNullOrWhiteSpace(option.TokenId))
                {
                    var result = await _mediator.Send(
                        new GetNftDetailsQuery.Request(
                            new GetNftDetailsQueryModel.Request(
                                option.TokenId)));
                    
                    result.Switch(searchResult =>
                        {
                            _consoleWriter.WriteLine($"Token {searchResult.TokenId} is owned by {searchResult.AddressId}");
                        },
                        none =>
                        {
                            _consoleWriter.WriteLine("no results");
                        });
                }
            });
    }
    
    private void HandleCommandResult(OneOf<bool, string> result, string successMessage)
    {
        result.Switch(success =>
            {
                _consoleWriter.WriteLine(successMessage);
            },
            error =>
            {
                _consoleWriter.WriteLine(error);
            });
    }
    
    private void HandleCommandResult(OneOf<bool, IEnumerable<string>> result, string successMessage)
    {
        result.Switch(success =>
            {
                _consoleWriter.WriteLine(successMessage);
            },
            error =>
            {
                var formattedErrors = string.Join(", ", error);
                _consoleWriter.WriteLine(formattedErrors);
            });
    }
}

