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
    public bool clickafag = false;
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
        if (_nickname.Length < 5)
        {
            nickname = _nickname;
        }
        else
        {
            text[2].text = "닉넴이 너무 길어요";
        }
    }
    public void Logins()
    {
        if (clickafag == false)
        {
            StartCoroutine(LoginStart());
            clickafag = true;
        }
        else
        {
            text[2].text = "이미 입력하셨어요 가세요";
        }
    }
    IEnumerator LoginStart()
    {

        WWWForm wwf = new WWWForm();
        wwf.AddField("nickname", nickname);
        wwf.AddField("score", gamem.scorre);
        UnityWebRequest www = UnityWebRequest.Post("http://ec2-15-165-161-44.ap-northeast-2.compute.amazonaws.com:8000", wwf);
        Debug.Log(www);
        yield return www.SendWebRequest();
        text[2].gameObject.SetActive(true);
        if (www.isNetworkError || www.isHttpError)
        {

            text[2].text = www.error;
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("성공~");
            text[2].text = www.downloadHandler.text;
        }
    }
}
