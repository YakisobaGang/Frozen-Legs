using Unity.VisualScripting;
using UnityEngine;

namespace YakisobaGang.Interactions
{
    public class ContadorDaColeta : MonoBehaviour
    {
        static int contador = 0;
        public GameObject vitoria;
        void Awake()
        {
            contador = 0;
        }

        void Update()
        {
            if (contador == 3)
            {
                vitoria.SetActive(true);
                contador = 0;
            }
        }

        public static void adicionar()
        {
            contador = contador + 1;
        }
    }
}