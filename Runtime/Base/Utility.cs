﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.Video;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Checksum;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_STANDALONE_WIN

[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Auto )]
public class OpenDialogFile
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

[StructLayout( LayoutKind.Sequential , CharSet = CharSet.Auto )]
public class OpenDialogDir
{
    public IntPtr hwndOwner = IntPtr.Zero;
    public IntPtr pidlRoot = IntPtr.Zero;
    public String pszDisplayName = null;
    public String lpszTitle = null;
    public UInt32 ulFlags = 0;
    public IntPtr lpfn = IntPtr.Zero;
    public IntPtr lParam = IntPtr.Zero;
    public int iImage = 0;
}

#endif

namespace X
{
    public partial class Utility
    {
        public const int InvalidID = -1;

        public const string MetaExtension = ".meta";
        public const string AssetExtension = ".unity3d";

        public static string CurrentDirectory = "";

        public const string X = "X";

        public const string Name = "Name";
        public const string Des = "Des";
        public const string Move = "Move";

        public const string XPath = "X/";

        public const string IconsPath = "Icons/";
        public const string PrefabsPath = "Prefabs/";
        public const string ObjectsPath = "Objects/";
        public const string PlayersPath = "Players/";
        public const string PartsPath = "Parts/";
        public const string LogicPath = "Logic/";
        public const string ViewPath = "View/";
        public const string ModelsPath = "Models/";
        public const string MapPath = "Map/";
        public const string SkyIconPath = "SkyIcons/";
        public const string SkyBoxPath = "SkyBoxes/";

        public const string BinPath = "Bin/";

#if UNITY_EDITOR
        public const string PngExtension = ".png";
#else
        public const string PngExtension = ".unity3d";
#endif

#if UNITY_EDITOR
        public const string JpgExtension = ".jpg";
#else
        public const string PngExtension = ".unity3d";
#endif

#if UNITY_EDITOR
        public const string PrefabExtension = ".prefab";
#else
        public const string PrefabExtension = ".unity3d";
#endif

#if UNITY_EDITOR
        public const string ConfigExtension = ".asset";
#else
        public const string ConfigExtension = ".unity3d";
#endif

#if UNITY_EDITOR
        public const string AnimationExtension = ".anim";
#else
        public const string AnimationExtension = ".unity3d";
#endif

#if UNITY_EDITOR
        public static readonly string[] SkyBoxExtension = { ".hdr", ".exr", ".jpeg" };
#else
        public static readonly string[] SkyBoxExtension = { ".unity3d" };
#endif


#if UNITY_EDITOR
        public static string AssetsPath = "Assets/";
#else
        public static string AssetsPath = "./";
#endif



        public static string MapsPath = "Maps/";
        public static string VersionsPath = "Versions/";

        public static string PlayerDataPath = "PlayerData/";
        public static string PlayerDataExtension = ".dat";

        public const string GameMapPath = "Game/";

        public const string TempPath = "Temp/";
        public const string TemplatesPath = "Templates/";

        public const string MapExtension = ".map";
        public const string MapDataFile = "Map.dat";

        public const string UsersPath = "Users/";

        public const string ModsPath = "Mods/";
        public const string CommonPath = "Common/";
        public const string SharesPath = "Shares/";
        public const string GamesPath = "Games/";
        public const string ConfigPath = "Config/";
        public const string TexturesPath = "Textures/";
        public const string AnimationsPath = "Animations/";
        public const string DataPath = "Data/";
        public const string ScriptsPath = "Scripts/";
        public const string BattlePath = "Battle/";
        public const string SkillPath = "Skill/";
        public const string AudioPath = "Audio/";
        public const string VideoPath = "Video/";
        public const string UIPath = "UI/";
        public const string UIAtlasPath = "UIAtlas/";
        public const string MaterialsPath = "Materials/";
        public const string ScenesPath = "Scenes/";
        public const string HumanoidModelsPath = "HumanoidModels/";
        public const string ActionsPath = "Actions/";
        public const string AnimatorPath = "Animator/";
        public const string UtilityPath = "Utility/";
        public const string WaterPath = "Water/";

        public const string InspectorsPath = "Inspectors/";
        public const string MapEditorPath = "MapEditor/";
        public const string ModelViewerPath = "ModelViewer/";
        public const string GamePath = "Game/";
        public const string GameServerPath = "GameServer/";

        public const string DressingRoomPath = "DressingRoom/";

        public const string PlayerPath = "Player/";
        public const string ThirdPersonPath = "ThirdPerson/";

        public const string LobbyPath = "Lobby/";


        static Dictionary<string , string> hashDictionary = new Dictionary<string , string>();

        public static long GetTime()
        {
            return ( DateTime.Now.ToUniversalTime().Ticks - 621355968000000000 );
        }

        public static void ClearMapTemp()
        {
            string tempPath = MapsPath + TempPath;

            if ( Directory.Exists( tempPath ) )
            {
                Directory.Delete( tempPath , true );
            }

            Directory.CreateDirectory( tempPath );
        }

        public static bool IsNullOrEmpty( string str )
        {
            return string.IsNullOrEmpty( str );
        }

        public static int EnumToInt( object o )
        {
            return (int)o;
        }

        public static string FloatToString( float f , string format )
        {
            return f.ToString( format );
        }

        public static string IntToString( float n )
        {
            return n.ToString();
        }

        public static int StringToInt( string obj )
        {
            if ( string.IsNullOrEmpty( obj ) )
            {
                return 0;
            }

            return int.Parse( obj.Split( '.' )[ 0 ] );
        }

        public static float Round2( float x )
        {
            return (float)Math.Round( x , 2 , MidpointRounding.AwayFromZero );
        }

        public static float GetDistance( int x1 , int y1 , int x2 , int y2 )
        {
            int dx = x1 - x2;
            int dy = y1 - y2;

            return Mathf.Sqrt( dx * dx + dy * dy );
        }

        public static uint SDBMHash( byte[] bytes )
        {
            uint hash = 0;

            for ( int i = 0 ; i < bytes.Length ; i++ )
            {
                hash = ( bytes[ i ] ) + ( hash << 6 ) + ( hash << 16 ) - hash;
            }

            return ( hash & 0x7FFFFFFF );
        }

        public static uint SDBMHash( string str )
        {
            if ( string.IsNullOrEmpty( str ) )
            {
                return 0;
            }

            uint hash = 0;

            byte[ ] bytes = Encoding.UTF8.GetBytes( str );

            for ( int i = 0 ; i < bytes.Length ; i++ )
            {
                hash = ( bytes[ i ] ) + ( hash << 6 ) + ( hash << 16 ) - hash;
            }

            return ( hash & 0x7FFFFFFF );
        }


        public static ulong SDBMHashLong( byte[] bytes )
        {
            ulong hash = 0;

            for ( int i = 0 ; i < bytes.Length ; i++ )
            {
                hash = ( bytes[ i ] ) + ( hash << 6 ) + ( hash << 16 ) - hash;
            }

            return ( hash & 0x7FFFFFFFFFFFFFFF );
        }

        public static float PointToAngle( Vector3 p1 , Vector3 p2 )
        {
            Vector2 p;
            p.x = p2.x - p1.x;
            p.y = p2.y - p1.y;

            return Mathf.Atan2(p.y, p.x) * 180 / Mathf.PI;
        }

        public static float Angle180To360( float angle )
        {
            if ( angle >= 0 && angle <= 180 )
                return angle;
            else
                return 360 + angle;
        }

        public static string GetFullName( GameObject obj )
        {
            string str = obj.name;

            Transform trans = obj.transform.parent;

            while ( trans )
            {
                str = trans.name + "/" + str;
                trans = trans.parent;
            }

            return str;
        }

        public static bool ParseShort( string str , out short s )
        {
            s = 0;

            if ( str == null || str == "" )
                return false;

            return short.TryParse( str , out s );
        }
        public static bool ParseUShort( string str , out ushort s )
        {
            s = 0;

            if ( str == null || str == "" )
                return false;

            return ushort.TryParse( str , out s );
        }
        public static bool ParseByte( string str , out byte b )
        {
            b = 0;

            if ( str == null || str == "" )
                return false;

            uint n = 0;
            uint.TryParse( str , out n );

            if ( n > Byte.MaxValue )
                return false;

            b = (byte)n;

            return true;
        }
        public static bool ParseSByte( string str , out sbyte b )
        {
            b = 0;

            if ( str == null || str == "" )
                return false;

            int n = 0;
            int.TryParse( str , out n );

            if ( n > SByte.MaxValue || n < SByte.MinValue )
                return false;

            b = (sbyte)n;

            return true;
        }
        public static bool ParseInt( string str , out int n )
        {
            n = 0;

            if ( str == null || str == "" )
                return false;

            return int.TryParse( str , out n );
        }
        public static bool ParseUInt( string str , out uint n )
        {
            n = 0;

            if ( str == null || str == "" )
                return false;

            return uint.TryParse( str , out n );
        }
        public static bool ParseFloat( string str , out float f )
        {
            f = 0f;

            if ( str == null || str == "" )
                return false;

            return float.TryParse( str , out f );
        }
        public static bool ParseString( string str , out string n )
        {
            n = "";

            if ( str == null || str == "" )
                return false;

            n = str;

            return true;
        }

        public static string[] Split( string str , string split )
        {
            return Regex.Split( str , split );
        }

        public static void DestroyAllChild( Transform trans )
        {
            while ( trans.childCount > 0 )
            {
                GameObject obj = trans.GetChild( 0 ).gameObject;

                GameObject.Destroy( obj );
                obj.transform.SetParent( null );
            }
        }

        public static bool StringContains( string str , string f )
        {
            return str.Contains( f );
        }

        public static bool BoundsIsEncapsulated( Bounds Encapsulator , Bounds Encapsulating )
        {
            return Encapsulator.Contains( Encapsulating.min ) && Encapsulator.Contains( Encapsulating.max );
        }

        public static string[] GetMaps()
        {
            string[] files = Directory.GetFiles( MapsPath , "*" + MapExtension , SearchOption.AllDirectories );
            return files;
        }

        public static bool CheckHash( string path )
        {
            string hash = "";

            if ( !hashDictionary.TryGetValue( path.ToLower() , out hash ) )
            {
                return true;
            }

            byte[] bytes = File.ReadAllBytes( path );

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] encryptdata = md5.ComputeHash( bytes );
            string base64 = Convert.ToBase64String( encryptdata );

            return hash == base64;
        }

        public static void ReadHash( string path )
        {
            FileStream fs = null;
            BinaryReader br = null;

            try
            {
                File.ReadAllBytes( path );

                fs = File.Open( path , FileMode.Open , FileAccess.ReadWrite , FileShare.ReadWrite );
                br = new BinaryReader( fs );

                br.ReadString();
                br.ReadInt32();

                int count = br.ReadInt32();

                for ( int i = 0 ; i < count ; i++ )
                {
                    string file = br.ReadString();
                    string hash = br.ReadString();

                    if ( hashDictionary.ContainsKey( file ) )
                    {
                        hashDictionary[ file ] = hash;
                    }
                    else
                    {
                        hashDictionary.Add( file , hash );
                    }
                }
            }
            catch ( System.Exception e )
            {
                Debug.LogError( e.Message );
            }
            finally
            {
                if ( fs != null )
                {
                    fs.Close();
                    fs.Dispose();
                }
                if ( br != null )
                {
                    br.Close();
                    br.Dispose();
                }
            }
        }

        public static void BuildHash( string path , List<string> hashList )
        {
            FileStream fs = null;
            BinaryWriter bf = null;

            try
            {
                fs = File.Open( path , FileMode.Create , FileAccess.ReadWrite , FileShare.ReadWrite );
                bf = new BinaryWriter( fs );

                bf.Write( "XDAT" );
                bf.Write( 1 );

                bf.Write( hashList.Count );

                for ( int i = 0 ; i < hashList.Count ; i++ )
                {
                    string file = hashList[ i ];

                    byte[] bytes = File.ReadAllBytes( file );

                    MD5 md5 = new MD5CryptoServiceProvider();

                    byte[] encryptdata = md5.ComputeHash( bytes );
                    string base64 = System.Convert.ToBase64String( encryptdata );

                    bf.Write( file.Replace( "\\" , "/" ).Replace( Utility.AssetsPath , "" ).ToLower() );
                    bf.Write( base64 );
                }
            }
            catch ( System.Exception e )
            {
                Debug.LogError( e.Message );
            }
            finally
            {
                if ( fs != null )
                {
                    fs.Close();
                    fs.Dispose();
                }
                if ( bf != null )
                {
                    bf.Close();
                    bf.Dispose();
                }
            }
        }

        public static byte[] Compress( byte[] bytesToCompress , int offset , int length )
        {
            byte[] rebyte = null;
            MemoryStream ms = new MemoryStream();

            GZipOutputStream s = new GZipOutputStream( ms );

            try
            {
                s.Write( bytesToCompress , offset , length );
                s.Flush();
                s.Finish();
            }
            catch ( System.Exception ex )
            {
#if UNITY_EDITOR
                Debug.Log( ex );
#endif
            }

            ms.Seek( 0 , SeekOrigin.Begin );

            rebyte = ms.ToArray();

            s.Close();
            ms.Close();

            s.Dispose();
            ms.Dispose();

            return rebyte;
        }

        public static byte[] DeCompress( byte[] bytesToDeCompress )
        {
            byte[] rebyte = new byte[ bytesToDeCompress.Length * 20 ];

            MemoryStream ms = new MemoryStream( bytesToDeCompress );
            MemoryStream outStream = new MemoryStream();

            GZipInputStream s = new GZipInputStream( ms );

            int read = s.Read( rebyte , 0 , rebyte.Length );
            while ( read > 0 )
            {
                outStream.Write( rebyte , 0 , read );
                read = s.Read( rebyte , 0 , rebyte.Length );
            }

            byte[] rebyte1 = outStream.ToArray();

            ms.Close();
            s.Close();
            outStream.Close();

            ms.Dispose();
            s.Dispose();
            outStream.Dispose();

            bytesToDeCompress = null;
            rebyte = null;

            return rebyte1;
        }

        public static object LoadObjectFromBinary( string path )
        {
            if ( !File.Exists( path ) )
            {
                return null;
            }

            FileStream fs = null;

            try
            {
                fs = File.Open( path , FileMode.Open , FileAccess.ReadWrite , FileShare.ReadWrite );

                BinaryFormatter bf = new BinaryFormatter();

                object obj = bf.Deserialize( fs );

                return obj;
            }
            catch ( Exception e )
            {
                Debug.LogError( e.Message );
            }
            finally
            {
                if ( fs != null )
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

            return null;
        }

        public static void SaveObjectToBinary( string path , object data )
        {
            FileStream fs = null;

            try
            {
                fs = File.Open( path , FileMode.Create , FileAccess.ReadWrite , FileShare.ReadWrite );

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize( fs , data );
            }
            catch ( Exception e )
            {
                Debug.LogError( e.Message );
            }
            finally
            {
                if ( fs != null )
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }


        public delegate void UploadFileCallback( string path , string code , bool err );
        public static IEnumerator UploadFile( string url , string path , UploadFileCallback cb )
        {
            if ( !File.Exists( path ) )
            {
                cb( path , "" , true );
                yield break;
            }

            byte[ ] data = null;
            try
            {
                data = File.ReadAllBytes( path );
            }
            catch ( Exception )
            {
                cb( path , "" , true );
                yield break;
            }

            WWWForm form = new WWWForm();
            form.AddBinaryData( "file" , data , Path.GetFileName( path ) );

            UnityWebRequest web = UnityWebRequest.Post( url , form );
            web.uploadHandler.contentType = "multipart/form-data";
            web.SetRequestHeader( "token" , "unity3d" );

            yield return web.SendWebRequest();

            if ( web.isDone )
            {
                if ( web.error != null )
                {
                    cb( path , "" , true );
                }
                else
                {
                    cb( path , web.downloadHandler.text , false );
                    web.Dispose();
                }
            }
        }


#if UNITY_STANDALONE_WIN
        [DllImport( "Comdlg32.dll" , SetLastError = true , ThrowOnUnmappableChar = true , CharSet = CharSet.Auto )]
        public static extern bool GetOpenFileName( [In, Out] OpenDialogFile ofn );

        [DllImport( "shell32.dll" , SetLastError = true , ThrowOnUnmappableChar = true , CharSet = CharSet.Auto )]
        public static extern bool SHGetPathFromIDList( [In] IntPtr pidl , [In, Out] char[ ] fileName );

        [DllImport( "shell32.dll" , SetLastError = true , ThrowOnUnmappableChar = true , CharSet = CharSet.Auto )]
        public static extern IntPtr SHBrowseForFolder( [In, Out] OpenDialogDir ofn );

#endif

        public static string OpenFolderPanel( string title )
        {
#if UNITY_EDITOR
            return EditorUtility.OpenFolderPanel( title , CurrentDirectory , "" );
#elif UNITY_STANDALONE_WIN
        try
        {
            OpenDialogDir ofn2 = new OpenDialogDir();
            ofn2.pszDisplayName = new string( new char[ 1024 ] );
            ofn2.lpszTitle = title;

            IntPtr pidlPtr = SHBrowseForFolder( ofn2 );

            if ( pidlPtr != null )
            {
                char[] charArray = new char[ 1024 ];

                SHGetPathFromIDList( pidlPtr , charArray );
                string fullDirPath = new String( charArray );

                return fullDirPath.Substring( 0 , fullDirPath.IndexOf( '\0' ) );
            }
        }
        catch ( Exception )
        {
        }

        return "";
#else
        return "";
#endif
        }


        public static string OpenFilePanel( string title , string extension )
        {
#if UNITY_EDITOR
            return EditorUtility.OpenFilePanel( title , CurrentDirectory , extension );
#elif UNITY_STANDALONE_WIN
        try
        {
            OpenDialogFile ofn = new OpenDialogFile();
            ofn.structSize = Marshal.SizeOf( ofn );
            //        ofn.filter = "All Files\0*.*\0\0";
            ofn.filter = extension + "\0*." + extension + "\0\0";
            ofn.file = new string( new char[ 1024 ] );
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string( new char[ 64 ] );
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = CurrentDirectory;
            ofn.title = title;
            ofn.flags = 0x00001000 | 0x00000800 | 0x00000004;

            if ( GetOpenFileName( ofn ) )
            {
                return ofn.file;
            }
        }
        catch ( Exception )
        {
        }

        return "";
#else
        return "";
#endif
        }

        public static string SaveFilePanel( string title , string extension )
        {
#if UNITY_EDITOR
            return EditorUtility.SaveFilePanel( title , CurrentDirectory , "" , extension );
#elif UNITY_STANDALONE_WIN
        try
        {
            OpenDialogFile ofn = new OpenDialogFile();
            ofn.structSize = Marshal.SizeOf( ofn );
            ofn.filter = extension + "\0*." + extension + "\0\0";
            ofn.file = new string( new char[ 1024 ] );
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string( new char[ 64 ] );
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = CurrentDirectory;
            ofn.title = title;
            ofn.flags = 0x00000800 | 0x00000004 | 0x00000002;
            ofn.defExt = extension;

            if ( GetOpenFileName( ofn ) )
            {
                return ofn.file;
            }
        }
        catch ( Exception )
        {
        }

        return "";
#else
        return "";
#endif
        }



        public delegate void LoadCallback( byte[] bytes , bool err );
        public static void LoadRes( MonoBehaviour monoBehaviour , string path , LoadCallback cb )
        {
#if ( UNITY_WEBGL || UNITY_ANDROID ) && !UNITY_EDITOR
            monoBehaviour.StartCoroutine( WebLoad( path , cb ) );
#else
            byte[] bytes = ReadFile( path );
            cb( bytes , bytes == null );
#endif
        }

        public static byte[] ReadFile( string path )
        {
            if ( !File.Exists( path ) )
            {
                return null;
            }

            FileStream stream = null;

            try
            {
                stream = File.Open( path , FileMode.Open , FileAccess.ReadWrite , FileShare.ReadWrite );
            }
            catch ( Exception )
            {
                return null;
            }

            int length = (int)stream.Length;

            byte[] bytes = new byte[ length ];
            stream.Read( bytes , 0 , length );
            stream.Close();
            stream.Dispose();
            return bytes;
        }

        public static void HttpGet( MonoBehaviour monoBehaviour , string url , LoadCallback cb )
        {
            monoBehaviour.StartCoroutine( HttpGet( url , cb ) );
        }

        static IEnumerator HttpGet( string url , LoadCallback cb )
        {
            UnityWebRequest web = UnityWebRequest.Get( url );
            yield return web.SendWebRequest();

            if ( web.isDone )
            {
                if ( web.error != null )
                {
                    cb( null , true );
                }
                else
                {
                    cb( web.downloadHandler.data , false );
                    web.Dispose();
                }
            }
            else
            {
                cb( null , true );
            }
        }


        static IEnumerator WebLoad( string url , LoadCallback cb )
        {
#if UNITY_ANDROID
#elif UNITY_IPHONE
#elif UNITY_WEBGL
#elif UNITY_STANDALONE
            url = "file:///" + url;
#elif UNITY_WSA
#else
#endif

            UnityWebRequest web = UnityWebRequest.Get( url );
            yield return web.SendWebRequest();

            if ( web.isDone )
            {
                if ( web.error != null )
                {
                    cb( null , true );
                }
                else
                {
                    cb( web.downloadHandler.data , false );
                    web.Dispose();
                }
            }
            else
            {
                cb( null , true );
            }
        }


        public delegate void LoadAudioCallback( AudioClip ac , bool err );

        public static void LoadAudio( MonoBehaviour monoBehaviour , bool file , string path , AudioType type , LoadAudioCallback cb )
        {
            monoBehaviour.StartCoroutine( AudioLoad( path , file , type , cb ) );
        }

        static IEnumerator AudioLoad( string url , bool file , AudioType type , LoadAudioCallback cb )
        {
            if ( file )
            {
                url = "file:///" + url;
            }

            UnityWebRequest web = UnityWebRequestMultimedia.GetAudioClip( url , type );
            yield return web.SendWebRequest();

            if ( web.isDone )
            {
                if ( web.error != null )
                {
                    cb( null , true );
                }
                else
                {
                    try
                    {
                        AudioClip myClip = DownloadHandlerAudioClip.GetContent( web );
                        cb( myClip , false );
                        web.Dispose();
                    }
                    catch ( Exception )
                    {
                        cb( null , true );
                        web.Dispose();
                    }
                }
            }
            else
            {
                cb( null , true );
            }
        }

        public static bool ZipFile( List<string> files , List<string> paths , string rootPath , string destinationPath , int compressLevel )
        {
#if UNITY_STANDALONE_WIN
            rootPath = rootPath.Replace( "/" , "\\" );
            destinationPath = destinationPath.Replace( "/" , "\\" );
#endif
            try
            {
                string rootMark = rootPath;
                Crc32 crc = new Crc32();
                ZipOutputStream outPutStream = new ZipOutputStream( File.Create( destinationPath ) );
                outPutStream.SetLevel( compressLevel );
                outPutStream.Password = "xStudios.net.1911";

                foreach ( string file1 in files )
                {
#if UNITY_STANDALONE_WIN
                    string file = file1.Replace( "/" , "\\" );
#else
                    string file = file1;
#endif

                    FileStream fileStream = File.Open( file , FileMode.Open , FileAccess.ReadWrite , FileShare.ReadWrite );
                    byte[] buffer = new byte[ fileStream.Length ];
                    fileStream.Read( buffer , 0 , buffer.Length );
                    ZipEntry entry = new ZipEntry( file.Replace( rootMark , string.Empty ) );
                    entry.DateTime = DateTime.Now;

                    entry.Size = fileStream.Length;
                    fileStream.Close();
                    fileStream.Dispose();
                    crc.Reset();
                    crc.Update( buffer );
                    entry.Crc = crc.Value;
                    outPutStream.PutNextEntry( entry );
                    outPutStream.Write( buffer , 0 , buffer.Length );
                }

                files.Clear();

                foreach ( string emptyPath1 in paths )
                {
#if UNITY_STANDALONE_WIN
                    string emptyPath = emptyPath1.Replace( "/" , "\\" );
#else
                    string emptyPath = emptyPath1;
#endif
                    ZipEntry entry = new ZipEntry( emptyPath.Replace( rootMark , string.Empty ) + "/" );
                    outPutStream.PutNextEntry( entry );
                }

                paths.Clear();

                outPutStream.Finish();
                outPutStream.Close();
                outPutStream.Dispose();

                GC.Collect();

                return true;
            }
            catch ( Exception e )
            {
                Debug.LogError( e.Message );
            }

            return false;
        }


#if UNITY_STANDALONE_WIN

        public static bool UnZipFile( string file , string fileDir )
        {
            ZipConstants.DefaultCodePage = 0;

#if UNITY_EDITOR
            Debug.Log( fileDir );
#endif
            fileDir = fileDir.Replace( "/" , "\\" );

            try
            {
                FileStream fs = File.Open( file , FileMode.Open , FileAccess.ReadWrite , FileShare.ReadWrite );

                if ( !Directory.Exists( fileDir ) )
                {
                    Directory.CreateDirectory( fileDir );
                }

                ZipInputStream s = new ZipInputStream( fs );
                s.Password = "xStudios.net.1911";

                ZipEntry theEntry;
                string path = fileDir;
                string rootDir = " ";
                while ( ( theEntry = s.GetNextEntry() ) != null )
                {
                    rootDir = Path.GetDirectoryName( theEntry.Name );
                    //			rootDir.Replace( "\\" , "/" );

                    if ( rootDir.IndexOf( "\\" ) >= 0 )
                    {
                        rootDir = rootDir.Substring( 0 , rootDir.IndexOf( "\\" ) + 1 );
                    }

                    string dir = Path.GetDirectoryName( theEntry.Name );
                    string fileName = Path.GetFileName( theEntry.Name );

                    //			dir.Replace( "\\" , "/" );
                    //			fileName.Replace( "\\" , "/" );

                    if ( dir != " " )
                    {
                        path = fileDir + "\\" + dir;

                        if ( !Directory.Exists( path ) )
                        {
                            Directory.CreateDirectory( path );
                        }
                    }
                    else if ( dir == " " && fileName != "" )
                    {
                        path = fileDir;
                    }
                    else if ( dir != " " && fileName != "" )
                    {
                        if ( dir.IndexOf( "\\" ) > 0 )
                        {
                            path = fileDir + "\\" + dir;
                        }
                    }
                    if ( dir == rootDir )
                    {
                        path = fileDir + "\\" + rootDir;
                    }

                    if ( fileName != String.Empty )
                    {
                        FileStream streamWriter = File.Create( path + "\\" + fileName );

                        int size = 2048;
                        byte[] data = new byte[ 2048 ];
                        while ( true )
                        {
                            size = s.Read( data , 0 , data.Length );
                            if ( size > 0 )
                            {
                                streamWriter.Write( data , 0 , size );
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }

                fs.Close();
                fs.Dispose();

                s.Close();
                s.Dispose();

                GC.Collect();

                return true;
            }
            catch ( Exception )
            {
                return false;
            }
        }

#else

	public static bool UnZipFile( string file , string fileDir )
	{
        ZipConstants.DefaultCodePage = 0;

#if UNITY_EDITOR
        Debug.Log(fileDir);
#endif
        fileDir = fileDir.Replace("\\", "/");

        try
        {
            FileStream fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            ZipInputStream s = new ZipInputStream( fs );
            s.Password = "xStudios.net.1911";

            ZipEntry theEntry;
            string path = fileDir;
            string rootDir = " ";
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string ename = theEntry.Name.Replace( "\\" , "/" );

                rootDir = Path.GetDirectoryName(ename);
                //          rootDir.Replace( "\\" , "/" );

                if (rootDir.IndexOf("/") >= 0)
                {
                    rootDir = rootDir.Substring(0, rootDir.IndexOf("/") + 1);
                }

                string dir = Path.GetDirectoryName(ename);
                string fileName = Path.GetFileName(ename);

                //          dir.Replace( "\\" , "/" );
                //          fileName.Replace( "\\" , "/" );

                if (dir != " ")
                {
                    path = fileDir + "/" + dir;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                else if (dir == " " && fileName != "")
                {
                    path = fileDir;
                }
                else if (dir != " " && fileName != "")
                {
                    if (dir.IndexOf("/") > 0)
                    {
                        path = fileDir + "/" + dir;
                    }
                }
                if (dir == rootDir)
                {
                    path = fileDir + "/" + rootDir;
                }

                if (fileName != String.Empty)
                {
                    FileStream streamWriter = File.Create(path + "/" + fileName);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            s.Close();
            s.Dispose();

            return true;
        }
        catch ( Exception e )
        {
            return false;
        }
    }

#endif


    }

}

