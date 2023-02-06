using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace X
{
    public class BuildAssetBundles
    {

        public static void ClearTemp()
        {
            string tempPath = "Temp/assets";

            if ( Directory.Exists( tempPath ) )
            {
                Directory.Delete( tempPath , true );
            }

            Directory.CreateDirectory( tempPath );
        }

        public static string GetSelectedPathOrFallback()
        {
            string path = "Assets";

            foreach ( UnityEngine.Object obj in Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.Assets ) )
            {
                path = AssetDatabase.GetAssetPath( obj );
                if ( !string.IsNullOrEmpty( path ) && File.Exists( path ) )
                {
                    path = Path.GetDirectoryName( path );
                    break;
                }
            }

            return path;
        }

        [MenuItem( "Assets/Build Assets Preview" )]
        static void BuildAssetsPreview()
        {
            UnityEngine.Object[] objects = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.Assets );

            for ( int i = 0 ; i < objects.Length ; i++ )
            {
                UnityEngine.Object obj = objects[ i ];
                string path = AssetDatabase.GetAssetPath( obj );
                string name = Path.GetDirectoryName(path) + "\\Icons\\" + Path.GetFileNameWithoutExtension(path) + ".png";

                Texture2D texture = AssetPreview.GetAssetPreview( obj );
                byte[] bytes = texture.EncodeToPNG();

                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllBytes( name , bytes );

//                 Texture2D.DestroyImmediate( texture );
            }

            AssetDatabase.Refresh();
        }

        [MenuItem( "Assets/Build Directory AssetBundles" )]
        static void BuildAssetDirectoryAssetBundle()
        {
            try
            {
                ClearTemp();
            }
            catch ( System.Exception )
            {
            }

            string path = "";
            UnityEngine.Object[] objects = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.Assets );

            if ( objects.Length == 0 )
            {
                return;
            }

            DirectoryInfo pathInfo = new DirectoryInfo( AssetDatabase.GetAssetPath( objects[ 0 ] ) );
            string[] dir = Path.GetDirectoryName( AssetDatabase.GetAssetPath( objects[ 0 ] ) ).Replace( "\\" , "/" ).Split( '/' );
            string name1 = dir[ dir.Length - 1 ];

            List<AssetBundleBuild> builds = GetSharesBuilds();
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = name1;
            build.assetBundleVariant = Utility.AssetExtension.Replace( "." , "" );
            build.assetNames = new string[ objects.Length ];
            build.addressableNames = new string[ objects.Length ];

            for ( int i = 0 ; i < objects.Length ; i++ )
            {
                UnityEngine.Object obj = objects[ i ];

                path = AssetDatabase.GetAssetPath( obj );

                if ( !string.IsNullOrEmpty( path ) && File.Exists( path ) )
                {
                    string name = Path.GetFileNameWithoutExtension( path );

                    build.assetNames[ i ] = path;
                    build.addressableNames[ i ] = name;
                }
            }

            builds.Add( build );

            BuildPipeline.BuildAssetBundles( "Temp" , builds.ToArray() ,
                BuildAssetBundleOptions.None , EditorUserBuildSettings.activeBuildTarget );

            string filePath = Path.GetDirectoryName( path ) + "/" + name1 + Utility.AssetExtension;

            if ( File.Exists( filePath ) )
            {
                File.Delete( filePath );
            }

            FileInfo fi = new FileInfo( "Temp/" + name1 + Utility.AssetExtension );
            fi.MoveTo( filePath );

            AssetDatabase.Refresh();
        }

        static List<AssetBundleBuild> GetSharesBuilds()
        {
            AssetSetting assetSetting = AssetTools.LoadSetting();


            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

            for ( int i = 0 ; i < assetSetting.sharedAssetList.Count ; i++ )
            {
                List<string> pathList = new List<string>();

                AssetSetting.SharedAsset sharedAsset = assetSetting.sharedAssetList[ i ];

                for ( int j = 0 ; j < sharedAsset.AssetPathList.Count ; j++ )
                {

                }
                for ( int j = 0 ; j < sharedAsset.AssetFileList.Count ; j++ )
                {
                    pathList.Add( sharedAsset.AssetFileList[ j ] );
                }

                AssetBundleBuild build = new AssetBundleBuild();

                string name = Path.GetFileNameWithoutExtension( sharedAsset.Path );
                build.assetBundleName = Path.GetDirectoryName( sharedAsset.Path ).Replace( "\\" , "/" ) + "/" + name;
                build.assetBundleVariant = Utility.AssetExtension.Replace( "." , "" );
                build.assetNames = new string[ pathList.Count ];
                build.addressableNames = new string[ pathList.Count ];

                for ( int j = 0 ; j < pathList.Count ; j++ )
                {
                    string path = pathList[ j ];

                    if ( !string.IsNullOrEmpty( path ) && File.Exists( path ) )
                    {
                        name = Path.GetFileNameWithoutExtension( path );

                        build.assetNames[ j ] = path;
                        build.addressableNames[ j ] = name;
                    }
                }

                builds.Add( build );
            }

            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes0.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes0Mask.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes0Normal.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes1.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes1Mask.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes1Normal.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes2.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes2Mask.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Cubes2Normal.tga" );
            // 
            //             pathList.Add( "Assets/Objects/Textures/Maps/EdgeMask.tga" );
            //             pathList.Add( "Assets/Objects/Textures/Maps/Ridge.tga" );

            Resources.UnloadAsset( assetSetting );

            return builds;
        }

        [MenuItem( "Assets/Build Single AssetBundles" )]
        static void BuildSingleAssetBundles()
        {
            try
            {
                ClearTemp();
            }
            catch ( System.Exception )
            {
            }

            string path = "Assets";
            UnityEngine.Object[] objects = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.Assets );


            for ( int i = 0 ; i < objects.Length ; i++ )
            {
                UnityEngine.Object obj = objects[ i ];

                path = AssetDatabase.GetAssetPath( obj ).Replace( "\\" , "/" );

                if ( !path.Contains( "." ) )
                {
                    continue;
                }

                if ( !string.IsNullOrEmpty( path ) && File.Exists( path ) )
                {
                    List<AssetBundleBuild> builds = GetSharesBuilds();
                    AssetBundleBuild build = new AssetBundleBuild();

                    string name = Path.GetFileNameWithoutExtension( path );

                    build.assetBundleName = Path.GetDirectoryName( path ).Replace( "\\" , "/" ) + "/" + name;
                    build.assetBundleVariant = Utility.AssetExtension.Replace( "." , "" );
                    build.assetNames = new string[] { path };
                    build.addressableNames = new string[] { name };

                    builds.Add( build );

                    BuildPipeline.BuildAssetBundles( "Temp" , builds.ToArray() ,
                        BuildAssetBundleOptions.None , EditorUserBuildSettings.activeBuildTarget );

                    for ( int j = 0 ; j < builds.Count ; j++ )
                    {
                        try
                        {
                            AssetBundleBuild bundleBuild = builds[ j ];

                            name = Path.GetFileNameWithoutExtension( bundleBuild.assetBundleName );
                            string filePath = Path.GetDirectoryName( bundleBuild.assetBundleName ) + "\\" + name + Utility.AssetExtension;

                            if ( File.Exists( "Temp/" + bundleBuild.assetBundleName + Utility.AssetExtension ) )
                            {
                                if ( File.Exists( filePath ) )
                                {
                                    File.Delete( filePath );
                                }

                                FileInfo fi = new FileInfo( "Temp/" + bundleBuild.assetBundleName + Utility.AssetExtension );
                                fi.MoveTo( filePath );
                            }
                        }
                        catch ( System.Exception )
                        {
                        }
                    }

                }
            }


            AssetDatabase.Refresh();
        }

    }

}


