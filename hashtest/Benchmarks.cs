using BenchmarkDotNet.Attributes;
using HashDepot;
using Blake2Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashtest
{
    public class Benchmarks
    {
        public static string text = "veni vidi vici";
        public static byte[] data = Encoding.UTF8.GetBytes(text);

        /// <summary>
        /// Small: 4 bytes of data ("veni", from 'Veni vidi vici')
        /// </summary>
        private byte[] smallInput;
        /// <summary>
        /// Medium: 1 KB of randomized data
        /// </summary>
        private byte[] mediumInput;
        /// <summary>
        /// Large: 1 MB of randomized data
        /// </summary>
        private byte[] largeInput;

        /// <summary>
        /// Test input strings for hashing.
        /// </summary>
        public static string[] testInputs = new string[]
        {
            "Hello, World!",
            "The quick brown fox jumps over the lazy dog",
            "",
            "a",
            "abc",
            "        ",
            "abcdefghijklmnopqrstuvwxyz",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
            "0000000000000000000000000000000000000000000000000000000000000000",
            "1111111111111111111111111111111111111111111111111111111111111111",
            "123456789012345678901234567890",
            "0101010101010101010101010101010101010101010101010101010101010101",
            "0101011101010111010101010101011101010111000101010001110101010100"
        };

        [Benchmark]
        public ulong XXHash32_Small() => XXHash.Hash32(smallInput);
        [Benchmark]
        public ulong XXHash32_Medium() => XXHash.Hash32(mediumInput);
        [Benchmark]
        public ulong XXHash32_Large() => XXHash.Hash32(largeInput);
        [Benchmark]
        public ulong SipHash24_Small() => SipHash24.Hash64(smallInput, new byte[16]);
        [Benchmark]
        public ulong SipHash24_Medium() => SipHash24.Hash64(mediumInput, new byte[16]);
        [Benchmark]
        public ulong SipHash24_Large() => SipHash24.Hash64(largeInput, new byte[16]);
        [Benchmark]
        public byte[] Blake2b_Small() => Blake2b.ComputeHash(smallInput);
        [Benchmark]
        public byte[] Blake2b_Medium() => Blake2b.ComputeHash(mediumInput);
        [Benchmark]
        public byte[] Blake2b_Large() => Blake2b.ComputeHash(largeInput);

        /// <summary>
        /// Benchmark setup to initialize inputs.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            smallInput = Encoding.UTF8.GetBytes("veni");
            mediumInput = new byte[1024]; // 1 KB
            largeInput = new byte[1024 * 1024]; // 1 MB

            // Fill with some predictable data
            for (int i = 0; i < mediumInput.Length; i++) mediumInput[i] = (byte)(i % 256);
            for (int i = 0; i < largeInput.Length; i++) largeInput[i] = (byte)(i % 256);
        }

        /// <summary>
        /// Benchmark method to run through predefined test vectors.
        /// </summary>
        [Benchmark]
        public void MicroHash_TestVectors()
        {
            var key = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            foreach (var input in testInputs)
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                ulong hash = XXHash.Hash32(data);
                Console.WriteLine($"Input: \"{input}\", Hash32: 0x{hash:X8}");
                ulong hash64 = XXHash.Hash64(data);
                Console.WriteLine($"Input: \"{input}\", Hash64: 0x{hash64:X16}");
                ulong hash3 = SipHash24.Hash64(data, key);
                Console.WriteLine($"Input: \"{input}\", SipHash24: 0x{hash3:X16}");
            }
        }
    }
}
