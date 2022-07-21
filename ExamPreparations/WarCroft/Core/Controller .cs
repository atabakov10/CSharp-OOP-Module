using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Core
{
    public class Controller:IController
    {
        private readonly VesselRepository vesselRepository;
        private readonly List<ICaptain> captainRepository;

        public Controller()
        {
            vesselRepository= new VesselRepository();
            captainRepository= new List<ICaptain>();
        }
        public string HireCaptain(string fullName)
        {
            ICaptain captain = captainRepository.FirstOrDefault(x => x.FullName == fullName);
            if (captain!=null)
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }

            ICaptain newCaptain = new Captain(fullName);
            captainRepository.Add(newCaptain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel vessel = vesselRepository.FindByName(name);
            if (vessel != null)
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vessel.GetType().Name, name);
            }

            if (vesselType!= "Submarine")
            {
                return "Invalid vessel type.";
            }
            else if (vesselType!= "Battleship")
            {
                return "Invalid vessel type.";
            }

            IVessel newVessel = null;
            if (vesselType== "Submarine")
            {
                newVessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType=="Battleship")
            {
                newVessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            vesselRepository.Add(newVessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, newVessel.GetType().Name, name,
                mainWeaponCaliber, speed);
           
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captain = captainRepository.FirstOrDefault(x => x.FullName == selectedCaptainName);
            if (captain == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }
            IVessel vessel = vesselRepository.Models.FirstOrDefault(x => x.Name == selectedVesselName); 
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            IVessel capitan = captain.Vessels.FirstOrDefault(x => x.Name == selectedVesselName);
            if (capitan!=null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }
            captain.AddVessel(vessel);
            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);

        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain captain =captainRepository.FirstOrDefault(x => x.FullName == captainFullName);
            return captain.Report();
        }

        public string VesselReport(string vesselName)
        {
           IVessel vessel = vesselRepository.FindByName(vesselName);
           return vessel.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vesselRepository.FindByName(vesselName);
            if (vessel.GetType().Name == "Battleship")
            {
                vessel.
            }
            throw new System.NotImplementedException();

        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            throw new System.NotImplementedException();
        }

        public string ServiceVessel(string vesselName)
        {
            throw new System.NotImplementedException();
        }
    }
}