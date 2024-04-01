using System.IO;
using AddressablesTools.Catalog;
using AddressablesTools.Binary;

namespace AddressablesTools
{
    public static class AddressablesBinaryParser
    {
        public static ContentCatalogData FromStream(Stream stream)
        {
            using BinaryReader binaryReader = new(stream);
            ContentCatalogDataBinary ccdBinary = new ContentCatalogDataBinary();
            ccdBinary.ReadBinary(binaryReader);
            stream.Dispose();

            return ccdBinary;
        }
    }
}