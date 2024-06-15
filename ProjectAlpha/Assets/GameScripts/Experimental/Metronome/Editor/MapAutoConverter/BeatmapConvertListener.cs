using System.IO;
using UnityEditor;
using UnityEngine;

namespace ProjectAlpha
{
    public class BeatmapConvertListener : AssetPostprocessor
    {
        private const string TargetFolder = "Assets/AssetRaw/BeatmapRaw";

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            bool isTargetFolderChanged = false;

            foreach (string str in importedAssets)
            {
                if (str.StartsWith(TargetFolder))
                {
                    Debug.Log("Reimported Asset: " + str);
                    HandleReimportedAsset(str);
                    isTargetFolderChanged = true;
                }
            }

            foreach (string str in deletedAssets)
            {
                if (str.StartsWith(TargetFolder))
                {
                    Debug.Log("Deleted Asset: " + str);
                    HandleDeletedAsset(str);
                    isTargetFolderChanged = true;
                }
            }

            for (int i = 0; i < movedAssets.Length; i++)
            {
                if (movedAssets[i].StartsWith(TargetFolder) || movedFromAssetPaths[i].StartsWith(TargetFolder))
                {
                    Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
                    HandleMovedAsset(movedAssets[i], movedFromAssetPaths[i]);
                    isTargetFolderChanged = true;
                }
            }

            if (isTargetFolderChanged)
            {
                OnTargetFolderChanged();
            }
        }

        private static void HandleReimportedAsset(string assetPath)
        {
            if (AssetDatabase.IsValidFolder(assetPath))
            {
                Debug.Log($"文件夹路径: {assetPath}");
                // 获取这个文件夹下的所有 .osu 和 .txt 的文件路径
                string[] guids = AssetDatabase.FindAssets("t:TextAsset", new[] { assetPath });
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    if (path.EndsWith(".osu") || path.EndsWith(".txt"))
                    {
                        string fileContent = File.ReadAllText(path);
                        var beatmap = ConvertBeatmap(fileContent, assetPath);
                        if (beatmap != null)
                        {
                            SaveMetronomeMap(beatmap, path);
                        }
                    }
                }
            }
        }

        private static void HandleDeletedAsset(string assetPath)
        {
            if (AssetDatabase.IsValidFolder(assetPath))
            {
                Debug.Log($"文件夹路径: {assetPath}");
            }
        }

        private static void HandleMovedAsset(string newAssetPath, string oldAssetPath)
        {
        }

        private static void OnTargetFolderChanged()
        {
            // 在这里处理文件夹内容变化后的逻辑
            Debug.Log("Target folder content changed!");
            // 可以在这里触发其他逻辑，例如更新资源引用或重新生成某些数据等
        }

        private static void SaveMetronomeMap(MetronomeMap beatmap, string originalPath)
        {
            string directoryPath = "Assets/AssetRaw/Beatmaps";
            if (!AssetDatabase.IsValidFolder(directoryPath))
            {
                AssetDatabase.CreateFolder("Assets/AssetRaw", "Beatmaps");
            }

            string fileName = Path.GetFileNameWithoutExtension(originalPath);
            string assetPath = $"{directoryPath}/{fileName}_MetronomeMap.asset";

            AssetDatabase.CreateAsset(beatmap, assetPath);
            AssetDatabase.SaveAssets();
        }

        private static MetronomeMap ConvertBeatmap(string content, string assetPath)
        {
            if (assetPath.Contains("Osu"))
            {
                return MetronomeMapConverter.ConvertOsu(content);
            }
            else if (assetPath.Contains("CryptOfTheNecroDancer"))
            {
                return MetronomeMapConverter.ConvertCryptOfTheNecroDancer(content);
            }
            else
            {
                Debug.LogError($"Unknown folder type for asset path: {assetPath}");
                return null;
            }
        }
    }
}