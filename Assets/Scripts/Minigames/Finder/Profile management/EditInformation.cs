using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.App;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Data_Management.Table;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Minigames.Finder.Profile_management {
    public class EditInformation : MonoBehaviour {
        private List<Text> _fields;
        private DataTable _profileTable;

        [SerializeField] public GameObject FormContent;
        [SerializeField] public GameObject Warning;

        // Use this for initialization
        private void Start() {
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

        /// <summary>
        ///     Save the updated information locally and online
        /// </summary>
        public void SaveChanges() {
            var parameters = DataParams.Build();

            var handshake = new InformationProtocol(Protocol.Data)
                .SetHandler("finderProfileUpdate", InformationProtocol.HandlerType.Update)
                .AddParameter("action", "update")
                .AddParameter("name", PlayerPrefs.GetString("name"))
                .AddParameter("uid", PlayerPrefs.GetString("uid"));

            foreach (var field in _fields) {
                var key = field.name;
                var value = field.GetComponentInChildren<InputField>().text;

                if (string.IsNullOrEmpty(value)) {
                    Warning.SetActive(true);
                    return;
                }

                if (field)
                    parameters.Append(key, value);
                handshake.AddParameter(key, value);
            }

            _profileTable.Update(parameters, "");
            handshake.Send();
            FindObjectOfType<ProfileManagement>().OpenView();
        }
    }
}