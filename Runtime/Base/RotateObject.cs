using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace X
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField]
        float roatateSpeed = 1.0f;

        private void Update()
        {
            transform.Rotate( new Vector3( 0f , roatateSpeed , 0f ) );
        }
    }
}
