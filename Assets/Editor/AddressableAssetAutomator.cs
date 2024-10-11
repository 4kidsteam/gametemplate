using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class AddressableAssetAutomator
{
    [MenuItem("Tools/Addressable Automator/Mark All Assets as Addressable")]
    public static void MarkTexturesAndAudioAsAddressable()
    {
        // Lấy các asset trong project (ngoại trừ Editor folder)
        string[] assetPaths = AssetDatabase.GetAllAssetPaths();
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        foreach (string assetPath in assetPaths)
        {
            if (!assetPath.StartsWith("Assets/Editor")) // Không đánh dấu asset trong Editor folder
            {
                // Lấy loại của asset
                Object asset = AssetDatabase.LoadMainAssetAtPath(assetPath);

                // Kiểm tra nếu asset là Texture hoặc AudioClip
                if (asset is Texture || asset is AudioClip)
                {
                    var guid = AssetDatabase.AssetPathToGUID(assetPath);
                    var entry = settings.FindAssetEntry(guid);
                    if (entry == null)
                    {
                        // Thêm asset vào Addressable nếu chưa có
                        settings.CreateOrMoveEntry(guid, settings.DefaultGroup);
                        Debug.Log($"Marked as Addressable: {assetPath}");
                    }
                }
            }
        }

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/Addressable Automator/Unmark All Assets as Addressable")]
    public static void UnmarkAllAssetsAsAddressable()
    {
        // Lấy setting của Addressable
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        // Lấy tất cả các entry hiện tại đã được đánh dấu là Addressable
        var entries = settings.groups
            .SelectMany(group => group.entries)
            .ToList();

        // Duyệt qua các entry và xóa chúng khỏi Addressable
        foreach (var entry in entries)
        {
            settings.RemoveAssetEntry(entry.guid);
            Debug.Log($"Unmarked Addressable: {AssetDatabase.GUIDToAssetPath(entry.guid)}");
        }

        // Lưu thay đổi
        AssetDatabase.SaveAssets();
    }
}
