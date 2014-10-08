using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleWindsorTest
{
    public interface IFileBuffer
    {
        bool Exists();
        string Checksum();
    }

    public class FileBuffer: IFileBuffer
    {
        private IChecksumProvider checksumProvider;
        public FileBuffer(IChecksumProvider checksum)
        {
            checksumProvider = checksum;
        }

        public bool Exists()
        {
            return true;
        }

        public string Checksum()
        {
            return checksumProvider.GenerateChecksum();
        }
    }
}
