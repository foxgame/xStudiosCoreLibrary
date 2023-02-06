using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace X.UI
{
    public class UISelection : MonoBehaviour
    {
        [SerializeField]
        Image imageSelect;

        bool selected = false;

        public bool Selected { get { return selected; } }

        private void Awake()
        {
            imageSelect.gameObject.SetActive( false );
        }

        public void Select( bool b )
        {
            selected = b;
            imageSelect.gameObject.SetActive( b );
        }
    }
}
