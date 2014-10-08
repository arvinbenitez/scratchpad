using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleWindsorTest
{
    public interface IChecksumProvider
    {
        string GenerateChecksum();
    }

    public class ChecksumProvider : IChecksumProvider
    {

        public string GenerateChecksum()
        {
            return "Test Checksum";
        }
    }
}
