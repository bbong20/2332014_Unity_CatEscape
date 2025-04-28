using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void f_RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}