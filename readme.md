# Passman
Passman is a CLI password manager that stores encrypted login information locally on your machine

## Todo
- [X] Generate password
- [ ] Copy to clipboard
- [ ] Edit Command
- [ ] Master Password session timeout
- [ ] Help system
- [ ] Binary release

## Setup
1. Clone the repo
2. Make sure you have .NET installed
3. Run the following commands to install the tool globally
```shell
dotnet build
cd src/Passman.Cli
dotnet pack -c Release
dotnet tool install --global --add-source ./nupkg passman.cli
```

## Commands
### Init
Usage: `passman init`  

This is the command to initialize your database and **must be the first one** you use. This will also set the master password that you will enter for all other commands that access your database

| Flag | Description |  
| --- | --- |  
| `--csv` | Enter a path to a csv file containing credentials you want to import. Leave empty to initialize an empty database |

**Note: the csv file **must** be in the following format in order to correctly import:

```
site,username,password
example,user,password123
...
```

### Add
Usage: `passman add`

Use this to add a credential to your database

### Search
Usage: `passman search <arg>`

Use this to display credentials for a site you want to search for. It will return all matching websites that contain the string you enter

### Delete
Usage: `passman delete <arg>`

This will search your database and return all matching sites that contain the string you entered. If there are multiple matches, it will display them. You then get to choose from among the results which you would like to delete.

## About
### Encryption
All sensitive data stored by Passman — including usernames and passwords — is encrypted locally to protect it from unauthorized access.

**Key Derivation**  
The master password you set in the `init` command is processed using PBKDF2 (RFC 2898) with a salt and multiple iterations. This ensures that even if two users choose the same password, their encryption keys are unique and resistant to brute-force attacks. 

**AES-GCM Encryption**  
The actual data (credentials) is encrypted using AES in Galois/Counter Mode (AES-GCM)

**Nonce and Tag**  
Each encryption operation uses a unique nonce (similar to an initialization vector) and produces an authentication tag to verify data integrity during decryption.

**Secure Storage**  
The database file stores:
- Encrypted credentials
- The salt used for key derivation
- The nonce and authentication tag (this allows the database to be safely restored or moved to another machine — all you need is the master password)