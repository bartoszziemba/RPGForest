using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGNamespace
{
    public class FighterInfo
    {

        public string name;
        public int hp;
        public int hpMax;
        public int strength;
        public int defence;

        public FighterInfo()
        {

        }
        public FighterInfo(FighterInfo orginal):this(orginal.name,orginal.hp,orginal.hpMax,orginal.strength,orginal.defence)
        {

        }
        public FighterInfo(string _name,int _hp,int _maxhp, int _strength,int _defence)
        {
            name = _name;
            hp = _hp;
            hpMax = _maxhp;
            strength = _strength;
            defence = _defence;
        }
        public void HealSelf(int amount)
        {
            hp += amount;
            if (hp > hpMax)
                hp = hpMax;
        }
        public void ReciveHit(FighterInfo source)
        {
            hp -= Math.Max(source.strength - defence/2,0);
            if (hp <= 0)
                hp = 0;
        }
        public void Attack(FighterInfo target)
        {
            target.ReciveHit(this);
        }
        public override string ToString()
        {
            return $"[Fighter {name}, {hp}/{hpMax}, att:{strength} def:{defence}]";
        }
    }
}
