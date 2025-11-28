using FAT00800Model.VMs;

namespace FAT00800Front
{
    /// <summary>
    /// Parameter class for FAT00800AssetInformation tab page
    /// </summary>
    public class FAT00800AssetInformationParam
    {
        /// <summary>
        /// Asset Information ViewModel containing asset information and allocation data
        /// </summary>
        public FAT00800AssetInfoViewModel ViewModel { get; set; } = new();
    }
}
