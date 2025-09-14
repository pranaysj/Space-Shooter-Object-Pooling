using CosmicCuration.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace CosmicCuration.PowerUps
{
    public class PowerUpPool : GenericObjectPool<PowerUpController>
    {
        private PowerUpData powerUpData;

        public PowerUpController GetPowerUp<T>(PowerUpData powerUpData) where T : PowerUpController
        {
            this.powerUpData = powerUpData;
            return GetItem<T>(); 
        }

        protected override PowerUpController CreateItem<T>()
        {
            if(typeof(T) == typeof(Shield))
            {
                return new Shield(powerUpData);
            }
            else if(typeof(T) == typeof(RapidFire))
            {
                return new RapidFire(powerUpData);
            }
            else if(typeof(T) == typeof(DoubleTurret))
            {
                return new DoubleTurret(powerUpData);
            }
            else
            {
                throw new NotImplementedException("CreateItem() method not implemented for this type in PowerUpPool");
            }
        }
    }
}
