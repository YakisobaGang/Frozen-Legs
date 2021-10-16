using UnityEngine;
using UnityEngine.SceneManagement;

namespace YakisobaGang
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadSceneAfterTime(float time)
        {
            Invoke(nameof(LoadScene), time);   
        }
    }
}