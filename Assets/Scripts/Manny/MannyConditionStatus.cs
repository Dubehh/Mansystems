namespace Assets.Scripts.Manny {
    /// <summary>
    ///     A data class that represents a specific state of Manny.
    /// </summary>
    public class MannyConditionStatus {
        public float Minimum { get; set; }
        public float Decrease { get; set; }
        public bool Weak { get; set; }
        public Attribute Attribute { get; set; }
        public string Message { get; set; }

        public void SetWeak(bool weak) {
            Weak = weak;
        }
    }
}