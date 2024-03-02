using System.ComponentModel.DataAnnotations;
using wan24.Core;

namespace wan24.Crypto.NaCl
{
    /// <summary>
    /// Argon2id KDF algorithm options
    /// </summary>
    public sealed record class KdfArgon2IdOptions
    {
        /// <summary>
        /// Degree of parallelism
        /// </summary>
        private int _Parallelism = KdfArgon2IdAlgorithm.DEFAULT_PARALLELISM;
        /// <summary>
        /// Memory limit in bytes
        /// </summary>
        private long _MemoryLimit = KdfArgon2IdAlgorithm.DEFAULT_MEMORY;

        /// <summary>
        /// Constructor
        /// </summary>
        public KdfArgon2IdOptions() { }

        /// <summary>
        /// Default degree of parallelism
        /// </summary>
        public static int DefaultParallelism { get; set; } = KdfArgon2IdAlgorithm.DEFAULT_PARALLELISM;

        /// <summary>
        /// Default memory limit in bytes
        /// </summary>
        public static long DefaultMemoryLimit { get; set; } = KdfArgon2IdAlgorithm.DEFAULT_MEMORY;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="json">JSON string</param>
        public KdfArgon2IdOptions(string json)
        {
            KdfArgon2IdOptions options = (json ?? throw new ArgumentException("Invalid JSON data", nameof(json)))!;
            Parallelism = options.Parallelism;
            MemoryLimit = options.MemoryLimit;
        }

        /// <summary>
        /// Degree of parallelism
        /// </summary>
        [Range(KdfArgon2IdAlgorithm.DEFAULT_PARALLELISM, KdfArgon2IdAlgorithm.MAX_PARALLELISM)]
        public int Parallelism
        {
            get => _Parallelism;
            set
            {
                if (value < KdfArgon2IdAlgorithm.DEFAULT_PARALLELISM || value > KdfArgon2IdAlgorithm.MAX_PARALLELISM) throw new ArgumentOutOfRangeException(nameof(value));
                _Parallelism = value;
            }
        }

        /// <summary>
        /// Memory limit in bytes
        /// </summary>
        [Range(KdfArgon2IdAlgorithm.DEFAULT_MEMORY, KdfArgon2IdAlgorithm.MAX_MEMORY)]
        public long MemoryLimit
        {
            get => _MemoryLimit;
            set
            {
                if (value < KdfArgon2IdAlgorithm.DEFAULT_MEMORY || value > KdfArgon2IdAlgorithm.MAX_MEMORY) throw new ArgumentOutOfRangeException(nameof(value));
                _MemoryLimit = value;
            }
        }

        /// <summary>
        /// Get a copy of this instance
        /// </summary>
        /// <returns>Instance copy</returns>
        public KdfArgon2IdOptions GetCopy() => new()
        {
            _Parallelism = _Parallelism,
            _MemoryLimit = _MemoryLimit
        };

        /// <inheritdoc/>
        public override string ToString() => this;

        /// <summary>
        /// Cast as JSON string
        /// </summary>
        /// <param name="options">Options</param>
        public static implicit operator string(KdfArgon2IdOptions options) => options.ToJson();

        /// <summary>
        /// Cast as options
        /// </summary>
        /// <param name="json">JSON string</param>
        public static implicit operator KdfArgon2IdOptions?(string? json)
            => json is null ? null : json.DecodeJson<KdfArgon2IdOptions>() ?? throw new InvalidDataException("Invalid JSON data");

        /// <summary>
        /// Cast as <see cref="CryptoOptions"/>
        /// </summary>
        /// <param name="options">Options</param>
        public static implicit operator CryptoOptions(KdfArgon2IdOptions options) => new()
        {
            KdfAlgorithm = KdfArgon2IdAlgorithm.ALGORITHM_NAME,
            KdfIterations = KdfArgon2IdAlgorithm.Instance.DefaultIterations,
            KdfOptions = options
        };
    }
}
