using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enum with all the attributes related to Manny
/// </summary>
public enum Attribute {
    Food,
    Thirst,
    Level,
    Experience,
    Coins,
    Skillpoints,
    
    // The DNA core values
    DNA_CustomerOriented,
    DNA_Initiative,
    DNA_Responsibility,
    DNA_Creative,
    DNA_Communication,
    DNA_Surpass
}
public class MannyAttribute {
    private Dictionary<Attribute, float> _attributes;

    public MannyAttribute() {
        _attributes = new Dictionary<Attribute, float>();
    }

    /// <summary>
    /// Returns the value of a specific attribute
    /// </summary>
    /// <param name="attribute">The attribute which value needs to be returned</param>
    /// <returns>The attribute's float value</returns>
    public float GetAttribute(Attribute attribute) {
        if (_attributes.Count == 0) Load();
        return _attributes[attribute];
    }

    /// <summary>
    /// Sets a new value for an excisting attribute;
    /// </summary>
    /// <param name="attribute">The attribute which value needs to be changed</param>
    /// <param name="value">The new float value for the attribute</param>
    public void SetAttribute(Attribute attribute, float value) {
        if (_attributes.Count == 0) Load();
        _attributes[attribute] = value >= 0 ? value : 0;
    }

    /// <summary>
    /// Adds an increment value to the value of an excisting attribute;
    /// </summary>
    /// <param name="attribute">The attribute which value needs to be changed</param>
    /// <param name="value">The new float value that needs to be added to the current value</param>
    public void IncrementAttribute(Attribute attribute, float increment) {
        if (_attributes.Count == 0) Load();
        _attributes[attribute] += increment;
        _attributes[attribute] = _attributes[attribute] >= 0 ? _attributes[attribute] : 0;
    }

    /// <summary>
    /// Retrieves all the last saved attribute values from the playerprefs and fill the dictionary with them
    /// </summary>
    private void Load() {
        foreach (var attribute in Enum.GetValues(typeof(Attribute))) {
            var name = Enum.GetName(typeof(Attribute), attribute);
            if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetFloat(name, GetDefault((Attribute)attribute));
            _attributes[(Attribute)attribute] = PlayerPrefs.GetFloat(name);
        }
    }

    /// <summary>
    /// When the application quits this method is called and saves all the attributes to the PlayerPrefs
    /// </summary>
    public void Save() {
        PlayerPrefs.SetString(MannyBrain.StampKey, DateTime.Now.ToBinary().ToString());
        foreach(var attribute in _attributes)            
            PlayerPrefs.SetFloat(Enum.GetName(typeof(Attribute), attribute.Key), attribute.Value);
    }

    /// <summary>
    /// Returns a default value for each attribute (only called at first time)
    /// </summary>
    /// <param name="attribute">The attribute to return the default value for</param>
    private float GetDefault(Attribute attribute) {
        switch (attribute) {
            case Attribute.Food:
            case Attribute.Thirst:
                return 50;
            case Attribute.Level:
                return 1;
            case Attribute.Coins:
                return 1000;
            case Attribute.Skillpoints:
                return 1000;
            default:
                return 0;
        }
    }

}

