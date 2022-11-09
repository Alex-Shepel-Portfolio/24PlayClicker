using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEditor.Presets;
using UnityEngine;

namespace TFPlay.AssetTools
{
    public class EnforcePresetPostProcessor : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            if (!assetImporter.importSettingsMissing)
                return;

            if (assetPath.StartsWith("Assets/") && !assetPath.EndsWith(".cs") && !assetPath.EndsWith(".preset"))
            {
                var path = Path.GetDirectoryName(assetPath);
                ApplyPresetsFromFolderRecursively(path);
            }
        }

        private void ApplyPresetsFromFolderRecursively(string folder)
        {
            var parentFolder = Path.GetDirectoryName(folder);
            if (!string.IsNullOrEmpty(parentFolder))
                ApplyPresetsFromFolderRecursively(parentFolder);
            context.DependsOnCustomDependency($"PresetPostProcessor_{folder}");
            var presetPaths = Directory.EnumerateFiles(folder, "*.preset", SearchOption.TopDirectoryOnly).OrderBy(a => a);
            foreach (var presetPath in presetPaths)
            {
                var preset = AssetDatabase.LoadAssetAtPath<Preset>(presetPath);
                if (preset == null || preset.ApplyTo(assetImporter))
                {
                    context.DependsOnArtifact(presetPath);
                }
            }
        }

        public class UpdateFolderPresetDependency : AssetsModifiedProcessor
        {
            protected override void OnAssetsModified(string[] changedAssets, string[] addedAssets, string[] deletedAssets, AssetMoveInfo[] movedAssets)
            {
                HashSet<string> folders = new HashSet<string>();
                foreach (var asset in changedAssets)
                {
                    if (asset.EndsWith(".preset"))
                    {
                        folders.Add(Path.GetDirectoryName(asset));
                    }
                }
                foreach (var asset in addedAssets)
                {
                    if (asset.EndsWith(".preset"))
                    {
                        folders.Add(Path.GetDirectoryName(asset));
                    }
                }
                foreach (var asset in deletedAssets)
                {
                    if (asset.EndsWith(".preset"))
                    {
                        folders.Add(Path.GetDirectoryName(asset));
                    }
                }
                foreach (var movedAsset in movedAssets)
                {
                    if (movedAsset.destinationAssetPath.EndsWith(".preset"))
                    {
                        folders.Add(Path.GetDirectoryName(movedAsset.destinationAssetPath));
                    }
                    if (movedAsset.sourceAssetPath.EndsWith(".preset"))
                    {
                        folders.Add(Path.GetDirectoryName(movedAsset.sourceAssetPath));
                    }
                }
                if (folders.Count != 0)
                {
                    EditorApplication.delayCall += () =>
                    {
                        DelayedDependencyRegistration(folders);
                    };
                }
            }

            static void DelayedDependencyRegistration(HashSet<string> folders)
            {
                foreach (var folder in folders)
                {
                    var presetPaths = AssetDatabase.FindAssets("glob:\"**.preset\"", new[] { folder })
                            .Select(AssetDatabase.GUIDToAssetPath)
                            .Where(presetPath => Path.GetDirectoryName(presetPath) == folder)
                            .OrderBy(a => a);
                    Hash128 hash = new Hash128();
                    foreach (var presetPath in presetPaths)
                    {
                        hash.Append(presetPath);
                        hash.Append(AssetDatabase.LoadAssetAtPath<Preset>(presetPath).GetTargetFullTypeName());
                    }
                    AssetDatabase.RegisterCustomDependency($"PresetPostProcessor_{folder}", hash);
                }
                AssetDatabase.Refresh();
            }
        }
    }
}