using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YakisobaGang.Interactions;

namespace YakisobaGang
{
    public class Coletar : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Pegou!");
                ContadorDaColeta.adicionar();
                Destroy(this.gameObject);
            }
        }
    }
}
