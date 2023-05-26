using UnityEngine;
using Firebase.Database;
using TMPro;

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

    public void CreateUserDataInfo()
    {
        DataInfo dataInfo = new DataInfo(uiController.nameInpFld.text, uiController.emailInpFld.text);
        string json = JsonUtility.ToJson(dataInfo);

        databaseReference.Child("DataInfo").Child(dataInfo_Id).SetRawJsonValueAsync(json);
    }

}
