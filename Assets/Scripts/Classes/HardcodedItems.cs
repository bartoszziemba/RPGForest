using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGNamespace;

namespace RPGItems
{
    public static class HardcodedPlayers
    {
        public static string testPlayer = 
            @"100;100;Arnold;
eq;;shortSword;;;
inv;hpotion;hpotion;simpleBoots;simpleChestplate;simpleHelmet;simpleCloak;;;;;;;";
        public static string defaultPlayer =
    @"200;200;Arnold;
eq;;shortSword;;;
inv;hpotion;;;;;;;;;;;;
[0; 0; 0]";

    }
    public static class HardcodedItems
    {
        public static Dictionary<string, Item> items = new Dictionary<string, Item> {
            
            {"hpotion",new UsableItem("hpotion",name: "Health Potion", 
                value: 10, healthAmount: 10, sprite: Resources.Load<Sprite>("ItemSprites/hp")) },

            {"shortSword",new EquipableItem("shortSword",name: "Short Sword",
                value: 15, strength: 5, eqType: EquipableItem.EquipementType.Weapon,sprite: Resources.Load<Sprite>("ItemSprites/sword")) },
            {"sword",new EquipableItem("shortSword",name: "Short Sword",
                value: 15, strength: 10, eqType: EquipableItem.EquipementType.Weapon,sprite: Resources.Load<Sprite>("ItemSprites/sword")) },
            {"bigSword",new EquipableItem("shortSword",name: "Short Sword",
                value: 15, strength: 20, eqType: EquipableItem.EquipementType.Weapon,sprite: Resources.Load<Sprite>("ItemSprites/sword")) },

            {"simpleHelmet", new EquipableItem("simpleHelmet",name: "Simple Helmet",
                value: 15, armor: 5, eqType: EquipableItem.EquipementType.Helmet, sprite: Resources.Load<Sprite>("ItemSprites/helmets")) },
            {"simpleChestplate",new EquipableItem("simpleChestplate",name: "Simple Chestplate",
                value: 20, armor: 10, eqType: EquipableItem.EquipementType.Chestplate, sprite: Resources.Load<Sprite>("ItemSprites/armor")) },
            {"simpleBoots",new EquipableItem("simpleBoots",name: "Simple Boots",
                value: 15, armor: 5,eqType: EquipableItem.EquipementType.Boots, sprite: Resources.Load<Sprite>("ItemSprites/boots")) },
            {"simpleCloak",new EquipableItem("simpleCloak",name: "Simple Cloak",
                value: 15, armor: 5,eqType: EquipableItem.EquipementType.Chestplate, sprite: Resources.Load<Sprite>("ItemSprites/cloaks")) }
        
        };

        //public static Item healthPotion = new UsableItem(name: "Health Potion",
        //    value: 10, healthAmount: 10,sprite: Resources.Load<Sprite>("ItemSprites/hp"));

        //public static Item glowPotion = new UsableItem(name: "Glowing Potion",
        //    value: 10, healthAmount: 10);

        //public static Item shortSword = new EquipableItem(name: "Short Sword",
        //    value: 15, strength: 50, eqType: EquipableItem.EquipementType.Weapon,sprite: Resources.Load<Sprite>("ItemSprites/sword"));

        //public static Item simpleHelmet = new EquipableItem(name: "Simple Helmet",
        //    value: 15, armor: 5, eqType: EquipableItem.EquipementType.Helmet, sprite: Resources.Load<Sprite>("ItemSprites/helmets"));

        //public static Item simpleChestplate = new EquipableItem(name: "Simple Chestplate",
        //    value: 20, armor: 10, eqType: EquipableItem.EquipementType.Chestplate, sprite: Resources.Load<Sprite>("ItemSprites/armor"));

        //public static Item simpleBoots = new EquipableItem(name: "Simple Boots",
        //    value: 15, armor: 5,eqType: EquipableItem.EquipementType.Boots, sprite: Resources.Load<Sprite>("ItemSprites/boots"));

        //public static Item simpleCloak = new EquipableItem(name: "Simple Cloak",
        //    value: 15, armor: 5,eqType: EquipableItem.EquipementType.Chestplate, sprite: Resources.Load<Sprite>("ItemSprites/cloaks"));

    }

    public static class HardcodedEnemies
    {
        public static FighterInfo oversisedWurm = new FighterInfo("oversized wurm", 100, 100, 2,2);
        public static FighterInfo fatRat = new FighterInfo("fat rat", 100, 100, 3,1);
        public static FighterInfo smallWasp = new FighterInfo("small wasp", 100, 100, 4,0);
    }
}
