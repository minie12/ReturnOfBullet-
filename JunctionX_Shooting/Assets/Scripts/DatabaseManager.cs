using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class DatabaseManager : MonoBehaviour
{

    public Text scoreText;

    public InputField usernameField;
    public string username;
    public int score;
    public GameObject LeaderBoard;
    public bool isSaved = false;
    public Button savebttn;

    class Rank
    {
        // 순위 정보를 담는 Rank 클래스
        // Firebase와 동일하게 name, score, timestamp를 가지게 해야함
        public string name;
        public int score;
        public string timestamp;
        // JSON 형태로 바꿀 때, 프로퍼티는 지원이 안됨. 프로퍼티로 X

        public Rank(string name, int score, string timestamp)
        {
            // 초기화하기 쉽게 생성자 사용
            this.name = name;
            this.score = score;
            this.timestamp = timestamp;
        }
    }

    private DatabaseReference databaseReference;

    private void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = score.ToString();
        usernameField.characterLimit = 10;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        OnClickLoadButton();
        Transform ranking = LeaderBoard.transform.GetChild(9).GetChild(1);
        string lastscore = ranking.GetComponent<Text>().text;
        if (lastscore == "")
        {
            lastscore = "0";
        }
        if (score <= int.Parse(lastscore))
        {
            savebttn.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        string str;
        str = usernameField.text;
        str = Regex.Replace(str, @"[^0-9a-zA-Z가-힣]", "");
        usernameField.text = str;
    }

    public void OnClickSaveButton()
    {
        username = usernameField.text.Trim();

        //var data = new Data(level, gold);
        score = score * -1;
        Rank rank = new Rank(username, score, DateTime.Now.ToString());
        string jsonData = JsonUtility.ToJson(rank);

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        string key = databaseReference.Child("rank").Push().Key;
        databaseReference.Child("rank").Child(key).SetRawJsonValueAsync(jsonData);

        savebttn.gameObject.SetActive(false);
        
    }

    public void OnClickLoadButton()
    {
        FirebaseDatabase.DefaultInstance.GetReference("rank").OrderByChild("score").LimitToFirst(10).ValueChanged+=HandleValueChanged ;
    }
    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        int i = 0;
        // Do something with the data in args.Snapshot
        foreach (DataSnapshot data in args.Snapshot.Children)
        {
            try
            {
                IDictionary rank = (IDictionary)data.Value;
                // JSON은 사전 형태이기 때문에 딕셔너리 형으로 변환
                Transform ranking = LeaderBoard.transform.GetChild(i);
                ranking.GetChild(0).GetComponent<Text>().text = rank["name"].ToString();
                ranking.GetChild(1).GetComponent<Text>().text = rank["score"].ToString().Substring(1);
                i++;
            }
            catch { }
        }
    }


}