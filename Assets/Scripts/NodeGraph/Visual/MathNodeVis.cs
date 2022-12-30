using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NodeGraph.Visual
{
    public class MathNodeVis : NodeVis
    {
        MathNode self;
        TextMeshProUGUI description;
        TMP_Dropdown dropdown;

        List<TMP_Dropdown.OptionData> dropdown_List = new List<TMP_Dropdown.OptionData> {
                new TMP_Dropdown.OptionData(MathType.Add.ToString()),
                new TMP_Dropdown.OptionData(MathType.Subtract.ToString()),
                new TMP_Dropdown.OptionData(MathType.Multiply.ToString()),
                new TMP_Dropdown.OptionData(MathType.Divide.ToString()),
                new TMP_Dropdown.OptionData(MathType.Modulus.ToString()),
        };

        void Start()
        {
            self = GetComponent<MathNode>();
            description = GetComponentInChildren<TextMeshProUGUI>();
            dropdown = GetComponentInChildren<TMP_Dropdown>();
            dropdown.options = dropdown_List;
        }

        public void OnValueChanged(int val)
        {
            self.Type = (MathType)(val);
        }

        public void OnTypeChanged()
        {
            description.text = self.Type.ToString();
        }
    }
}