using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGItems
{
    public class UsableItem : Item
    {
        public int healthAmount;
        public int glowTime;
        public UsableItem(string id,int value = 0,int healthAmount = 0,int glowTime = 0,string name = "unnamed",Sprite sprite = null) : base(id)
        {

            this.value = value;
            this.healthAmount = healthAmount;
            this.glowTime = glowTime;
            this.name = name;
            this.sprite = sprite;
        }
        public UsableItem(UsableItem orginal) : base(orginal.id)
        {
            this.value = orginal.value;
            this.healthAmount = orginal.healthAmount;
            this.glowTime = orginal.glowTime;
            this.name = orginal.name;
            this.sprite = orginal.sprite;
        }

        public static UsableItem FromString(string repr)
        {
            UsableItem usableItem = new UsableItem(HardcodedItems.items[repr] as UsableItem);
            return usableItem;
        }

        public override void Use()
        {
            if (owner)
                owner.Use(this);
            else
                Debug.LogError("Cant find target to use");
        }

        public override string Serialize()
        {
            return this.id;
        }
    }
}
