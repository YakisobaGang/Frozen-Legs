using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YakisobaGang
{
    public class Rodar : MonoBehaviour
    {


        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, Time.deltaTime * 10,  0, Space.Self);
        }
    }
}
