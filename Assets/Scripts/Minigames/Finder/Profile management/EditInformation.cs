using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine;
using UnityEngine.UI;

public class EditInformation : MonoBehaviour {
    [SerializeField] public GameObject FormContent;

    private DataTable _profileTable;

    private List<Text> _fields;
    // Use this for initialization
    private void Start () {
        _profileTable = AppData.Instance().Registry.Fetch(FinderController.ProfileTable);
        _fields = FormContent.GetComponentsInChildren<Text>().Where(x => x.tag == "InputName").ToList();

        _profileTable.Select("*", "", reader => {
            if (!reader.Read()) return;

            for (var i = 0; i < _fields.Count; i++) {
                var field = _fields[i];
                field.GetComponentInChildren<InputField>().text = reader[i].ToString();
            }
        });
    }

    public void SaveChanges() {
        var parameters = DataParams.Build();

        var handshake = new InformationProtocol(Protocol.Data)
            .SetHandler("finderProfileUpdate", InformationProtocol.HandlerType.Update)
            .AddParameter("targetTable", "module_finder")
            .AddParameter("uid", PlayerPrefs.GetString("uid"));

        foreach (var field in _fields) {
            var key = field.name;
            var value = field.GetComponentInChildren<InputField>().text;

            parameters.Append(key, value);
            handshake.AddParameter(key, value);
        }

        _profileTable.Update(parameters, "");
        handshake.Send(request => {
            Debug.Log(request.downloadHandler.text);
        });
    }
}
