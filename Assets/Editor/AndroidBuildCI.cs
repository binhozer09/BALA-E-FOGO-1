// Trigger initial Android CI build.\nusing System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BalaEFogo.Editor
{
    public static class AndroidBuildCI
    {
        public static void Build()
        {
            var scenes = EditorBuildSettings.scenes;
            if (scenes == null || scenes.Length == 0)
                throw new System.Exception("BALA_E_FOGO_BUILD_NO_SCENES: nenhuma cena está habilitada em Build Settings.");

            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Build", "Android");
            Directory.CreateDirectory(outputDirectory);
            var apkPath = Path.Combine(outputDirectory, "BALA-E-FOGO-1.apk");

            var options = new BuildPlayerOptions
            {
                scenes = System.Array.ConvertAll(scenes, scene => scene.path),
                locationPathName = apkPath,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result != BuildResult.Succeeded)
                throw new System.Exception("BALA_E_FOGO_BUILD_FAILED: " + report.summary.result);

            Debug.Log("BALA_E_FOGO_BUILD_OK: " + apkPath + " (" + report.summary.totalSize + " bytes)");
        }
    }
}
