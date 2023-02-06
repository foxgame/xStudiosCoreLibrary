using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace X
{
    [CreateAssetMenu()]
    public class AssetSetting : ScriptableObject
    {
        [System.Serializable]
        public class SharedAsset
        {
            public string Path;
            
            public List<string> AssetPathList = new List<string>();
            public List<string> AssetFileList = new List<string>();
        }

        // Assets/Mods/X/Common/Config/
        // D:\Work\OpenCubeWorldCore\Server\WorldServer\bin\Debug\netcoreapp3.1\Config\
        // D:\Work\OpenCubeWorldCore\Server\WorldServer\bin\Release\netcoreapp3.1\Config\

        public List<SharedAsset> sharedAssetList = new List<SharedAsset>();

        public List<string> configPathList = new List<string>();
        public List<string> configCopyToList = new List<string>();
    }

}

