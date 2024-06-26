﻿using System.IO;
using NitroxModel.Helper;
using NitroxServer_BelowZero.Resources.Parsers;

namespace NitroxServer_BelowZero.Resources;

public static class ResourceAssetsParser
{
    private static ResourceAssets resourceAssets;

    public static ResourceAssets Parse()
    {
        if (resourceAssets != null)
        {
            return resourceAssets;
        }

        using (PrefabPlaceholderGroupsParser prefabPlaceholderGroupsParser = new())
        {
            resourceAssets = new ResourceAssets
            {
                WorldEntitiesByClassId = new WorldEntityInfoParser().ParseFile(),
                LootDistributionsJson = new EntityDistributionsParser().ParseFile(),
                PrefabPlaceholdersGroupsByGroupClassId = prefabPlaceholderGroupsParser.ParseFile(),
                NitroxRandom = new RandomStartParser().ParseFile()
            };
        }
        AssetParser.Dispose();
        
        ResourceAssets.ValidateMembers(resourceAssets);
        return resourceAssets;
    }

    public static string FindDirectoryContainingResourceAssets()
    {
        string subnauticaPath = NitroxUser.GamePath_BZ;
        if (string.IsNullOrEmpty(subnauticaPath))
        {
            throw new DirectoryNotFoundException("Could not locate Subnautica: Below Zero installation directory for resource parsing.");
        }

        if (File.Exists(Path.Combine(subnauticaPath, "SubnauticaZero_Data", "resources.assets")))
        {
            return Path.Combine(subnauticaPath, "SubnauticaZero_Data");
        }
        if (File.Exists(Path.Combine("..", "resources.assets"))) //  SubServer => Subnautica/SubnauticaZero_Data/SubServer
        {
            return Path.GetFullPath(Path.Combine(".."));
        }
        if (File.Exists(Path.Combine("..", "SubnauticaZero_Data", "resources.assets"))) //  SubServer => Subnautica/SubServer
        {
            return Path.GetFullPath(Path.Combine("..", "SubnauticaZero_Data"));
        }
        if (File.Exists("resources.assets")) //  SubServer/* => Subnautica/SubnauticaZero_Data/
        {
            return Directory.GetCurrentDirectory();
        }
        throw new FileNotFoundException("Make sure resources.assets is in current or parent directory and readable.");
    }
}
