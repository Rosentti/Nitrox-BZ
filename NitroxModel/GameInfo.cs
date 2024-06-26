namespace NitroxModel
{
    public sealed class GameInfo
    {
        public static readonly GameInfo Subnautica = new()
        {
            Name = "Subnautica",
            FullName = "Subnautica",
            DataFolder = "Subnautica_Data",
            ExeName = "Subnautica.exe",
            SteamAppId = 264710,
            MsStoreStartUrl = @"ms-xbl-38616e6e:\\",
            MapSubFolderName = "Build18"
        };

        public static readonly GameInfo SubnauticaBelowZero = new()
        {
            Name = "SubnauticaZero",
            FullName = "Subnautica: Below Zero",
            DataFolder = "SubnauticaZero_Data",
            ExeName = "SubnauticaZero.exe",
            SteamAppId = 848450,
            MsStoreStartUrl = @"ms-xbl-6e27970f:\\",
            MapSubFolderName = "Expansion"
        };

        public string Name { get; private set; }

        public string FullName { get; private set; }

        public string DataFolder { get; private set; }

        public string ExeName { get; private set; }

        public int SteamAppId { get; private set; }

        public string MsStoreStartUrl { get; private set; }

        public string MapSubFolderName { get; private set; }

        private GameInfo()
        {
        }
    }
}
