using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using LitJson;

public class DB : MonoBehaviour
{
    public List<Text> nicknames = new List<Text>();
    public List<Text> scores = new List<Text>();
    public List<Text> times = new List<Text>();
    public Transform dbtransform;

    public Text loadingt;
    public GameObject dbrank;
    // Start is called before the first frame update
    void Start()
    {
        SetAtcive();
        for(int i = 0; i < dbtransform.childCount; i++)
        {
            nicknames.Add(dbtransform.GetChild(i).transform.Find("NickName").GetComponent<Text>());
            scores.Add(dbtransform.GetChild(i).transform.Find("Score").GetComponent<Text>());
            times.Add(dbtransform.GetChild(i).transform.Find("Time").GetComponent<Text>());
        }
    }
    public void ClickDB()
    {
        StartCoroutine(LoginStart());
    }
    public void SetAtcive()
    {
        dbrank.SetActive(!dbrank.activeSelf);
    }
    IEnumerator LoginStart()
    {

       
        UnityWebRequest www = UnityWebRequest.Get("http://ec2-15-165-161-44.ap-northeast-2.compute.amazonaws.com:8000");
        Debug.Log(www);
        loadingt.gameObject.SetActive(true);
        loadingt.text = "·ÎµùÁß";
        yield return www.SendWebRequest();
       
        if (www.isNetworkError || www.isHttpError)
        {
            loadingt.text = www.error;
            Debug.Log(www.error);
        }
        else
        {
            loadingt.gameObject.SetActive(false);
            SetAtcive();
            Debug.Log(www.downloadHandler.text);
            File.WriteAllBytes(Application.dataPath + "Login.json", www.downloadHandler.data);
            string tokendata = File.ReadAllText(Application.dataPath + "Login.json");
            JsonData token = JsonMapper.ToObject(tokendata);
            Debug.Log(token.ToString());
            for(int i = 0; i < token.Count; i++)
            {
                nicknames[i].text = token["data"][i]["nickname"].ToString();
                scores[i].text = ((int)token["data"][i]["score"]).ToString();
                times[i].text = ((long)token["data"][i]["time"]).ToString();
            }
            
            File.Delete(Application.dataPath + "Login.json");
        }


    }
}
