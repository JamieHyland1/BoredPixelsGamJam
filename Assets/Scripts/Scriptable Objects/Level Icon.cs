using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New level", menuName = "Level Icon")]
public class LevelIcon : ScriptableObject
{
    public string LevelName;

    public Sprite Levelimage;

    public Scene LevelScene;

    public void Startlevel() {

        SceneManager.LoadScene(1);

    }

}
