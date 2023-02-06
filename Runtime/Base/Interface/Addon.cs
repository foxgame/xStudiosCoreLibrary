using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace X
{
    public interface Addon
    {
        public void Start();
        public void Update( float delay );
    }

}


