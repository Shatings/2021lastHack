using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanLoad : MonoBehaviour
{
    public static SceanLoad sceanM;

    public void Awake()
    {
        if (sceanM == null)
        {
            sceanM = this;
           
            SceneManager.sceneLoaded += OnLoadScene;
        }
    }
    
    public void LoadScean(string sceanname)
    {
        SceneManager.LoadScene(""+sceanname);
    }
    public void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        SoundM.instanse.SceanMove();
        if (scene.name == "First")
        {
         
        }
        if (scene.name == "MainGame")
        {
          
           
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }



}
