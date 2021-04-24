﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SendRateField.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit GameScene
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.GameScene.Cockpit
{
    /// <summary>
    /// PhotonNetwork.SendRate InputField.
    /// </summary>
    public class SendRateField : MonoBehaviour
    {

        public InputField PropertyValueInput;

        int _cache;

        bool registered;

        void OnEnable()
        {
            if (!registered)
            {
                registered = true;
                PropertyValueInput.onEndEdit.AddListener(OnEndEdit);
            }
        }

        void OnDisable()
        {
            registered = false;
            PropertyValueInput.onEndEdit.RemoveListener(OnEndEdit);
        }

        void Update()
        {
            if (PhotonNetwork.SendRate != _cache)
            {
                _cache = PhotonNetwork.SendRate;
                PropertyValueInput.text = _cache.ToString();
            }
        }

        // new UI will fire "EndEdit" event also when loosing focus. So check "enter" key and only then StartChat.
        public void OnEndEdit(string value)
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Tab))
            {
                this.SubmitForm(value.Trim());
            }
            else
            {
                this.SubmitForm(value);
            }

        }

        public void SubmitForm(string value)
        {
            _cache = int.Parse(value);
            PhotonNetwork.SendRate = _cache;
            //Debug.Log("PhotonNetwork.SendRate = " + PhotonNetwork.SendRate, this);
        }
    }
}