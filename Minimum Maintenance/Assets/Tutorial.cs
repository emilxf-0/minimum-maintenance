using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadNext), 7f);
    }

    private void LoadNext()
    {
        SceneManager.LoadScene("MainScene");
    }
    
}
