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

To set NaCl defaults as `wan24-Crypto` defaults:

```cs
NaClHelper.SetDefaults();
```

Per default the current `wan24-Crypto` default will be set as counter 
algorithms to `HybridAlgorithmHelper`.

## Argon2id

A simple KDF operation example:

```cs
(byte[] stretchedPwd, byte[] salt) = KdfArgon2IdAlgorithm.Instance.Stretch(pwd, len: 32);
```

To use Argon2id as default KDF algorithm:

```cs
KdfHelper.DefaultAlgorithm = KdfArgon2IdAlgorithm.Instance;
```

You may specify Argon2id specific options using the `KdfArgon2IdOptions`, 
which cast implicit to/from a JSON string. The default options are the OWASP 
recommendations (46M memory usage):

```cs
(byte[] stretchedPwd, byte[] salt) = pwd.Stretch(len: 32, options: new KdfArgon2IdOptions()
	{
		// Configure the options here
	});// KdfArgon2IdOptions cast implicit to CryptoOptions
```

Or when using `CryptoOptions`:

```cs
CryptoOptions options = new()
{
	KdfAlgorithm = KdfArgon2IdAlgorithm.ALGORITHM_NAME,
	KdfIterations = KdfArgon2IdAlgorithm.Instance.DefaultIterations,
	KdfOptions = new KdfArgon2IdOptions()
	{
		// Configure the options here
	}
};
```
