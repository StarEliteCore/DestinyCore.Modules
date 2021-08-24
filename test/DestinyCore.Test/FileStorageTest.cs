using DestinyCore.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DestinyCore.Test
{
    public class FileStorageTest
    {
   

        protected const int DataLength = 2048;
        protected readonly byte[] DummyData = GenerateBytes(DataLength);

        [Fact]
        public async Task WriteAsync_Test()
        {
            using FileStorage storage = new FileStorage("");

            await storage.WriteAsync(DummyData, 0, DataLength);

            var length = storage.GetLength();

            Assert.Equal(DataLength, length);
            storage.Clear();
        }

        public static  byte[] GenerateBytes(int length)
        {
            if (length < 1)
                throw new ArgumentException("长度必须是 > 0");

            Random rand = new Random();
            byte[] buffer = new byte[length];
            rand.NextBytes(buffer);
            return buffer;
        }
    }
}
