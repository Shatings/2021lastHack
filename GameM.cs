using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    
    public List<GameObject> emys = new List<GameObject>();
    public List<GameObject> gameObjectscore = new List<GameObject>();
  
    
    public Transform emysrespwn;
    public Transform scorerspwn;

    public List<float> respwntime = new List<float>();
    public List<float> reswpwendtime = new List<float>();
  
   
   
    public int emyindex = 0;
    public int goal = 0;
    public List<Transform> respawn = new List<Transform>();
    public int scorre = 0;
    public bool endgame = false;
    public bool waitmove;
    [SerializeField]
    public float jumppower;
    public string type;
    void Awake()
    {
        type = "EMy";
        ListAdd(emysrespwn,emys);
        ListAdd(scorerspwn, gameObjectscore);
    
       
    }
    public void ListAdd(Transform _transform,List<GameObject> _list)
    {
        for(int i = 0; i < _transform.childCount; i++)
        {
            _list.Add(_transform.GetChild(i).gameObject);
            _list[i].SetActive(false);
        }
    }
    public void AddTime()
    {
        for(int i = 0; i < respwntime.Count; i++)
        {
            respwntime[i] += Time.deltaTime;
            if (respwntime[i] > reswpwendtime[i])
            {
                if (i == 0)
                {
                    EmySetAtive(emys, i, emysrespwn,emyindex);
                    CheckY(emysrespwn, emys);
                }
                else
                {
                    EmySetAtive(gameObjectscore, i, scorerspwn,goal);
                    CheckY(scorerspwn, gameObjectscore);
                }
            }
        }
    }
    void Update()
    {
        if (!endgame)
        {
            AddTime();
           
        }
        else
        {
            jumppower = 0;
            waitmove = true;
            
        }
      
    }
    public void EmySetAtive(List<GameObject> _list, int _index,Transform _transform,int _listcount)
    {
        _list[_listcount].SetActive(true);
        Transform(_list[_listcount],respawn[Random.Range(0, 2)], respawn[2]);
        respwntime[_index] = 0;
       
        if (_index == 0)
        {
            if (emyindex < _transform.childCount - 1)
            {
                emyindex++;
            }
            else
            {
                emyindex = 0;
            }
        }
        else
        {
            if (goal < _transform.childCount - 1)
            {
                goal++;
            }
            else
            {
                goal = 0;
            }
            
        }
       
        
        
    }
    public void Transform(GameObject _gameob,Transform _resp,Transform _middle)
    {
        _gameob.transform.position = new Vector3(Random.Range(_resp.position.x, _middle.position.x), (_resp.position.y+4));
    }
    public void EmySet()
    {
        
        for(int i = 0; i < emys.Count; i++)
        {
            emys[i].SetActive(false);
            emys[i].transform.position = emysrespwn.position;
        }
    }
    public void CheckY(Transform _transform, List<GameObject> _list)
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
            if (FindObjectOfType<Player>().CheckY(_list[i], _transform,type))
            {
                _list[i].SetActive(false);
                scorre++;
            }
        }
    }
}
