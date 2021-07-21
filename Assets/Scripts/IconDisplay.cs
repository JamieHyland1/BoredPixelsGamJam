using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconDisplay : MonoBehaviour
{
    public LevelIcon icon;

    public Text nameText;
    public Image iconImage;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = icon.LevelName;

        iconImage.sprite = icon.Levelimage;
    }

}
