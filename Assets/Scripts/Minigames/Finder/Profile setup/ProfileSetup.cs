using System.Collections.Generic;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine;

public class ProfileSetup : MonoBehaviour {
    private int _currentStepIndex;

    [SerializeField] public FinderController FinderController;
    public DataTable FinderProfile;

    [SerializeField] public GameObject[] Steps;

    private void Awake() {
        var likedProfileIDs = new List<string>();
        var profile = AppData.Instance().Registry.Fetch(FinderController.ProfileTable);
        if (profile != null) {
            var table = AppData.Instance().Registry.Fetch(FinderController.LikeTable);
            table.Select("*", "", reader => {
                while (reader.Read())
                    likedProfileIDs.Add(reader["ProfileID"].ToString());
            });

            gameObject.SetActive(false);
            FinderController.gameObject.SetActive(true);
            FinderController.LikedProfileIDs = likedProfileIDs;
            return;
        }

        FinderProfile = new DataTable(FinderController.ProfileTable);
        FinderProfile.AddProperty(new DataProperty("Name", DataProperty.DataPropertyType.VARCHAR));
        FinderProfile.AddProperty(new DataProperty("Age", DataProperty.DataPropertyType.INT));
        FinderProfile.AddProperty(new DataProperty("City", DataProperty.DataPropertyType.VARCHAR));
        FinderProfile.AddProperty(new DataProperty("PhoneNumber", DataProperty.DataPropertyType.INT));
        FinderProfile.AddProperty(new DataProperty("FavMovie", DataProperty.DataPropertyType.VARCHAR));
        FinderProfile.AddProperty(new DataProperty("FavMusic", DataProperty.DataPropertyType.VARCHAR));
        FinderProfile.AddProperty(new DataProperty("FavFood", DataProperty.DataPropertyType.VARCHAR));
        FinderProfile.AddProperty(new DataProperty("FavSport", DataProperty.DataPropertyType.VARCHAR));
        FinderProfile.AddProperty(new DataProperty("FavGame", DataProperty.DataPropertyType.VARCHAR));
        FinderProfile.AddProperty(new DataProperty("FavVacation", DataProperty.DataPropertyType.VARCHAR));
        AppData.Instance().Registry.Register(FinderProfile);
    }

    /// <summary>
    ///     Disables the current panel and activates the next panel from the Steps list
    /// </summary>
    public void NextStep() {
        Steps[_currentStepIndex].SetActive(false);
        _currentStepIndex += 1;
        Steps[_currentStepIndex].SetActive(true);
    }
}