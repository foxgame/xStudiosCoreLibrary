using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace X
{
    public class TestMeshViewer : MonoBehaviour
    {
        [SerializeField]
        private Mesh mesh;

        public List<Vector3> verticesList = new List<Vector3>();
        public List<Vector2> uvList = new List<Vector2>();
        public List<int> triList = new List<int>();

        public void ReadMeshInfo()
        {
            verticesList.Clear();
            uvList.Clear();
            triList.Clear();

            for ( int i = 0, imax = mesh.vertexCount ; i < imax ; ++i )
            {
                verticesList.Add( mesh.vertices[ i ] );
                uvList.Add( mesh.uv[ i ] );
            }

            for ( int i = 0, imax = mesh.triangles.Length ; i < imax ; ++i )
            {
                triList.Add( mesh.triangles[ i ] );
            }
        }
    }

}

