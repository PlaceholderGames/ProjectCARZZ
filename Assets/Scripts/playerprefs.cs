using UnityEngine;
using UnityEngine.UI;

public class playerprefs : MonoBehaviour {

    public Text text;
    int whatever;
    private PlayerPrefs playerPres;

	void Start () {
        whatever = PlayerPrefs.GetInt("playerPres");
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            whatever += 1;
        PlayerPrefs.SetInt("playerPres", whatever);
        text.text=("Points:" + whatever);
	}
}
