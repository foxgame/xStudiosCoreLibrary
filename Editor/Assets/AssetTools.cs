using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace X
{
    public class AssetTools
    {

        public static AssetSetting LoadSetting()
        {
            AssetSetting assetSetting = AssetDatabase.LoadAssetAtPath<AssetSetting>( "Assets/xStudios/Assets/AssetSetting.asset" );
            return assetSetting;
        }

        [MenuItem( "xStudios/Assets/Setting" )]
        public static void Setting()
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath( "Assets/xStudios/Assets/AssetSetting.asset" );
        }

    }

}
