# BlockchainTestProject

Sinple console app in C# that receives some subset of transactions, and processes them in such a way that enables the program to answer questions about NFT ownership.

# Commands
Read File (--read-file <file>)
Reads either a single json element, or an array of json elements representing
transactions from the file in the specified location.
program --read-file transactions.json

NFT Ownership (--nft <id>)
Returns ownership information for the nft with the given id
program --nft 0x...

Wallet Ownership (--wallet <address>)
Lists all NFTs currently owned by the wallet of the given address
program --wallet 0x...
