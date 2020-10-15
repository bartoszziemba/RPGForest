using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGItems
{
    public abstract class Item
    {
        protected PlayerComponent owner;
        public Sprite sprite;
        public readonly string id;
        public int value;
        public int inventoryPlace = -1;
        public string name = "unnamed";

        public abstract void Use();

        public override string ToString()
        {
            return id;
        }
        public void SetOwner(PlayerComponent newOwner)
        {
            owner = newOwner;
        }
        protected Item(string id)
        {
            this.id = id;
        }

        public abstract string Serialize();

    }
}