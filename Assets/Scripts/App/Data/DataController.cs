using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data {
    public class DataController : MonoBehaviour{

        public DataStorage Storage { get; set; }
        private void Awake() {
            Storage = new DataStorage();
        }



    }
}
