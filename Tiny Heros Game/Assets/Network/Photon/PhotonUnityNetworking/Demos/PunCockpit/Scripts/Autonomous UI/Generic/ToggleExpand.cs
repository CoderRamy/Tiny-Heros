// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToggleExpand.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit GameScene
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.GameScene.Cockpit
{
    /// <summary>
    /// UI toggle to activate GameObject.
    /// </summary>
    public class ToggleExpand : MonoBehaviour
    {
        public GameObject Content;
        public GameObject Content1;

        public Toggle Toggle;

        bool _init;

        void OnEnable()
        {
           // Content.SetActive(Toggle.isOn);

            if (!_init)
            {
                _init = true;
                Toggle.onValueChanged.AddListener(HandleToggleOnValudChanged);
            }

            HandleToggleOnValudChanged(Toggle.isOn);

        }


        void HandleToggleOnValudChanged(bool value)
        {
            Content.SetActive(value);
            if (value)
            {
                Content1.SetActive(false);

            }
            else
            {
                Content1.SetActive(true);

            }
        }

    }
}