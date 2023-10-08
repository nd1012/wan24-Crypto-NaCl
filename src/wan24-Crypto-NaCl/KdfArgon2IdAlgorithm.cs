using NSec.Cryptography;
using System.Security.Cryptography;

namespace wan24.Crypto.NaCl
{
    /// <summary>
    /// Argon2id KDF algorithm
    /// </summary>
    public sealed record class KdfArgon2IdAlgorithm : KdfAlgorithmBase
    {
        /// <summary>
        /// Algorithm name
        /// </summary>
        public const string ALGORITHM_NAME = "ARGON2ID";
        /// <summary>
        /// Algorithm value
        /// </summary>
        public const int ALGORITHM_VALUE = 1;
        /// <summary>
        /// Default degree of parallelism (p=fixed to 1 currently)
        /// </summary>
        public const int DEFAULT_PARALLELISM = 1;
        /// <summary>
        /// Max. degree of parallelism
        /// </summary>
        public const int MAX_PARALLELISM = 16_777_215;
        /// <summary>
        /// Default memory size (m=46M)
        /// </summary>
        public const long DEFAULT_MEMORY = 47_104;
        /// <summary>
        /// Max. memory size
        /// </summary>
        public const long MAX_MEMORY = 4_294_967_295;
        /// <summary>
        /// Default number of passes (t=1)
        /// </summary>
        public const int DEFAULT_PASSES = 1;
        /// <summary>
        /// Max. number of passes
        /// </summary>
        public const int MAX_PASSES = int.MaxValue;// 2^32-1 actually
        /// <summary>
        /// Default salt bytes length
        /// </summary>
        public const int DEFAULT_SALT_LEN = 16;
        /// <summary>
        /// Display name
        /// </summary>
        public const string DISPLAY_NAME = "Argon2id";

        /// <summary>
        /// Default degree of parallelism
        /// </summary>
        private int _DefaultParallelism = DEFAULT_PARALLELISM;
        /// <summary>
        /// Default memory limit
        /// </summary>
        private long _DefaultMemoryLimit = DEFAULT_MEMORY;
        /// <summary>
        /// Default number of passes
        /// </summary>
        private int _DefaultIterations = DEFAULT_PASSES;

        /// <summary>
        /// Static constructor
        /// </summary>
        static KdfArgon2IdAlgorithm() => Instance = new();

        /// <summary>
        /// Constructor
        /// </summary>
        public KdfArgon2IdAlgorithm() : base(ALGORITHM_NAME, ALGORITHM_VALUE) => DefaultKdfOptions = new KdfArgon2IdOptions();

        /// <summary>
        /// Instance
        /// </summary>
        public static KdfArgon2IdAlgorithm Instance { get; }

        /// <inheritdoc/>
        public override int DefaultIterations
        {
            get => _DefaultIterations;
            set
            {
                if (value < DEFAULT_PASSES || value > MAX_PASSES) throw new ArgumentOutOfRangeException(nameof(value));
                _DefaultIterations = value;
            }
        }

        /// <summary>
        /// Default degree of parallelism
        /// </summary>
        public int DefaultParallelism
        {
            get => _DefaultParallelism;
            set
            {
                if (value < DEFAULT_PARALLELISM || value > MAX_PARALLELISM) throw new ArgumentOutOfRangeException(nameof(value));
                _DefaultParallelism = value;
            }
        }

        /// <summary>
        /// Default memory limit
        /// </summary>
        public long DefaultMemoryLimit
        {
            get => _DefaultMemoryLimit;
            set
            {
                if (value < DEFAULT_MEMORY || value > MAX_MEMORY) throw new ArgumentOutOfRangeException(nameof(value));
                _DefaultMemoryLimit = value;
            }
        }

        /// <inheritdoc/>
        public override int MinIterations => DEFAULT_PASSES;

        /// <inheritdoc/>
        public override int MinSaltLength => DEFAULT_SALT_LEN;

        /// <inheritdoc/>
        public override int SaltLength => DEFAULT_SALT_LEN;

        /// <inheritdoc/>
        public override bool IsPostQuantum => true;

        /// <inheritdoc/>
        public override string DisplayName => DISPLAY_NAME;

        /// <inheritdoc/>
        public override (byte[] Stretched, byte[] Salt) Stretch(byte[] pwd, int len, byte[]? salt = null, CryptoOptions? options = null)
        {
            try
            {
                if (len < 1) throw new ArgumentOutOfRangeException(nameof(len));
                options ??= DefaultOptions;
                options = KdfHelper.GetDefaultOptions(options);
                if (options.KdfIterations < DEFAULT_PASSES || options.KdfIterations > MAX_PASSES)
                    throw new ArgumentException("Invalid KDF iterations", nameof(options));
                salt ??= RandomNumberGenerator.GetBytes(DEFAULT_SALT_LEN);
                if (salt.Length < DEFAULT_SALT_LEN) throw new ArgumentException("Invalid salt length", nameof(salt));
                KdfArgon2IdOptions kdfOptions = ((options.KdfOptions ??= DefaultKdfOptions) ?? new KdfArgon2IdOptions())!;
                return (
                    PasswordBasedKeyDerivationAlgorithm.Argon2id(new()
                    {
                        DegreeOfParallelism = kdfOptions.Parallelism,
                        MemorySize = kdfOptions.MemoryLimit,
                        NumberOfPasses = options.KdfIterations
                    }).DeriveBytes(pwd, salt, len),
                    salt
                    );
            }
            catch (CryptographicException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw CryptographicException.From(ex);
            }
        }

        /// <summary>
        /// Register the algorithm to the <see cref="CryptoConfig"/>
        /// </summary>
        public static void Register() => CryptoConfig.AddAlgorithm(typeof(KdfArgon2IdAlgorithm), ALGORITHM_NAME);
    }
}
