using SuperCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCalculator
{
    internal class BMIRCalculator
    {
        private double height;
        private double weight;

        private UnitTypes unit;

        private int age;

        public BMIRCalculator()
        {
            unit = UnitTypes.Metric;
        }

        #region Setters and getters

        public void SetHeight(double height)
        {
            if (height >= 0.0)
                this.height = height;
        }

        public void SetWeight(double weight)
        {
            this.weight = weight;
        }

        public void SetAge(int age)
        {
            this.age = age;
        }

        public double GetAge()
        {
            return age;
        }

        public double GetHeight()
        {
            return height;
        }

        public double GetWeight()
        {
            return weight;
        }

        public void SetUnitType(UnitTypes unit)
        {
            this.unit = unit;
        }

        public UnitTypes GetUnitType()
        {
            return unit;
        }

        #endregion

        // Calculations of BMI
        // Formula: weight (lb) / [height (in)]^2 * 703 (factor) or
        // weight (kg) / [height (m)] ^ 2

        public double CalculateBMI()
        {
            double bmivalue = 0.0;
            double factor = 1.0;

            if (GetUnitType() == UnitTypes.Imperial)
            {
                factor = 703.0;
            }

            bmivalue = factor * weight / (height * height);

            return bmivalue;
        }

        public string BMIWeightCategory()
        {
            double bmi = CalculateBMI();
            string bmiString = string.Empty;

            if (bmi < 18.5)
                bmiString = "Underweight";
            else if (bmi < 24.9)
                bmiString = "Normal Weight";
            else if (bmi < 29.9)
                bmiString = "Overweight (Pre-obesity)";
            else if (bmi < 34.9)
                bmiString = "Overweight (Obesity Class I)";
            else if (bmi < 39.9)
                bmiString = "Overweight (Obesity Class II)";
            else
                bmiString = "Overweight (Obesity Class III)";

            return bmiString;

        }

        public double GetNormalWelghtUpper()
        {
            double factor = 1.0;
            if (GetUnitType() == UnitTypes.Imperial)
            {
                factor = 703.0;
            }
            double NormalWelghtUpper = 24.9 * height * height / factor;
            return NormalWelghtUpper;
        }

        public double GetNormalWelghtLower()
        {
            double factor = 1.0;
            if (GetUnitType() == UnitTypes.Imperial)
            {
                factor = 703.0;
            }
            double NormalWelghtLower = 18.5 * height * height / factor;
            return NormalWelghtLower;
        }



        public double CalculateBMR()
        {
            double bmr = 0.0;
            
            double weightFactor = 0.45359237;
            double heightFactor = 2.54;

            if (GetUnitType() == UnitTypes.Imperial)
            {
                bmr = 10 * (weight*weightFactor) + 6.25 * height * heightFactor - 5 * age;
            }
            else
            {
                bmr = 10 * weight + 6.25 * 100* height - 5 * age;
            }
            return bmr;
        }
    }
}
