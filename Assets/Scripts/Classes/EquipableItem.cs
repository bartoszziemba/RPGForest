using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGItems
{
    public class EquipableItem : UsableItem
    {
        public enum EquipementType { Undefined = 0, Helmet, Chestplate, Boots, Weapon };
        public EquipementType type;
        public int armor { get; private set; }
        public int strength { get; private set; }
        public bool equiped = false;
        public EquipableItem(string id,int armor = 0, int strength = 0, int value = 0, string name = "unnamed", EquipementType eqType = EquipementType.Undefined, Sprite sprite = null):base(id)
        {
            this.armor = armor;
            this.strength = strength;
            this.value = value;
            this.name = name;
            this.type = eqType;
            this.sprite = sprite;
        }
        public EquipableItem(EquipableItem orginal):base(orginal.id)
        {
            this.armor = orginal.armor;
            this.strength = orginal.strength;
            this.value = orginal.value;
            this.name = orginal.name;
            this.type = orginal.type;
            this.owner = orginal.owner;
            this.sprite = orginal.sprite;
        }

        public override void Use()
        {
            if (owner)
                if (equiped)
                    owner.UnEquip(this);
                else
                    owner.Equip(this);
            else
                Debug.LogError("Cant find target to equip");
        }
        public new static EquipableItem FromString(string repr)
        {
            EquipableItem usableItem = new EquipableItem(HardcodedItems.items[repr] as EquipableItem);
            return usableItem;
        }
        public override string Serialize()
        {
            return this.id;
        }
    }

}