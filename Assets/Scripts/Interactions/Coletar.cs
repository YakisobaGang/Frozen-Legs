using UnityEngine;

namespace YakisobaGang.Interactions
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
