# _Cipher Sync Project_

## _Description_
Cipher Sync protects your files and passwords from unauthorized intruders.

## _Features_

### _Master Password_

- A Master Password is registered when you use the app for the first time.

- The Master Password is automatically backed up in an encrypted file named Master-Password.dat.

- The file is encrypted using the Data Protection API (DPAPI) with AES-256 encryption.

- You can log in by clicking on the button **Load Master Password**.

### _Password Generator_

- Generates complex passwords composed of:

- Uppercase letters

- Lowercase letters

- Digits

- Special characters

### _Files Encryption_

- Encrypts files using AES-256 encryption.

- The password used for encryption is saved to an encrypted backup file.

- You can load the encrypted password directly with a click.

### _Password Manager_

- Inputs are saved to an encrypted database.

- The database uses SQLCipher with AES-256 encryption.

### _REGEX Files Renamer_

- Easily rename your files using C# regex symbols.

- Additionally, use a counter to rename your files incrementally by inserting in Replacement tab this symbol **{n}**

## _Getting Started_

## _Prerequisites_

Ensure you have Microsoft .NET Framework installed on your system.

### _Installation_

Clone the repository:

```git clone https://github.com/yourusername/cipher-sync.git```

Open the solution file **CipherSync.sln** in Visual Studio.

## _Usage_

### _Master Password_

- Register your Master Password upon the first use of the app.

- Load the Master Password using the "Load Master Password" button.

## _Contributing_

Contributions are welcome! Please fork the repository and create a pull request.

## _License_
This project is licensed under the MIT License.

## _Hint_

The Installer is made without a signing it using thrid-part authority such as comodo which requires $200 yearly.
When using the installer, you will be warned not to use it, this simply because the installer is not signed as explained, else it's 100% safe.
Otherwise, just compile the code.
