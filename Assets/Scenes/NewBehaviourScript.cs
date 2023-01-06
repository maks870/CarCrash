using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] SceneAsset scene;
    
    void Start()
    {
        SceneManager.LoadScene(scene.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
