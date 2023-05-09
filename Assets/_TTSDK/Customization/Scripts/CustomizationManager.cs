using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TT.Customization
{
    public class CustomizationManager : MonoBehaviour
    {
        public static CustomizationManager instance;

        public List<Customization> customizationDataList;

        public CustomCharacter character;

        private void Awake()
        {
            instance = this;
        }

        public void Apply(Customization custom)
        {
            character.skinMat.color = custom.skinColor;
            character.hairMat.color = custom.hairColor;
            character.beardMat.color = custom.beardColor;
            character.tshirtMat.color = custom.tshirtColor;
            character.pantMat.color = custom.pantColor;
            character.shoeMat.color = custom.shoeColor;

            character.hair.SetActive(custom.hair);
            character.tshirt.SetActive(custom.tshirt);
            character.tshirt001.SetActive(custom.tshirt);
            character.belt.SetActive(custom.belt);

            character.beard1.SetActive(custom.beard1);
            character.beard2.SetActive(custom.beard2);
            character.beard3.SetActive(custom.beard3);

            character.mustache1.SetActive(custom.mustache1);
            character.mustache2.SetActive(custom.mustache2);
            character.mustache3.SetActive(custom.mustache3);

            character.leftEarning1.SetActive(custom.leftEarning1);
            character.leftEarning2.SetActive(custom.leftEarning2);
            character.leftEarning3.SetActive(custom.leftEarning3);

            character.rightEarning1.SetActive(custom.rightEarning1);
            character.rightEarning2.SetActive(custom.rightEarning2);
            character.rightEarning3.SetActive(custom.rightEarning3);
        }
    }

    [Serializable]
    public class Customization
    {
        public Color skinColor;
        public Color hairColor;
        public Color beardColor;
        public Color tshirtColor;
        public Color pantColor;
        public Color shoeColor;

        public bool hair;
        public bool tshirt;
        public bool belt;

        public bool beard1;
        public bool beard2;
        public bool beard3;

        public bool mustache1;
        public bool mustache2;
        public bool mustache3;

        public bool leftEarning1;
        public bool leftEarning2;
        public bool leftEarning3;

        public bool rightEarning1;
        public bool rightEarning2;
        public bool rightEarning3;
    }
}
