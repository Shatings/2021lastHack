using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class TextM : MonoBehaviour
{
    public List<Text> text = new List<Text>();
    public GameM gamem;
    public Player palyer;
    public string nickname;
    public bool check = false;
    public GameObject inputtext;
    // Start is called before the first frame update
    void Start()
    {
        gamem = FindObjectOfType<GameM>();
        palyer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (!gamem.endgame)
        {
            HpT();
            Score();
        }
        else
        {
            if (!check)
            {
                text[0].text = "체력" + 0;
                inputtext.SetActive(true);
                check = true;
            }
        }
    }
    public void HpT()
    {
        text[0].text = "체력" + palyer.hp;
    }
    public void Score()
    {
        text[1].text = "점수" + gamem.scorre;
    }
    public void InputName(string _nickname)
    {
        nickname = _nickname;
    }
    public void Logins()
    {
        StartCoroutine(LoginStart()) ; 
    }
    IEnumerator LoginStart()
    {

        WWWForm wwf = new WWWForm();
        wwf.AddField("nickname", nickname);
        wwf.AddField("score", gamem.scorre);
        UnityWebRequest www = UnityWebRequest.Post("http://172.16.7.168:3000", wwf);
        Debug.Log(www);
        yield return www.SendWebRequest();
        text[3].gameObject.SetActive(true);
        if (www.isNetworkError || www.isHttpError)
        {

            text[2].text = www.error;
            Debug.Log(www.error);
        }
        else
        {
            text[2].text = www.downloadHandler.text;
        }

      
    }
}
