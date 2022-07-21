namespace NavalVessels.Models
{
    public class Submarine:Vessel
    {
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 200)
        {
        }
        public  bool SubmergeMode = false;

        public void ToggleSubmergeMode()
        {
            if (SubmergeMode==true)
            {
                SubmergeMode=false;
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else if (SubmergeMode == false)
            {
                SubmergeMode = true;
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
        }

        public override void RepairVessel()
        {
            if (ArmorThickness<200)
            {
                ArmorThickness = 200;
            }
        }

        public override string ToString()
        {
            string onOrOff = null;
            onOrOff = SubmergeMode == true ? "ON" : "OFF";
            return base.ToString() + $"*Submerge mode: {onOrOff}";
        }
    }
}