using UnityEngine;

namespace StaticCalculator
{
    [System.Serializable]
    public class Calculate
    {   
        public static double NewtonSecondLaw(out double force, double mass, double acceleration)
        {
            return force = mass * acceleration;
        }

        public static double ImpulseOfBody(out double impulse, double mass, double velocity)
        {
            return impulse = mass * velocity;
        }

        // V - Voltage => volts
        // I - Current => amperes/amps
        // R - Resistance => ohms
        public static double OhmLaw(out double current, double resistance, double voltage)
        {
            return current = voltage / resistance;
        }

        // Q - Heat => Joules
        // c - Specific heat Capacity => теплоёмкость (Джоули / кг * градус по цельсию)
        public static double AmountOfWaterHeat(out double heat, double mass, double deltaTemperature, ConstantVariables constant)
        {
            return heat = constant.WaterHeatCapacity * mass * deltaTemperature;
        }
    }

    [CreateAssetMenu(menuName = "Calculator/Constant")]
    public class ConstantVariables : ScriptableObject
    {
        [SerializeField] private double _waterHeatCapacity = 4200;

        public double WaterHeatCapacity => _waterHeatCapacity;
    }

    public abstract class A_SO_Operation : ScriptableObject
    {
        [SerializeField] private string _formula;
    }
}