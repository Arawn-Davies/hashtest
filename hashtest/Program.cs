using Blake2Fast;
using Standart.Hash.xxHash;
using System.Diagnostics;
using System.Text;


namespace hashtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            XXHashTest();
            Blake2Test();
            Console.ReadLine();
        }

        static async void XXHashTest()
        {
            Stopwatch sw = new Stopwatch();

            byte[] data = Encoding.UTF8.GetBytes("veni vidi vici");

            ulong h64_1 = xxHash64.ComputeHash(data, data.Length);
            ulong h64_2 = xxHash64.ComputeHash(new Span<byte>(data), data.Length);
            ulong h64_3 = xxHash64.ComputeHash(new ReadOnlySpan<byte>(data), data.Length);
            ulong h64_4 = xxHash64.ComputeHash(new MemoryStream(data));
            ulong h64_5 = await xxHash64.ComputeHashAsync(new MemoryStream(data));
            ulong h64_6 = xxHash64.ComputeHash("veni vidi vici");
            sw.Stop();
            Console.WriteLine($"xxHash64:");
            Console.WriteLine($"h64_1 = 0x{h64_1:X16}");
            Console.WriteLine($"h64_2 = 0x{h64_2:X16}");
            Console.WriteLine($"h64_3 = 0x{h64_3:X16}");
            Console.WriteLine($"h64_4 = 0x{h64_4:X16}");
            Console.WriteLine($"h64_5 = 0x{h64_5:X16}");
            Console.WriteLine($"h64_6 = 0x{h64_6:X16}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds} ms\n");
            // Give nanoseconds
            Console.WriteLine($"Elapsed time in nanoseconds: {sw.Elapsed.TotalMilliseconds * 1000000} ns");
            Console.WriteLine();

            sw.Restart();
            uint h32_1 = xxHash32.ComputeHash(data, data.Length);
            uint h32_2 = xxHash32.ComputeHash(new Span<byte>(data), data.Length);
            uint h32_3 = xxHash32.ComputeHash(new ReadOnlySpan<byte>(data), data.Length);
            uint h32_4 = xxHash32.ComputeHash(new MemoryStream(data));
            uint h32_5 = await xxHash32.ComputeHashAsync(new MemoryStream(data));
            uint h32_6 = xxHash32.ComputeHash("veni vidi vici");
            sw.Stop();
            Console.WriteLine($"xxHash32:");
            Console.WriteLine($"h32_1 = 0x{h32_1:X8}");
            Console.WriteLine($"h32_2 = 0x{h32_2:X8}");
            Console.WriteLine($"h32_3 = 0x{h32_3:X8}");
            Console.WriteLine($"h32_4 = 0x{h32_4:X8}");
            Console.WriteLine($"h32_5 = 0x{h32_5:X8}");
            Console.WriteLine($"h32_6 = 0x{h32_6:X8}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds} ms\n");
            Console.WriteLine($"Elapsed time in nanoseconds: {sw.Elapsed.TotalNanoseconds} ns");
            Console.WriteLine();

            sw.Restart();
            ulong h3_1 = xxHash3.ComputeHash(data, data.Length);
            ulong h3_2 = xxHash3.ComputeHash(new Span<byte>(data), data.Length);
            ulong h3_3 = xxHash3.ComputeHash(new ReadOnlySpan<byte>(data), data.Length);
            ulong h3_4 = xxHash3.ComputeHash("veni vidi vici");
            sw.Stop();
            Console.WriteLine($"xxHash3 (64-bit):");
            Console.WriteLine($"h3_1 = 0x{h3_1:X16}");
            Console.WriteLine($"h3_2 = 0x{h3_2:X16}");
            Console.WriteLine($"h3_3 = 0x{h3_3:X16}");
            Console.WriteLine($"h3_4 = 0x{h3_4:X16}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds} ms\n");
            Console.WriteLine($"Elapsed time in nanoseconds: {sw.Elapsed.TotalNanoseconds} ns");
            Console.WriteLine();

            sw.Restart();
            uint128 h128_1 = xxHash128.ComputeHash(data, data.Length);
            uint128 h128_2 = xxHash128.ComputeHash(new Span<byte>(data), data.Length);
            uint128 h128_3 = xxHash128.ComputeHash(new ReadOnlySpan<byte>(data), data.Length);
            uint128 h128_4 = xxHash128.ComputeHash("veni vidi vici");
            sw.Stop();
            Console.WriteLine($"xxHash128:");
            Console.WriteLine($"h128_1 = {h128_1}");
            Console.WriteLine($"h128_2 = {h128_2}");
            Console.WriteLine($"h128_3 = {h128_3}");
            Console.WriteLine($"h128_4 = {h128_4}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds} ms\n");
            Console.WriteLine($"Elapsed time in nanoseconds: {sw.Elapsed.TotalNanoseconds} ns");
            Console.WriteLine();

            sw.Restart();
            Guid guid = h128_1.ToGuid();
            byte[] bytes = h128_1.ToBytes();
            sw.Stop();
            Console.WriteLine($"As Guid: {guid}");
            Console.WriteLine($"As Bytes: {BitConverter.ToString(bytes)}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds} ms\n");
            Console.WriteLine($"Elapsed time in nanoseconds: {sw.Elapsed.TotalNanoseconds} ns");
            Console.WriteLine();

            sw.Restart();
            byte[] hash_bytes_1 = xxHash128.ComputeHashBytes(data, data.Length);
            byte[] hash_bytes_2 = xxHash128.ComputeHashBytes(new Span<byte>(data), data.Length);
            byte[] hash_bytes_3 = xxHash128.ComputeHashBytes(new ReadOnlySpan<byte>(data), data.Length);
            byte[] hash_bytes_4 = xxHash128.ComputeHashBytes("veni vidi vici");
            sw.Stop();
            Console.WriteLine($"HashBytes:");
            Console.WriteLine($"hash_bytes_1 = {BitConverter.ToString(hash_bytes_1)}");
            Console.WriteLine($"hash_bytes_2 = {BitConverter.ToString(hash_bytes_2)}");
            Console.WriteLine($"hash_bytes_3 = {BitConverter.ToString(hash_bytes_3)}");
            Console.WriteLine($"hash_bytes_4 = {BitConverter.ToString(hash_bytes_4)}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds} ms\n");
            Console.WriteLine($"Elapsed time in nanoseconds: {sw.Elapsed.TotalNanoseconds} ns");
            Console.WriteLine();
        }

        static void Blake2Test()
        {
            Console.WriteLine("BLAKE2Fast test");
            Stopwatch sw = new Stopwatch();
            byte[] data = Encoding.UTF8.GetBytes("veni vidi vici");
            sw.Restart();
            byte[] blake2b_1 = Blake2b.ComputeHash(data);
            byte[] blake2b_2 = Blake2b.ComputeHash(new Span<byte>(data));
            byte[] blake2b_3 = Blake2b.ComputeHash(new ReadOnlySpan<byte>(data));
            byte[] blake2b_4 = Blake2b.ComputeHash(Encoding.ASCII.GetBytes("veni vidi vici"));
            sw.Stop();
            Console.WriteLine($"Blake2B:");
            Console.WriteLine($"blake2b_1 = {BitConverter.ToString(blake2b_1)}");
            Console.WriteLine($"blake2b_2 = {BitConverter.ToString(blake2b_2)}");
            Console.WriteLine($"blake2b_3 = {BitConverter.ToString(blake2b_3)}");
            Console.WriteLine($"blake2b_4 = {BitConverter.ToString(blake2b_4)}");
            Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalMilliseconds} ms\n");
            Console.WriteLine($"Elapsed time in nanoseconds: {sw.Elapsed.TotalNanoseconds} ns");
        }
    }
}
