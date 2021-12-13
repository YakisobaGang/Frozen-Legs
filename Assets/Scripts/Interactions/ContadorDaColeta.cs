using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace YakisobaGang.Interactions
{
    public class ContadorDaColeta : MonoBehaviour
    {

        static int contador = 0;
        public GameObject wave1;
        public GameObject wave2;
        public GameObject wave3;
        public GameObject wave4;
        public GameObject vitoria;

        public TMP_Text text;
        private void Awake()
        {
            contador = 0;
        }

        private void Update()
        {
            if (contador == 3)
            {
                wave1.SetActive(true);
                wave2.SetActive(true);
                wave3.SetActive(true);
                wave4.SetActive(true);
            }
            if (contador == 7)
            {
                vitoria.SetActive(true);
                contador = 0;
            }

        }

        private void OnEnable()
        {
            Coletar.OnPickup += ColetarOnOnPickup();
        }

        private void OnDisable()
        {
            Coletar.OnPickup -= ColetarOnOnPickup();
        }

        private Action<int> ColetarOnOnPickup()
        {
            return (x) =>
            {
                contador += x;
                text?.SetText(contador.ToString());
            };
        }
    }
}