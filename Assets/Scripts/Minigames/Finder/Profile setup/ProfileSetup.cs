using System.Collections.Generic;
using UnityEngine;

public class ProfileSetup : MonoBehaviour {
    private int _currentStepIndex;

    [SerializeField] public FinderController FinderController;

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
        }
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