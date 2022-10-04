# BlockchainTestProject

Sinple console app in C# that receives some subset of transactions, and processes them in such a way that enables the program to answer questions about NFT ownership.

# Commands
Read File (`--read-file <file>`)
Reads either a single json element, or an array of json elements representing
transactions from the file in the specified location.
`program --read-file transactions.json`

NFT Ownership (`--nft <id>`)
Returns ownership information for the nft with the given id
`program --nft 0x...`

Wallet Ownership (`--wallet <address>`)
Lists all NFTs currently owned by the wallet of the given address
program `--wallet 0x...`

Example command

```dotnet BlockchainTestProject.Cli.dll --read-file transactions.json --wallet 0x3000000000000000000000000000000000000000 --nft 0xB000000000000000000000000000000000000000```

result
```Mint transaction processed.
Mint transaction processed.
Mint transaction processed.
Burn transaction processed.
Transfer transaction processed.
Wallet 0x3000000000000000000000000000000000000000 2 Tokens: 0xC000000000000000000000000000000000000000 0xB000000000000000000000000000000000000000
Token 0xB000000000000000000000000000000000000000 is owned by 0x3000000000000000000000000000000000000000```
