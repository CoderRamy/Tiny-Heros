using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Localisation.Creator
{
    public class ClueItem : MonoBehaviour
    {

        public InputField number;
        public InputField data;

        public void UpdateData()
        {
            number.text = number.text.ToUpper();
        }

        public void GetData()
        {
            UpdateData();
        }

        public void SetData(string hintIndex, string hintDesc)
        {
            number.text = hintIndex.ToUpper();
            data.text = hintDesc;
        }
    }
}