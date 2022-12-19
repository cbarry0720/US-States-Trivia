using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string startScene = "SampleScene";

    public void Start_Game() {
        SceneManager.LoadScene(startScene);
    }
}
