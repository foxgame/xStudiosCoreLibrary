using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace X
{
    [CustomEditor( typeof( TestMeshViewer ) )]
    public class MeshViewerEditor : Editor
    {
        private bool showInfo = true;
        private bool showUV = true;
        private bool showVertice = true;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if ( GUILayout.Button( "ReadMeshInfo" ) )
            {
                TestMeshViewer test = (TestMeshViewer)target;
                test.ReadMeshInfo();
            }

            showInfo = GUILayout.Toggle( showInfo , "Show Info" );
            showVertice = GUILayout.Toggle( showVertice , "Show Vertice" );
            showUV = GUILayout.Toggle( showUV , "Show UV" );
        }

        private void OnSceneGUI()
        {
            if ( !showInfo )
            {
                return;
            }
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            style.fontSize = 15;

            TestMeshViewer viewer = target as TestMeshViewer;
            Dictionary<Vector3 , StringBuilder> posList = new Dictionary<Vector3 , StringBuilder>();

            for ( int i = 0, imax = viewer.verticesList.Count ; i < imax ; ++i )
            {
                Vector3 vPos = viewer.transform.TransformPoint( viewer.verticesList[ i ] );
                StringBuilder sb;
                if ( posList.TryGetValue( vPos , out sb ) )
                {
                    StringBuilder str = new StringBuilder( "v:" + i );
                    AddVerticeStr( ref str , vPos );
                    AddUVStr( ref str , ref viewer.uvList , i );
                    sb.AppendLine( str.ToString() );
                }
                else
                {
                    sb = new StringBuilder();
                    StringBuilder str = new StringBuilder( "v:" + i );
                    AddVerticeStr( ref str , vPos );
                    AddUVStr( ref str , ref viewer.uvList , i );
                    sb.AppendLine( str.ToString() );
                    posList.Add( vPos , sb );
                }

                Handles.Label( vPos , sb.ToString() , style );
            }
        }

        private void AddVerticeStr( ref StringBuilder sb , Vector3 vert )
        {
            if ( !showVertice )
            {
                return;
            }
            sb.Append( ",vertice:" + vert );
        }

        private void AddUVStr( ref StringBuilder sb , ref List<Vector2> uvList , int index )
        {
            if ( !showUV )
            {
                return;
            }

            if ( uvList.Count > index )
            {
                sb.Append( ",uv:" + uvList[ index ] );
            }
        }
    }
}