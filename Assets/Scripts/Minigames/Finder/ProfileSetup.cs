using Assets.Scripts.App.Tracking.Table;
using UnityEngine;

public class ProfileSetup : MonoBehaviour {

    [SerializeField]
    private GameObject[] _steps;
    private int _currentStepIndex;

    public DataTable FinderProfile;
    
    private void Awake() {
        FinderProfile = new DataTable("FinderProfile");
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

        if (FinderProfile.Exists("name"))
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Disables the current panel and activates the next panel from the _steps list
    /// </summary>
    public void NextStep() {
        _steps[_currentStepIndex].SetActive(false);
        _currentStepIndex += 1;
        _steps[_currentStepIndex].SetActive(true);
    }
}
