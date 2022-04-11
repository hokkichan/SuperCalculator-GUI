using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCalculator
{
    internal class SavingCalculator
    {
        int numOfMonths;
        double initialDeposit;
        double monthlySavings;
        double growthRate;
        double feeRate;

        public void SetInitialDeposit(double value)
        {
            initialDeposit = value;
        }

        public void SetMonthlySaving(double amount)
        {
            monthlySavings = amount;
        }

        public void SetPeriod (int period)
        {
            numOfMonths = period*12;
        }

        public void SetGrowthRate (double value)
        {
            growthRate = value;
        }

        public void SetFeeRate(double value)
        {
            feeRate = value;
        }

        // Calculations on total money deposited by client
        public double CalculateAmountPaid()
        {
            double amountPaid = initialDeposit + monthlySavings * numOfMonths;
            return amountPaid;
        }

        // Calculations on net amount to be received by client
        public double CalculateSavings()
        {
            double interestEarned;
            double balance = initialDeposit;

            for (int i=0; i < numOfMonths; i++)
            {
                double netRate = (growthRate - feeRate) / 1200.0;
                interestEarned = netRate * balance;
                balance += interestEarned + monthlySavings;
            }

            return balance;
        }

        // Calculations on gross amount before deducting fees charged
        public double CalculateGrossTotal()
        {
            double interestEarned;
            double grossBalance = initialDeposit;

            for (int i = 0; i < numOfMonths; i++)
            {
                double netRate = (growthRate) / 1200.0;
                interestEarned = netRate * grossBalance;
                grossBalance += interestEarned + monthlySavings;
            }
            
            return grossBalance;
        }     
    }
}
