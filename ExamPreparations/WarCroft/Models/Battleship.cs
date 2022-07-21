namespace NavalVessels.Models
{
    public class Battleship:Vessel
    {
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 300)
        {

        }
        public bool SonarMode=false;

        public void ToggleSonarMode()
        {
            
            if (SonarMode==true)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
                SonarMode=false;
            }
            else if (SonarMode==false)
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
                SonarMode=true;
            }
           
        }

        public override void RepairVessel()
        {
            if (ArmorThickness<300)
            {
                ArmorThickness = 300;
            }
        }

        public override string ToString()
        {
            string onOrOff = null;
            onOrOff = SonarMode==true ? "ON" : "OFF;";
            
            return base.ToString() + $"Sonar mode: {onOrOff}";

        }
    }
}