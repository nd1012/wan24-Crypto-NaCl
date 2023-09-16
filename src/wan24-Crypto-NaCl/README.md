# wan24-Crypto-NaCl

This library adopts [NSec](https://github.com/ektrah/nsec) to 
[wan24-Crypto](https://www.nuget.org/packages/wan24-Crypto/) and extends the 
`wan24-Crypto` library with these algorithms:

| Algorithm | ID | Name |
| --- | --- | --- |
| **KDF** |  |  |
| Argon2id | 1 | ARGON2ID |

## How to get it

This library is available as 
[NuGet package](https://www.nuget.org/packages/wan24-Crypto-NaCl/).

## Usage

In case you don't use the `wan24-Core` bootstrapper logic, you need to 
initialize the NaCl extension first, before you can use it:

```cs
wan24.Crypto.NaCl.Bootstrapper.Boot();
```

This will register the algorithms to the `wan24-Crypto` library.
