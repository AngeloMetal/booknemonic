# booknemonic
Encrypt and sign with memorizable words instead of large PGP keys.

Major updates:
09/02/2021: Added option for having your own contacts.
14/01/2020: Added option for file encryption/decryption. (Asymmetric)

Screenshots:</br>
<img width="500" src="https://booknemonic.org/screenshot1.png"></br></br><img width="500" src="https://booknemonic.org/screenshot2.png">

# Building
You can build Booknemonic using Visual Studio 2019 (with .NET 5). You'll have to firsly install these bitcoin related libraries:
<ul>
  <li><a href="https://github.com/MetacoSA/NBitcoin">NBitcoin</a></li>
  <li><a href="https://github.com/Autarkysoft/Denovo">Denovo</a></li>
  </ul>

Once you open Booknemonic.sln, hit "Build" on top of the menu and then "Build Solution".
The binaries should be on ``C:\Users\USERNAME\source\repos\Booknemonic\Booknemonic\bin\Debug\net5.0-windows``
