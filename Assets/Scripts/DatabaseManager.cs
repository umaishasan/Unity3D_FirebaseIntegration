using UnityEngine;
using Firebase.Database;
using TMPro;
using System.Collections;
using System;

public class DatabaseManager : MonoBehaviour
{
    #region Var

    private string dataInfo_Id;
    private DatabaseReference databaseReference;

    [Header("-Script")]
    [SerializeField] UIController uiController;

    #endregion

    #region Unity_Events

    private void Reset()
    {
        uiController = FindObjectOfType<UIController>(true);
    }

    private void Start()
    {
        dataInfo_Id = SystemInfo.deviceUniqueIdentifier;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    #endregion

    #region Firebase_CRUD

    public void CreateUserDataInfo()
    {
        DataInfo dataInfo = new DataInfo(uiController.nameInpFld.text, uiController.emailInpFld.text);
        string json = JsonUtility.ToJson(dataInfo);

        databaseReference.Child("DataInfo").Child(dataInfo_Id).SetRawJsonValueAsync(json);

        Debug.LogError("what is get in json? "+json.ToString());
    }

    public void UpdateUserDataInfo()
    {
        CommonMethodUpdateDataInfo("name", uiController.nameInpFld.text);
        CommonMethodUpdateDataInfo("email", uiController.emailInpFld.text);
    }

    public void RetriveUserDataInfo()
    {
        StartCoroutine(GettingDataFromCommonMethod((string name) =>
        {
            uiController.nameTxt.text = name;
        }, "name"));

        StartCoroutine(GettingDataFromCommonMethod((string email) =>
        {
            uiController.emailTxt.text = email;
        }, "email"));
    }

    public void DeleteUserDataInfo()
    {
        databaseReference.Child("DataInfo").Child(dataInfo_Id).RemoveValueAsync();
    }

    #endregion

    #region CommonMethod

    /// <summary>
    /// For Get Data From Firebase database
    /// </summary>
    /// <param name="onCallBack"></param>
    /// <param name="variableName"></param>
    /// <returns></returns>
    IEnumerator GettingDataFromCommonMethod(Action<string> onCallBack, string variableName)
    {
        var getData = databaseReference.Child("DataInfo").Child(dataInfo_Id).Child(variableName).GetValueAsync();
        yield return new WaitUntil(predicate: () => getData.IsCompleted);
        if(getData != null)
        {
            DataSnapshot dataSnapshot = getData.Result;
            onCallBack.Invoke(dataSnapshot.Value.ToString());
        }
    } 

    void CommonMethodUpdateDataInfo(string variableName, string setValue)
    {
        databaseReference.Child("DataInfo").Child(dataInfo_Id).Child(variableName).SetValueAsync(setValue);
    }

    #endregion
}
