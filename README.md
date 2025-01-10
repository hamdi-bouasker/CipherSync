###Cipher Sync Project

##Description
Cipher Sync protects your files and passwords from unauthorized intruders.

##Features

#Master Password
A Master Password is registered when you use the app for the first time.

The Master Password is automatically backed up in an encrypted file named Master-Password.dat.

The file is encrypted using the Data Protection API (DPAPI) with AES-256 encryption.

You can log in by clicking on the button "Load Master Password".

#Password Generator
Generates complex passwords composed of:

Uppercase letters

Lowercase letters

Digits

Special characters

#Files Encryption
Encrypts files using AES-256 encryption.

The password used for encryption is saved to an encrypted backup file.

You can load the encrypted password directly with a click.

#Password Manager
Inputs are saved to an encrypted database.

The database uses SQLCipher with AES-256 encryption.

#REGEX Files Renamer
Easily rename your files using C# regex symbols.

Additionally, use a counter to rename your files incrementally.

###Getting Started
##Prerequisites
Ensure you have Microsoft .NET Framework installed on your system.

##Installation
Clone the repository:

git clone https://github.com/yourusername/cipher-sync.git

Open the solution file (CipherSync.sln) in Visual Studio.

##Usage
Master Password:

Register your Master Password upon the first use of the app.

Load the Master Password using the "Load Master Password" button.

##Contributing
Contributions are welcome! Please fork the repository and create a pull request.

##License
This project is licensed under the MIT License.
